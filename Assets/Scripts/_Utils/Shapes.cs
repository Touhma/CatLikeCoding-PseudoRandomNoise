using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace _Utils
{
    public static class Shapes {
	
        public static float4x2 IndexTo4UV (int i, float resolution, float invResolution) {
            float4x2 uv;
            float4 i4 = 4f * i + float4(0f, 1f, 2f, 3f);
            uv.c1 = floor(invResolution * i4 + 0.00001f);
            uv.c0 = invResolution * (i4 - resolution * uv.c1 + 0.5f);
            uv.c1 = invResolution * (uv.c1 + 0.5f);
            return uv;
        }
        
        public delegate JobHandle ScheduleDelegate (
            NativeArray<float3x4> positions, NativeArray<float3x4> normals,
            int resolution, float4x4 trs, JobHandle dependency
        );
    }
}