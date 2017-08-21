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
// Create Time         :    2017/8/8 18:07:12
// Update Time         :    2017/8/8 18:07:12
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using FKVoxelEngine.Framework;
using FKVoxelEngine.RenderObj;
using Microsoft.Xna.Framework;
using System;
// ===============================================================================
namespace FKVoxelEngine.Graphics
{
    public class FKCamera : GameComponent, IFKCameraService
    {
        public Matrix Projection { get; private set; }
        public Matrix View { get; private set; }
        public Matrix World { get; private set; }

        public Vector3 Position { get; set; }
        public FKBaseChunk CurrentChunk { get; set; }
        public float CameraMoveSpeed { get; set; }

        private Vector3 _LookVector;
        private float _fCurrentElevation;
        private float _fCurrentRotation;
        private float _fAspectRatio;                     // 视角宽高比

        private IFKWorldService _World;

        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKCamera");

        private const float VIEW_ANGLE = MathHelper.PiOver4;
        private const float NEAR_PLANE_DISTANCE = 0.01f;
        private const float FAR_PLANE_DISTANCE = 1000f;
        private const float ROTATION_SPEED = 0.002f;

        public FKCamera(Game game)
            : base(game)
        {
            _World = (IFKWorldService)Game.Services.GetService(typeof(IFKWorldService));
            Game.Services.AddService(typeof(IFKCameraService), this);
        }

        public override void Initialize()
        {
            Logger.Trace("Init()");

            _fAspectRatio = Game.GraphicsDevice.Viewport.AspectRatio;
            World = Matrix.Identity;
            Projection = Matrix.CreatePerspectiveFieldOfView(VIEW_ANGLE, _fAspectRatio, NEAR_PLANE_DISTANCE, FAR_PLANE_DISTANCE);

            CameraMoveSpeed = 10.0f;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            var rotation = Matrix.CreateRotationX(_fCurrentElevation) * Matrix.CreateRotationY(_fCurrentRotation);
            var target = Vector3.Transform(Vector3.Forward, rotation) + Position;
            var upVector = Vector3.Transform(Vector3.Up, rotation);
            View = Matrix.CreateLookAt(Position, target, upVector);

            ProcessView();

            base.Update(gameTime);
        }

        public void RotateCamera(float step)
        {
            _fCurrentRotation -= ROTATION_SPEED * step;
        }

        public void ElevateCamera(float step)
        {
            _fCurrentElevation -= ROTATION_SPEED * step;
        }

        public void LookAt(Vector3 target)
        {
            View = Matrix.CreateLookAt(Position, target, Vector3.Up);
        }

        private void ProcessView()
        {
            var rotationMatrix = Matrix.CreateRotationX(_fCurrentElevation) *
                     Matrix.CreateRotationY(_fCurrentRotation);
            _LookVector = Vector3.Transform(Vector3.Forward, rotationMatrix);
            _LookVector.Normalize();
        }

        public void MoveCamera(GameTime gameTime, ENUM_CameraMoveDirection eDirction)
        {
            var moveVector = Vector3.Zero;

            switch (eDirction)
            {
                case ENUM_CameraMoveDirection.eBackward:
                    moveVector.Z++;
                    break;
                case ENUM_CameraMoveDirection.eForward:
                    moveVector.Z--;
                    break;
                case ENUM_CameraMoveDirection.eLeft:
                    moveVector.X--;
                    break;
                case ENUM_CameraMoveDirection.eRight:
                    moveVector.X++;
                    break;
                case ENUM_CameraMoveDirection.eDown:
                    moveVector.Y++;
                    break;
                case ENUM_CameraMoveDirection.eUp:
                    moveVector.Y--;
                    break;
            }

            if (moveVector == Vector3.Zero)
                return;

            moveVector *= CameraMoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            var rotation = Matrix.CreateRotationY(_fCurrentRotation);
            var rotatedVector = Vector3.Transform(moveVector, rotation);
            Position += rotatedVector;
        }

        public void SetCamera(FKVector2Int position)
        {
            Position = new Vector3(position.X * FKBaseChunk.WidthInBlocks, 100, position.Z * FKBaseChunk.LengthInBlocks);
            _World.SetCamera(position);
        }
    }
}