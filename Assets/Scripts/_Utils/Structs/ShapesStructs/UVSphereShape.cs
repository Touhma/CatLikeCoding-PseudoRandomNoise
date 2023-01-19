using _Utils.Interfaces;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace _Utils.Structs.ShapesStructs
{
    public struct UVSphereShape : IShape
    {
        public Point4 GetPoint4(int i, float resolution, float invResolution)
        {
            float4x2 uv = Shapes.IndexTo4UV(i, resolution, invResolution);

            float r = 0.5f;
            float4 s = r * sin(PI * uv.c1);

            Point4 p;
            p.positions.c0 = s * sin(2f * PI * uv.c0);
            p.positions.c1 = r * cos(PI * uv.c1);
            p.positions.c2 = s * cos(2f * PI * uv.c0);
            p.normals = p.positions;
            return p;
        }
    }
}