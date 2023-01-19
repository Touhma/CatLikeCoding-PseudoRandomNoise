using _Utils.Interfaces;
using Unity.Mathematics;

namespace _Utils.Structs.ShapesStructs
{
    public struct PlaneShape : IShape
    {
        
        public Point4 GetPoint4 (int i, float resolution, float invResolution) {
            float4x2 uv = Shapes.IndexTo4UV(i, resolution, invResolution);
            return new Point4 {
                positions = new float4x3(uv.c0 - 0.5f, 0f, uv.c1 - 0.5f),
                normals = new float4x3(0f, 1f, 0f)
            };
        }
    }
}