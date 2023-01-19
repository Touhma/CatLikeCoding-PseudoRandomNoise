using _Utils;
using _Utils.Extensions;
using _Utils.Interfaces;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using static Unity.Mathematics.math;


namespace Jobs
{

    [BurstCompile(FloatPrecision.Standard, FloatMode.Fast, CompileSynchronously = true)]
    public struct NoiseJob<N> : IJobFor where N : struct, INoise
    {
        [ReadOnly]
        public NativeArray<float3x4> positions;

        [WriteOnly]
        public NativeArray<float4> noise;

        public SmallXXHash4 hash;

        public float3x4 domainTRS;

        public void Execute (int i) {
            noise[i] = default(N).GetNoise4(
                domainTRS.TransformVectors(transpose(positions[i])), hash
            );
        }
        
        public static JobHandle ScheduleParallel (
            NativeArray<float3x4> positions, NativeArray<float4> noise,
            int seed, SpaceTRS domainTRS, int resolution, JobHandle dependency
        ) => new NoiseJob<N> {
            positions = positions,
            noise = noise,
            hash = SmallXXHash.Seed(seed),
            domainTRS = domainTRS.Matrix,
        }.ScheduleParallel(positions.Length, resolution, dependency);
        
        
    }
}