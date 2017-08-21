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
// Create Time         :    2017/8/21 17:46:58
// Update Time         :    2017/8/21 17:46:58
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework;
using System;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public enum ENUM_CombinationFunctions
    {
        eCF_D1,
        eCF_D2MINUSD1,
        eCF_D3MINUSD1
    }

    public enum ENUM_DistanceType
    {
        eCF_EUCLIDEAN3D,
        eCF_EUCLIDEAN2D,
        eCF_MANHATTAN3D,
        eCF_MANHATTAN2D,
        eCF_CHEBYSHEV3D,
        eCF_CHEBYSHEV2D
    }

    public class FKCellNoise : FKNoiseGen
    {
        public int Seed { get; set; }
        public ENUM_DistanceType DistanceFunction { get; set; }
        public ENUM_CombinationFunctions CombinationFunction { get; set; }

        public FKCellNoise() : this(new Random().Next())
        {
        }

        public FKCellNoise(int seed)
        {
            Seed = seed;
            DistanceFunction = ENUM_DistanceType.eCF_EUCLIDEAN3D;
            CombinationFunction = ENUM_CombinationFunctions.eCF_D2MINUSD1;
        }

        public override float Noise2D(float X, float Y)
        {
            float[] Distances = new float[3];
            uint lastRandom, numberFeaturePoints;
            Vector3 randomDiff, featurePoint;
            int cubeX, cubeY;

            for (int i = 0; i < Distances.Length; i++)
            {
                Distances[i] = 6666;
            }

            int evalCubeX = FKMathFuncs.Floor(X);
            int evalCubeY = FKMathFuncs.Floor(Y);
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    cubeX = evalCubeX + i;
                    cubeY = evalCubeY + j;
                    lastRandom = lcgRandom(hash2d((uint)(cubeX + Seed), (uint)(cubeY)));
                    numberFeaturePoints = probLookup(lastRandom);
                    for (uint l = 0; l < numberFeaturePoints; ++l)
                    {
                        lastRandom = lcgRandom(lastRandom);
                        randomDiff.X = lastRandom / 0x100000000;
                        lastRandom = lcgRandom(lastRandom);
                        randomDiff.Y = lastRandom / 0x100000000;
                        lastRandom = lcgRandom(lastRandom);
                        randomDiff.Z = lastRandom / 0x100000000;
                        featurePoint = new Vector3(randomDiff.X + cubeX, randomDiff.Y + cubeY, 0);
                        insert(Distances, Distance(new Vector3(X, Y, 0), featurePoint));
                    }
                }
            }
            return Combine(Distances);
        }

        public override float Noise3D(float X, float Y, float Z)
        {
            float[] Distances = new float[3];
            uint lastRandom, numberFeaturePoints;
            Vector3 randomDiff, featurePoint;
            int cubeX, cubeY, cubeZ;
            for (int i = 0; i < Distances.Length; i++)
            {
                Distances[i] = 6666;
            }
            int evalCubeX = FKMathFuncs.Floor(X);
            int evalCubeY = FKMathFuncs.Floor(Y);
            int evalCubeZ = FKMathFuncs.Floor(Z);
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    for (int k = -1; k < 2; ++k)
                    {
                        cubeX = evalCubeX + i;
                        cubeY = evalCubeY + j;
                        cubeZ = evalCubeZ + k;
                        lastRandom = lcgRandom(hash((uint)(cubeX + Seed), (uint)(cubeY), (uint)(cubeZ)));
                        numberFeaturePoints = probLookup(lastRandom);
                        for (uint l = 0; l < numberFeaturePoints; ++l)
                        {
                            lastRandom = lcgRandom(lastRandom);
                            randomDiff.X = lastRandom / 0x100000000;
                            lastRandom = lcgRandom(lastRandom);
                            randomDiff.Y = lastRandom / 0x100000000;
                            lastRandom = lcgRandom(lastRandom);
                            randomDiff.Z = lastRandom / 0x100000000;
                            featurePoint = new Vector3(randomDiff.X + cubeX, randomDiff.Y + cubeY, randomDiff.Z + cubeZ);
                            insert(Distances, Distance(new Vector3(X, Y, Z), featurePoint));
                        }
                    }
                }
            }
            return Combine(Distances);
        }

        private float Combine(float[] Array)
        {
            switch (CombinationFunction)
            {
                case ENUM_CombinationFunctions.eCF_D1:
                    return Array[0];
                case ENUM_CombinationFunctions.eCF_D2MINUSD1:
                    return Array[1] - Array[0];
                case ENUM_CombinationFunctions.eCF_D3MINUSD1:
                    return Array[2] - Array[0];
                default:
                    return Array[0];
            }
        }

        private float Distance(Vector3 p1, Vector3 p2)
        {
            switch (DistanceFunction)
            {
                case ENUM_DistanceType.eCF_EUCLIDEAN2D:
                    return (float)EuclidianDistance2D(p1, p2);
                case ENUM_DistanceType.eCF_EUCLIDEAN3D:
                    return (float)EuclidianDistance3D(p1, p2);
                case ENUM_DistanceType.eCF_CHEBYSHEV2D:
                    return (float)ChebyshevDistance2D(p1, p2);
                case ENUM_DistanceType.eCF_CHEBYSHEV3D:
                    return (float)ChebyshevDistance3D(p1, p2);
                case ENUM_DistanceType.eCF_MANHATTAN2D:
                    return (float)ManhattanDistance2D(p1, p2);
                case ENUM_DistanceType.eCF_MANHATTAN3D:
                    return (float)ManhattanDistance3D(p1, p2);
                default:
                    return (float)EuclidianDistance3D(p1, p2);
            }
        }

        private float EuclidianDistance2D(Vector3 p1, Vector3 p2)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
        }

        private float EuclidianDistance3D(Vector3 p1, Vector3 p2)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y) + (p1.Z - p2.Z) * (p1.Z - p2.Z);
        }

        private float ManhattanDistance2D(Vector3 p1, Vector3 p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }

        private float ManhattanDistance3D(Vector3 p1, Vector3 p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y) + Math.Abs(p1.Z - p2.Z);
        }

        private float ChebyshevDistance2D(Vector3 p1, Vector3 p2)
        {
            Vector3 diff = p1 - p2;
            return Math.Max(Math.Abs(diff.X), Math.Abs(diff.Y));
        }

        private float ChebyshevDistance3D(Vector3 p1, Vector3 p2)
        {
            Vector3 diff = p1 - p2;
            return Math.Max(Math.Max(Math.Abs(diff.X), Math.Abs(diff.Y)), Math.Abs(diff.Z));
        }
        private static uint probLookup(uint value)
        {
            if (value < 393325350)
                return 1;
            if (value < 1022645910)
                return 2;
            if (value < 1861739990)
                return 3;
            if (value < 2700834071)
                return 4;
            if (value < 3372109335)
                return 5;
            if (value < 3819626178)
                return 6;
            if (value < 4075350088)
                return 7;
            if (value < 4203212043)
                return 8;
            return 9;
        }

        private static void insert(float[] arr, float value)
        {
            float temp;
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (value > arr[i])
                    break;
                temp = arr[i];
                arr[i] = value;
                if (i + 1 < arr.Length)
                    arr[i + 1] = temp;
            }
        }

        private static uint lcgRandom(uint lastValue)
        {
            return (uint)((1103515245u * lastValue + 12345u) % 0x100000000u);
        }

        private const uint OFFSET_BASIS = 2166136261;
        private const uint FNV_PRIME = 16777619;

        private static uint hash(uint i, uint j, uint k)
        {
            return (uint)((((((OFFSET_BASIS ^ (uint)i) * FNV_PRIME) ^ (uint)j) * FNV_PRIME) ^ (uint)k) * FNV_PRIME);
        }

        private static uint hash2d(uint i, uint j)
        {
            return (uint)((((OFFSET_BASIS ^ (uint)i) * FNV_PRIME) ^ (uint)j) * FNV_PRIME);
        }
    }
}