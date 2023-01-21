using _Utils.Interfaces;
using Unity.Mathematics;
using static Unity.Mathematics.math;
using static _Utils.Shapes;
namespace _Utils.Structs.ShapesStructs
{
    
    public struct ShapeTorus : IShape {
        public Point4 GetPoint4 (int i, float resolution, float invResolution) {
            float4x2 uv = IndexTo4UV(i, resolution, invResolution);

            float r1 = 0.375f;
            float r2 = 0.125f;
            float4 s = r1 + r2 * cos(2f * PI * uv.c1);

            Point4 p;
            p.positions.c0 = s * sin(2f * PI * uv.c0);
            p.positions.c1 = r2 * sin(2f * PI * uv.c1);
            p.positions.c2 = s * cos(2f * PI * uv.c0);
            p.normals = p.positions;
            p.normals.c0 -= r1 * sin(2f * PI * uv.c0);
            p.normals.c2 -= r1 * cos(2f * PI * uv.c0);
            return p;
        }
    }
}