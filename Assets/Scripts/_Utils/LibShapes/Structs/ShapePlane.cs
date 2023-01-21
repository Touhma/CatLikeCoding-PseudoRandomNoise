using _Utils.Interfaces;
using Unity.Mathematics;

using static Unity.Mathematics.math;
using static _Utils.Shapes;

namespace _Utils.Structs.ShapesStructs
{
    public struct ShapePlane : IShape
    {
        public Point4 GetPoint4 (int i, float resolution, float invResolution) {
            float4x2 uv = IndexTo4UV(i, resolution, invResolution);
            return new Point4 {
                positions = float4x3(uv.c0 - 0.5f, 0f, uv.c1 - 0.5f),
                normals = float4x3(0f, 1f, 0f)
            };
        }
    }
}