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
// Create Time         :    2017/7/24 17:55:40
// Update Time         :    2017/7/24 17:55:40
// Class Version       :    v1.0.0.0
// Class Description   :    音频管理
// ===============================================================================
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using FKVoxelEngine.Base;
using FKVoxelEngine.Platform;
using System.Threading;
using FKVoxelEngine.Framework;
// ===============================================================================
namespace FKVoxelEngine.Audio
{
    public class FKAudioManager : GameComponent, IFKAudioManagerService
    {
        #region ==== 成员变量 ====

        const string MUSIC_PATH = "Audio\\Music\\";
        const string SOUND_PATH = "Audio\\Sound\\";

        private Song                                        _BackgroundSong;
        private SoundEffectInstance                         _CurSoundEffect;
        private Dictionary<string, SoundEffect>             _SoundEffectsList;
        private Random                                      _Random;

        private static readonly FKLogger _Logger = FKLogManager.CreateLogger("FKAudio");

        #endregion ==== 成员变量 ====

        #region ==== 对外接口 ====

        public FKAudioManager(Game game)
            :base(game)
        {
            _SoundEffectsList = new Dictionary<string, SoundEffect>();
            _Random = new Random();

            Game.Services.AddService(typeof(IFKAudioManagerService), this);
        }

        public void SetSoundEffect(string fileName)
        {
            if(!_SoundEffectsList.ContainsKey(fileName))
            {
                PreloadSoundEffect(fileName);
            }
            _CurSoundEffect = _SoundEffectsList[fileName].CreateInstance();
        }

        public void SetBackgroundSong(string fileName)
        {
            _BackgroundSong = Game.Content.Load<Song>(MUSIC_PATH + fileName);
            PlayBackgoundSong();
        }

        public void PreloadSoundEffect(string fileName)
        {
            _SoundEffectsList.Add(fileName, Game.Content.Load<SoundEffect>(SOUND_PATH + fileName));
        }

        #endregion ==== 对外接口 ====

        #region ==== 内部函数 ====

        public override void Initialize()
        {
            try
            {
                _Logger.Trace("Init()");

                // 开始播放音乐
                PlayBackgoundSong();
                // 开启音效线程
                var SoundThread = new Thread(SoundThreadMain);
                SoundThread.Start();
            }
            catch (Exception e)
            {
                _Logger.FatalException(e, "FKAudioManager init failed");
            }
        }

        /// <summary>
        /// 音效线程
        /// </summary>
        private void SoundThreadMain()
        {
            // todo: 声音是否开启的配置检查
            Thread.CurrentThread.Name = "FKSoundThread";
            while (true)
            {
                if (_CurSoundEffect == null || _CurSoundEffect.IsDisposed || _CurSoundEffect.State == SoundState.Playing)
                {
                    Thread.Sleep(100);
                    continue;
                }

                FadeInSoundEffect();
                _CurSoundEffect.Play();
                FadeOutSoundEffect();
            }
        }

        private void PlayBackgoundSong()
        {
            // todo: 音乐是否开启的配置检查
            if(FKPlatformManager.GetInstance.CurGameFramework == FKFrameworkEnum.eFramework_MonoGame)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(_BackgroundSong);
                MediaPlayer.Volume = 0.3f;
            }
        }

        private void FadeOutSoundEffect()
        {
            if (this._CurSoundEffect.State == SoundState.Stopped || this._CurSoundEffect.State == SoundState.Paused) return;

            for (float f = this._CurSoundEffect.Volume; f > 0f; f -= 0.05f)
            {
                Thread.Sleep(10);
                this._CurSoundEffect.Volume -= f;
            }
        }

        private void FadeInSoundEffect()
        {
            for (float f = this._CurSoundEffect.Volume; f < 1f; f += 0.05f)
            {
                Thread.Sleep(10);
                this._CurSoundEffect.Volume += f;
            }
        }

        #endregion ==== 内部函数 ====
    }
}