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
// Create Time         :    2017/8/8 11:44:59
// Update Time         :    2017/8/8 11:44:59
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using FKVoxelEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public class FKSky : DrawableGameComponent, IFKSkyService
    {
        private const int SIZE = 150;
        private bool[,] Clouds = new bool[SIZE, SIZE];

        public List<FKBlockVertex> VertexList;
        public List<short> IndexList;
        public short Index = 0;
        public VertexBuffer VertexBuffer { get; set; }
        public IndexBuffer IndexBuffer { get; set; }

        private bool _bIsMeshBuilt = false;
        private Effect _BlockEffect;
        private Texture2D _BlockTextureAtlas;
        private IFKCameraService _Camera;
        private IFKAssetManagerService _AssetManager;
        private IFKFoggerService _Fogger;

        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKSky");

        public FKSky(Game game)
            : base(game)
        {
            VertexList = new List<FKBlockVertex>();
            IndexList = new List<short>();
            Index = 0;

            Game.Services.AddService(typeof(IFKSkyService), this);
        }

        public override void Initialize()
        {
            Logger.Trace("Init()");

            _Camera = (IFKCameraService)(Game.Services.GetService(typeof(IFKCameraService)));
            _Fogger = (IFKFoggerService)(Game.Services.GetService(typeof(IFKFoggerService)));
            _AssetManager = (IFKAssetManagerService)(Game.Services.GetService(typeof(IFKAssetManagerService)));

            if (_AssetManager == null)
                throw new NullReferenceException("Can not find asset manager component.");

            var colors = TextureTo2DArray(_AssetManager.CloudTexture);
           
            for(int x = 0; x < SIZE; ++x)
            {
                for(int z = 0; z < SIZE; ++z)
                {
                    Clouds[x, z] = (colors[x, z] == Color.White);
                }
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _BlockEffect = _AssetManager.BlockEffect;
            _BlockTextureAtlas = _AssetManager.BlockTextureAtlas;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            BuildMesh();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game.GraphicsDevice.BlendState = BlendState.Opaque;

            _BlockEffect.Parameters["World"].SetValue(Matrix.Identity);
            _BlockEffect.Parameters["View"].SetValue(_Camera.View);
            _BlockEffect.Parameters["Projection"].SetValue(_Camera.Projection);
            _BlockEffect.Parameters["CameraPosition"].SetValue(_Camera.Position);

            _BlockEffect.Parameters["BlockTextureAtlas"].SetValue(_BlockTextureAtlas);

            _BlockEffect.Parameters["SunColor"].SetValue(FKWorld.SunColor);
            _BlockEffect.Parameters["NightColor"].SetValue(FKWorld.NightColor);
            _BlockEffect.Parameters["HorizonColor"].SetValue(FKWorld.HorizonColor);
            _BlockEffect.Parameters["MorningTint"].SetValue(FKWorld.MorningTint);
            _BlockEffect.Parameters["EveningTint"].SetValue(FKWorld.EveningTint);

            _BlockEffect.Parameters["TimeOfDay"].SetValue(FKGameTime.GetGameTimeOfDay());

            _BlockEffect.Parameters["FogNear"].SetValue(_Fogger.FogVector.X);
            _BlockEffect.Parameters["FogFar"].SetValue(_Fogger.FogVector.Y);

            foreach(var pass in _BlockEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                if (IndexBuffer == null || VertexBuffer == null)
                    continue;
                if (VertexBuffer.VertexCount <= 0 || IndexBuffer.IndexCount <= 0)
                    continue;

                Game.GraphicsDevice.SetVertexBuffer(VertexBuffer);
                Game.GraphicsDevice.Indices = IndexBuffer;
                Game.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 
                    /* 0, VertexBuffer.VertexCount, */ 0, IndexBuffer.IndexCount / 3);
            }

            base.Draw(gameTime);
        }

        private Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);

            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                    colors2D[x, y] = colors1D[x + y * texture.Width];

            return colors2D;
        }

        private void BuildMesh()
        {
            if (_bIsMeshBuilt)
                return;

            for(int x = 0; x < SIZE; ++x)
            {
                for(int z = 0; z < SIZE; ++z)
                {
                    if (!Clouds[x, z])
                        continue;

                    BuildBlockVertices(x, z);
                }
            }

            var vertices = VertexList.ToArray();
            var indices = IndexList.ToArray();

            if (vertices.Length == 0 || indices.Length == 0)
                return;

            VertexBuffer = new VertexBuffer(Game.GraphicsDevice, typeof(FKBlockVertex), vertices.Length, BufferUsage.WriteOnly);
            VertexBuffer.SetData(vertices);

            IndexBuffer = new IndexBuffer(Game.GraphicsDevice, IndexElementSize.SixteenBits, indices.Length, BufferUsage.WriteOnly);
            IndexBuffer.SetData(indices);

            _bIsMeshBuilt = true;
        }
        
        private void BuildBlockVertices(int x, int z)
        {
            var north = z != SIZE - 1 && this.Clouds[x, z + 1];
            var south = z != 0 && this.Clouds[x, z - 1];
            var east = x != SIZE - 1 && this.Clouds[x + 1, z];
            var west = x != 0 && this.Clouds[x - 1, z];

            if (!west)
            {
                BuildFaceVertices(x, z, ENUM_BlockFaceDirection.eXDecreasing);
            }
            if (!east)
            {
                BuildFaceVertices(x, z, ENUM_BlockFaceDirection.eXIncreasing);
            }

            // -yface
            BuildFaceVertices(x, z, ENUM_BlockFaceDirection.eYDecreasing);

            // +yface
            BuildFaceVertices(x, z, ENUM_BlockFaceDirection.eYIncreasing);

            if (!south) // -zface
            {
                BuildFaceVertices(x, z, ENUM_BlockFaceDirection.eZDecreasing);
            }
            if (!north) // +zface
            {
                BuildFaceVertices(x, z, ENUM_BlockFaceDirection.eZIncreasing);
            }
        }

        private void BuildFaceVertices(int x, int z, ENUM_BlockFaceDirection faceDir)
        {
            ENUM_FKBaseBlockTextureType texture = FKBaseBlock.GetTextureType(ENUM_FKBaseBlockType.eSnow, faceDir);
            int faceIndex = 0;

            var textureUVMappings = FKBaseBlockTextureHelper.BlockTextureMappings[(int)texture * 6 + faceIndex];

            switch (faceDir)
            {
                case ENUM_BlockFaceDirection.eXIncreasing:
                    {
                        //TR,TL,BR,BR,TL,BL
                        AddVertex(x, z, new Vector3(1, 1, 1), textureUVMappings[0]);
                        AddVertex(x, z, new Vector3(1, 1, 0), textureUVMappings[1]);
                        AddVertex(x, z, new Vector3(1, 0, 1), textureUVMappings[2]);
                        AddVertex(x, z, new Vector3(1, 0, 0), textureUVMappings[5]);
                        AddIndex(0, 1, 2, 2, 1, 3);
                    }
                    break;

                case ENUM_BlockFaceDirection.eXDecreasing:
                    {
                        //TR,TL,BL,TR,BL,BR
                        AddVertex(x, z, new Vector3(0, 1, 0), textureUVMappings[0]);
                        AddVertex(x, z, new Vector3(0, 1, 1), textureUVMappings[1]);
                        AddVertex(x, z, new Vector3(0, 0, 0), textureUVMappings[5]);
                        AddVertex(x, z, new Vector3(0, 0, 1), textureUVMappings[2]);
                        AddIndex(0, 1, 3, 0, 3, 2);
                    }
                    break;

                case ENUM_BlockFaceDirection.eYIncreasing:
                    {
                        //BL,BR,TR,BL,TR,TL
                        AddVertex(x, z, new Vector3(1, 1, 1), textureUVMappings[0]);
                        AddVertex(x, z, new Vector3(0, 1, 1), textureUVMappings[2]);
                        AddVertex(x, z, new Vector3(1, 1, 0), textureUVMappings[4]);
                        AddVertex(x, z, new Vector3(0, 1, 0), textureUVMappings[5]);
                        AddIndex(3, 2, 0, 3, 0, 1);
                    }
                    break;

                case ENUM_BlockFaceDirection.eYDecreasing:
                    {
                        //TR,BR,TL,TL,BR,BL
                        AddVertex(x, z, new Vector3(1, 0, 1), textureUVMappings[0]);
                        AddVertex(x, z, new Vector3(0, 0, 1), textureUVMappings[2]);
                        AddVertex(x, z, new Vector3(1, 0, 0), textureUVMappings[4]);
                        AddVertex(x, z, new Vector3(0, 0, 0), textureUVMappings[5]);
                        AddIndex(0, 2, 1, 1, 2, 3);
                    }
                    break;

                case ENUM_BlockFaceDirection.eZIncreasing:
                    {
                        //TR,TL,BL,TR,BL,BR
                        AddVertex(x, z, new Vector3(0, 1, 1), textureUVMappings[0]);
                        AddVertex(x, z, new Vector3(1, 1, 1), textureUVMappings[1]);
                        AddVertex(x, z, new Vector3(0, 0, 1), textureUVMappings[5]);
                        AddVertex(x, z, new Vector3(1, 0, 1), textureUVMappings[2]);
                        AddIndex(0, 1, 3, 0, 3, 2);
                    }
                    break;

                case ENUM_BlockFaceDirection.eZDecreasing:
                    {
                        //TR,TL,BR,BR,TL,BL
                        AddVertex(x, z, new Vector3(1, 1, 0), textureUVMappings[0]);
                        AddVertex(x, z, new Vector3(0, 1, 0), textureUVMappings[1]);
                        AddVertex(x, z, new Vector3(1, 0, 0), textureUVMappings[2]);
                        AddVertex(x, z, new Vector3(0, 0, 0), textureUVMappings[5]);
                        AddIndex(0, 1, 2, 2, 1, 3);
                    }
                    break;
            }
        }

        private void AddVertex(int x, int z, Vector3 addition, HalfVector2 textureCoordinate)
        {
            VertexList.Add(new FKBlockVertex(new Vector3(x, 128, z) + addition, textureCoordinate, 1f));
        }

        private void AddIndex(short i1, short i2, short i3, short i4, short i5, short i6)
        {
            IndexList.Add((short)(Index + i1));
            IndexList.Add((short)(Index + i2));
            IndexList.Add((short)(Index + i3));
            IndexList.Add((short)(Index + i4));
            IndexList.Add((short)(Index + i5));
            IndexList.Add((short)(Index + i6));
            Index += 4;
        }
    }
}