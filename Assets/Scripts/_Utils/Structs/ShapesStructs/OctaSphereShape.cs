using _Utils.Interfaces;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace _Utils.Structs.ShapesStructs
{
    public struct OctaSphereShape : IShape
    {
        public Point4 GetPoint4(int i, float resolution, float invResolution)
        {
            float4x2 uv = Shapes.IndexTo4UV(i, resolution, invResolution);

            Point4 p;
            p.positions.c0 = uv.c0 - 0.5f;
            p.positions.c1 = uv.c1 - 0.5f;
            p.positions.c2 = 0.5f - abs(p.positions.c0) - abs(p.positions.c1);
            
            float4 offset = max(-p.positions.c2, 0f);
            
            p.positions.c0 += select(-offset, offset, p.positions.c0 < 0f);
            p.positions.c1 += select(-offset, offset, p.positions.c1 < 0f);
            
            float4 scale = 0.5f * rsqrt(
                p.positions.c0 * p.positions.c0 +
                p.positions.c1 * p.positions.c1 +
                p.positions.c2 * p.positions.c2
            );
            p.positions.c0 *= scale;
            p.positions.c1 *= scale;
            p.positions.c2 *= scale;
            
            p.normals = p.positions;
            return p;
        }
    }
}