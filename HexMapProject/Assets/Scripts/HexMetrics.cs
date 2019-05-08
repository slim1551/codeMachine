using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexMetrics {
    // 网格块大小
    public const int chunkSizeX = 5, chunkSizeZ = 5;

    // 六边形外径
    public const float outerRadius = 10f;
    // 六边形内径
    public const float innerRadius = outerRadius * 0.866025404f;

    // 固定区域占比
    public const float solidFactor = 0.8f;
    // 混合区域占比
    public const float blendFactor = 1f - solidFactor;

    // 海拔步进基础单位
    public const float elevationStep = 3f;
    // 斜坡插入梯形数量
    public const int terracesPerSlope = 2;
    // 斜坡分成部分的数量
    public const int terracesStep = terracesPerSlope * 2 + 1;
    // 水平插值步长
    public const float horizontalTerracesStepSize = 1f / terracesStep;
    // 竖直插值步长
    public const float verticalTerracesStepSize = 1f / (terracesPerSlope + 1);

    // 噪声贴图
    public static Texture2D noiseSource;
    // 扰动强度预设值
    public const float cellPerturbStrength = 4f;
    // 海拔扰动强度
    public const float elevationPerturbStrength = 1.5f;
    // 噪声强度
    public const float noiseScale = 0.003f;

    /// <summary>
    /// 六边形顶点坐标
    /// </summary>
    private static Vector3[] corners =
    {
        new Vector3(0f,0f,outerRadius),
        new Vector3(innerRadius,0f,0.5f*outerRadius),
        new Vector3(innerRadius,0f,-0.5f*outerRadius),
        new Vector3(0f,0f,-outerRadius),
        new Vector3(-innerRadius,0f,-0.5f*outerRadius),
        new Vector3(-innerRadius,0f,0.5f*outerRadius),
        new Vector3(0f,0f,outerRadius)
    };

    /// <summary>
    /// 梯形插值方法
    /// </summary>
    public static Vector3 TerraceLerp(Vector3 a, Vector3 b, int step)
    {
        float h = step * horizontalTerracesStepSize;
        a.x += (b.x - a.x) * h;
        a.z += (b.z - a.z) * h;
        float v = (step + 1) / 2 * verticalTerracesStepSize;
        a.y += (b.y - a.y) * v;
        return a;
    }
    /// <summary>
    /// 梯形颜色插值方法，仅当连接处是平的时候计算
    /// </summary>
    public static Color TerraceLerp(Color a, Color b, int step)
    {
        float h = step * horizontalTerracesStepSize;
        return Color.Lerp(a, b, h);
    }
    
    public static Vector3 GetFirstCorner(HexDirection direction)
    {
        return corners[(int)direction];
    }
    public static Vector3 GetSecondCorner(HexDirection direction)
    {
        return corners[(int)direction + 1];
    }

    public static Vector3 GetFirstSolidCorner(HexDirection direction)
    {
        return corners[(int)direction] * solidFactor;
    }
    public static Vector3 GetSecondSolidCorner(HexDirection direction)
    {
        return corners[(int)direction + 1] * solidFactor;
    }

    /// <summary>
    /// 获得矩形连接桥
    /// </summary>
    public static Vector3 GetBridge(HexDirection direction)
    {
        return (corners[(int)direction] + corners[(int)direction + 1]) * blendFactor;
    }

    public static HexEdgeType GetEdgeType(int elevation1, int elevation2)
    {
        if(elevation1 == elevation2)
        {
            return HexEdgeType.Flat;
        }
        int delta = elevation2 - elevation1;
        if (delta == -1 || delta == 1)
        {
            return HexEdgeType.Slope;
        }
        return HexEdgeType.Cliff;
    }

    /// <summary>
    /// 生成包含四种噪声模式的四维向量
    /// </summary>
    public static Vector4 SampleNoise(Vector3 position)
    {
        return noiseSource.GetPixelBilinear(
            position.x * noiseScale, 
            position.z * noiseScale
            );
    }




}
