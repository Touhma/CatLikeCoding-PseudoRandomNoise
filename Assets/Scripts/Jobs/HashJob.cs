using _Utils;
using _Utils.Extensions;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Jobs
{
    [BurstCompile(FloatPrecision.Standard, FloatMode.Fast, CompileSynchronously = true)]
    public struct HashJob : IJobFor
    {
        [ReadOnly] public NativeArray<float3x4> positions;
        [WriteOnly] public NativeArray<uint4> hashes;

        public SmallXXHash4 hash;

        public float3x4 domainTRS;

        public void Execute(int i) {
            float4x3 p = domainTRS.TransformVectors(transpose(positions[i]));

            int4 u = (int4)floor(p.c0);
            int4 v = (int4)floor(p.c1);
            int4 w = (int4)floor(p.c2);

            hashes[i] = hash.Eat(u).Eat(v).Eat(w);
        }
    }
}