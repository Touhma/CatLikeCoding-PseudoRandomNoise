using _Utils.NoisesLib.NoisesStructs;
using _Utils.Structs.NoisesStructs;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

using static Unity.Mathematics.math;

namespace _Utils
{
    public static class Noises {
        
        
        public delegate JobHandle ScheduleDelegate (
            NativeArray<float3x4> positions, NativeArray<float4> noise,
            NoiseSettings settings, SpaceTRS domainTRS, int resolution, JobHandle dependency
        );
    }
}