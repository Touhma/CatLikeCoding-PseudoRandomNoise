using _Utils.NoisesLib.NoisesStructs;
using _Utils.Structs.NoisesStructs;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

using static Unity.Mathematics.math;

namespace _Utils
{
    public static class Noises {
        public static LatticeSpan4 GetLatticeSpan4 (float4 coordinates) {
            float4 points = floor(coordinates);
            LatticeSpan4 span;
            span.p0 = (int4)points;
            span.p1 = span.p0 + 1;
            span.g0 = coordinates - span.p0;
            span.g1 = span.g0 - 1f;
            span.t = coordinates - points;
            span.t = span.t * span.t * span.t * (span.t * (span.t * 6f - 15f) + 10f);
         
            return span;
        }
        
        public delegate JobHandle ScheduleDelegate (
            NativeArray<float3x4> positions, NativeArray<float4> noise,
            NoiseSettings settings, SpaceTRS domainTRS, int resolution, JobHandle dependency
        );
    }
}