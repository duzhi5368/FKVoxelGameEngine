/* 
 * WRANING: These codes below is far away from bugs with the god and his animal protecting
 *                  _oo0oo_                   ┏┓　　　┏┓
 *                 o8888888o                ┏┛┻━━━┛┻┓
 *                 88" . "88                ┃　　　　　　　┃ 　
 *                 (| -_- |)                ┃　　　━　　　┃
 *                 0\  =  /0                ┃　┳┛　┗┳　┃
 *               ___/`---'\___              ┃　　　　　　　┃
 *             .' \\|     |# '.             ┃　　　┻　　　┃
 *            / \\|||  :  |||# \            ┃　　　　　　　┃
 *           / _||||| -:- |||||- \          ┗━┓　　　┏━┛
 *          |   | \\\  -  #/ |   |          　　┃　　　┃神兽保佑
 *          | \_|  ''\---/''  |_/ |         　　┃　　　┃永无BUG
 *          \  .-\__  '-'  ___/-. /         　　┃　　　┗━━━┓
 *        ___'. .'  /--.--\  `. .'___       　　┃　　　　　　　┣┓
 *     ."" '<  `.___\_<|>_/___.' >' "".     　　┃　　　　　　　┏┛
 *    | | :  `- \`.;`\ _ /`;.`/ - ` : | |   　　┗┓┓┏━┳┓┏┛
 *    \  \ `_.   \_ __\ /__ _/   .-` /  /   　　　┃┫┫　┃┫┫
 *=====`-.____`.___ \_____/___.-`___.-'=====　　　┗┻┛　┗┻┛ 
 *                  `=---='　　　
 *          佛祖保佑       永无BUG
 */
// =============================================================================== 
// Author              :    Frankie.W
// Create Time         :    2017/8/11 14:43:45
// Update Time         :    2017/8/11 14:43:45
// Class Version       :    v1.0.0.0
// Class Description   :    CPU性能分析器
// ===============================================================================
using FKVoxelEngine.Base;
using FKVoxelEngine.Framework;
using FKVoxelEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
// ===============================================================================
namespace FKVoxelEngine.Profile
{
    public class FKTimeRuler : DrawableGameComponent, IFKTimeRulerService
    {
        private struct Marker
        {
            public int MarkerId;
            public float BeginTime;
            public float EndTime;
            public Color Color;
        }

        private class MarkerCollection
        {
            public Marker[] Markers = new Marker[MaxSamples];
            public int MarkerCount;
            public int[] MarkerNests = new int[MaxNestCall];
            public int NestCount;
        }

        private class FrameLog
        {
            public MarkerCollection[] Bars;

            public FrameLog()
            {
                Bars = new MarkerCollection[MaxBars];
                for (int i = 0; i < MaxBars; ++i)
                    Bars[i] = new MarkerCollection();
            }
        }

        private struct MarkerLog
        {
            public float SnapMin;
            public float SnapMax;
            public float SnapAvg;
            public float Min;
            public float Max;
            public float Avg;
            public int Samples;
            public Color Color;
            public bool Initialized;
        }

        private class MarkerInfo
        {
            public string Name;
            public MarkerLog[] Logs = new MarkerLog[MaxBars];
            public MarkerInfo(string name) { Name = name; }
        }

        const int MaxBars = 8;
        const int MaxSamples = 256;
        const int MaxNestCall = 32;
        const int MaxSampleFrames = 4;
        const int LogSnapDuration = 120;
        const int BarHeight = 8;
        const int BarPadding = 2;
        const int AutoAdjustDelay = 30;

        public bool ShowLog { get; set; }
        public int TargetSampleFrames { get; set; }
        public Vector2 Position { get; set; }
        public int Width { get; set; }

        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private SpriteFont spriteFont;
        private IFKAssetManagerService _AssetManager;

        private FrameLog[] logs;
        private FrameLog prevLog;
        private FrameLog curLog;
        private int frameCount;
        private int frameAdjust;
        private int sampleFrames;
        private int updateCount;
        private Vector2 position;

        private Stopwatch stopWatch = new Stopwatch();
        private List<MarkerInfo> markers = new List<MarkerInfo>();
        private Dictionary<string, int> markerNameToIdMap = new Dictionary<string, int>();
        private StringBuilder logString = new StringBuilder(512);

        public FKTimeRuler(Game game)
            :base(game)
        {
            Game.Services.AddService(typeof(IFKTimeRulerService), this);
        }

        public override void Initialize()
        {
            _AssetManager = (IFKAssetManagerService)Game.Services.GetService(typeof(IFKAssetManagerService));
            if (_AssetManager == null)
                throw new NullReferenceException("Can not find asset manager component.");

            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            texture = new Texture2D(Game.GraphicsDevice, 1, 1);
            Color[] whitePixels = new Color[] { Color.White };
            texture.SetData<Color>(whitePixels);

            spriteFont = _AssetManager.DefaultFont;

            logs = new FrameLog[2];
            for (int i = 0; i < logs.Length; ++i)
                logs[i] = new FrameLog();

            sampleFrames = TargetSampleFrames = 1;

            Enabled = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Width = (int)(GraphicsDevice.Viewport.Width * 0.8f);
            FKLayout layout = new FKLayout(GraphicsDevice.Viewport);
            position = layout.Place(new Vector2(Width, BarHeight), 0, 0.01f, Alignment.BottomCenter);
            base.LoadContent();
        }

        [Conditional("DEBUG")]
        public void StartFrame()
        {
            lock (this)
            {
                int count = Interlocked.Increment(ref updateCount);
                if (Visible && (1 < count && count < MaxSampleFrames))
                    return;

                prevLog = logs[frameCount++ & 0x1];
                curLog = logs[frameCount & 0x1];

                float endFrameTime = (float)stopWatch.Elapsed.TotalMilliseconds;
                for(int barIndex = 0; barIndex < prevLog.Bars.Length; ++barIndex)
                {
                    MarkerCollection prevBar = prevLog.Bars[barIndex];
                    MarkerCollection nextBar = curLog.Bars[barIndex];

                    for(int nest = 0; nest < prevBar.NestCount; ++nest)
                    {
                        int markerIndex = prevBar.MarkerNests[nest];

                        prevBar.Markers[markerIndex].EndTime = endFrameTime;

                        nextBar.MarkerNests[nest] = nest;
                        nextBar.Markers[nest].MarkerId = prevBar.Markers[markerIndex].MarkerId;
                        nextBar.Markers[nest].BeginTime = 0;
                        nextBar.Markers[nest].EndTime = -1;
                        nextBar.Markers[nest].Color = prevBar.Markers[markerIndex].Color;
                    }

                    for(int markerIndex = 0; markerIndex < prevBar.MarkerCount; ++markerIndex)
                    {
                        float duration = prevBar.Markers[markerIndex].EndTime - prevBar.Markers[markerIndex].BeginTime;
                        int markerId = prevBar.Markers[markerIndex].MarkerId;
                        MarkerInfo m = markers[markerId];

                        m.Logs[barIndex].Color = prevBar.Markers[markerIndex].Color;

                        if(!m.Logs[barIndex].Initialized)
                        {
                            m.Logs[barIndex].Min = duration;
                            m.Logs[barIndex].Max = duration;
                            m.Logs[barIndex].Avg = duration;

                            m.Logs[barIndex].Initialized = true;
                        }
                        else
                        {
                            m.Logs[barIndex].Min = Math.Min(m.Logs[barIndex].Min, duration);
                            m.Logs[barIndex].Max = Math.Min(m.Logs[barIndex].Max, duration);
                            m.Logs[barIndex].Avg += duration;
                            m.Logs[barIndex].Avg *= 0.5f;

                            if(m.Logs[barIndex].Samples++ >= LogSnapDuration)
                            {
                                m.Logs[barIndex].SnapMin = m.Logs[barIndex].Min;
                                m.Logs[barIndex].SnapMax = m.Logs[barIndex].Max;
                                m.Logs[barIndex].SnapAvg = m.Logs[barIndex].Avg;
                                m.Logs[barIndex].Samples = 0;
                            }
                        }
                    }

                    nextBar.MarkerCount = prevBar.NestCount;
                    nextBar.NestCount = prevBar.NestCount;
                }

                stopWatch.Reset();
                stopWatch.Start();
            }
        }

        public void BeginMark(string markerName, Color color)
        {
            BeginMark(0, markerName, color);
        }

        public void BeginMark(int barIndex, string markerName, Color color)
        {
            lock (this)
            {
                if(barIndex < 0 || barIndex >= MaxBars)
                {
                    throw new ArgumentOutOfRangeException("barIndex");
                }

                MarkerCollection bar = curLog.Bars[barIndex];
                if(bar.MarkerCount >= MaxSamples)
                {
                    throw new OverflowException("Exceeded sample count.\n" +
                        "Either set larger number to FKTimeRuler.MaxSmpale or" +
                        "lower sample count.");
                }

                if (bar.NestCount >= MaxNestCall)
                {
                    throw new OverflowException(
                        "Exceeded nest count.\n" +
                        "Either set larget number to FKTimeRuler.MaxNestCall or" +
                        "lower nest calls.");
                }

                int markerId = 0;
                if(!markerNameToIdMap.TryGetValue(markerName, out markerId))
                {
                    markerId = markers.Count;
                    markerNameToIdMap.Add(markerName, markerId);
                    markers.Add(new MarkerInfo(markerName));
                }

                bar.MarkerNests[bar.NestCount++] = bar.MarkerCount;

                bar.Markers[bar.MarkerCount].MarkerId = markerId;
                bar.Markers[bar.MarkerCount].Color = color;
                bar.Markers[bar.MarkerCount].BeginTime = (float)stopWatch.Elapsed.TotalMilliseconds;
                bar.Markers[bar.MarkerCount].EndTime = -1;

                bar.MarkerCount++;
            }
        }

        public void EndMark(string markerName)
        {
            EndMark(0, markerName);
        }

        public void EndMark(int barIndex, string markerName)
        {
            lock (this)
            {
                if (barIndex < 0 || barIndex >= MaxBars)
                {
                    throw new ArgumentOutOfRangeException("barIndex");
                }

                MarkerCollection bar = curLog.Bars[barIndex];
                if (bar.NestCount <= 0)
                {
                    throw new InvalidOperationException("Call BeginMark method before call EndMark method.");
                }

                int markerId = 0;
                if (!markerNameToIdMap.TryGetValue(markerName, out markerId))
                {
                    throw new InvalidOperationException(String.Format("Maker '{0}' is not registered." +
                            "Make sure you specifed same name as you used for BeginMark" +
                            " method.",
                            markerName));
                }

                int markerIdx = bar.MarkerNests[--bar.NestCount];
                if (bar.Markers[markerIdx].MarkerId != markerId)
                {
                    throw new InvalidOperationException(
                    "Incorrect call order of BeginMark/EndMark method." +
                    "You call it like BeginMark(A), BeginMark(B), EndMark(B), EndMark(A)" +
                    " But you can't call it like " +
                    "BeginMark(A), BeginMark(B), EndMark(A), EndMark(B).");
                }

                bar.Markers[markerIdx].EndTime =
                    (float)stopWatch.Elapsed.TotalMilliseconds;
            }
        }

        public float GetAverageTime(int barIndex, string markerName)
        {
            if (barIndex < 0 || barIndex >= MaxBars)
                throw new ArgumentOutOfRangeException("barIndex");

            float result = 0.0f;
            int markerId = 0;
            if (markerNameToIdMap.TryGetValue(markerName, out markerId))
            {
                result = markers[markerId].Logs[barIndex].Avg;
            }
            return result;
        }

        [Conditional("DEBUG")]
        public void ResetLog()
        {
            lock (this)
            {
                foreach(MarkerInfo markerInfo in markers)
                {
                    for(int i = 0; i < markerInfo.Logs.Length; ++i)
                    {
                        markerInfo.Logs[i].Initialized = false;

                        markerInfo.Logs[i].SnapAvg = markerInfo.Logs[i].SnapMax = markerInfo.Logs[i].SnapMin = 0;
                        markerInfo.Logs[i].Avg = markerInfo.Logs[i].Max = markerInfo.Logs[i].Min = 0;
                        markerInfo.Logs[i].Samples = 0;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Draw(position, Width);
            base.Draw(gameTime);
        }

        [Conditional("DEBUG")]
        public void Draw(Vector2 position, int width)
        {
            Interlocked.Exchange(ref updateCount, 0);

            int height = 0;
            float maxTime = 0;
            foreach (MarkerCollection bar in prevLog.Bars)
            {
                if (bar.MarkerCount > 0)
                {
                    height += BarHeight + BarPadding * 2;
                    maxTime = Math.Max(maxTime, bar.Markers[bar.MarkerCount - 1].EndTime);
                }
            }

            const float frameSpan = 1.0f / 60.0f * 1000f;
            float sampleSpan = (float)sampleFrames * frameSpan;

            if (maxTime > sampleSpan)
                frameAdjust = Math.Max(0, frameAdjust) + 1;
            else
                frameAdjust = Math.Min(0, frameAdjust) - 1;

            if (Math.Abs(frameAdjust) > AutoAdjustDelay)
            {
                sampleFrames = Math.Min(MaxSampleFrames, sampleFrames);
                sampleFrames = Math.Max(TargetSampleFrames, (int)(maxTime / frameSpan) + 1);

                frameAdjust = 0;
            }

            float msToPs = (float)width / sampleSpan;
            int startY = (int)position.Y - (height - BarHeight);
            int y = startY;

            spriteBatch.Begin();
            { 
                Rectangle rc = new Rectangle((int)position.X, y, width, height);
                spriteBatch.Draw(texture, rc, new Color(0, 0, 0, 128));

                rc.Height = BarHeight;
                foreach (MarkerCollection bar in prevLog.Bars)
                {
                    rc.Y = y + BarPadding;
                    if (bar.MarkerCount > 0)
                    {
                        for (int j = 0; j < bar.MarkerCount; ++j)
                        {
                            float bt = bar.Markers[j].BeginTime;
                            float et = bar.Markers[j].EndTime;
                            int sx = (int)(position.X + bt * msToPs);
                            int ex = (int)(position.X + et * msToPs);
                            rc.X = sx;
                            rc.Width = Math.Max(ex - sx, 1);

                            spriteBatch.Draw(texture, rc, bar.Markers[j].Color);
                        }
                    }

                    y += BarHeight + BarPadding;
                }

                rc = new Rectangle((int)position.X, (int)startY, 1, height);
                for (float t = 1.0f; t < sampleSpan; t += 1.0f)
                {
                    rc.X = (int)(position.X + t * msToPs);
                    spriteBatch.Draw(texture, rc, Color.Gray);
                }

                for (int i = 0; i <= sampleFrames; ++i)
                {
                    rc.X = (int)(position.X + frameSpan * (float)i * msToPs);
                    spriteBatch.Draw(texture, rc, Color.White);
                }

                if (ShowLog)
                {
                    y = startY - spriteFont.LineSpacing;
                    logString.Length = 0;
                    foreach (MarkerInfo markerInfo in markers)
                    {
                        for (int i = 0; i < MaxBars; ++i)
                        {
                            if (markerInfo.Logs[i].Initialized)
                            {
                                if (logString.Length > 0)
                                    logString.Append("\n");

                                logString.Append(" Bar ");
                                logString.AppendNumber(i);
                                logString.Append(" ");
                                logString.Append(markerInfo.Name);

                                logString.Append(" Avg.:");
                                logString.AppendNumber(markerInfo.Logs[i].SnapAvg);
                                logString.Append("ms ");

                                y -= spriteFont.LineSpacing;
                            }
                        }
                    }


                    Vector2 size = spriteFont.MeasureString(logString);
                    rc = new Rectangle((int)position.X, (int)y, (int)size.X + 12, (int)size.Y);
                    spriteBatch.Draw(texture, rc, new Color(0, 0, 0, 128));
                    spriteBatch.DrawString(spriteFont, logString,
                                            new Vector2(position.X + 12, y), Color.White);


                    y += (int)((float)spriteFont.LineSpacing * 0.3f);
                    rc = new Rectangle((int)position.X + 4, y, 10, 10);
                    Rectangle rc2 = new Rectangle((int)position.X + 5, y + 1, 8, 8);
                    foreach (MarkerInfo markerInfo in markers)
                    {
                        for (int i = 0; i < MaxBars; ++i)
                        {
                            if (markerInfo.Logs[i].Initialized)
                            {
                                rc.Y = y;
                                rc2.Y = y + 1;
                                spriteBatch.Draw(texture, rc, Color.White);
                                spriteBatch.Draw(texture, rc2, markerInfo.Logs[i].Color);

                                y += spriteFont.LineSpacing;
                            }
                        }
                    }
                }
            }
            spriteBatch.End();
        }
    }
}