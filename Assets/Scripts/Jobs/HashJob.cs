using _Utils;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

using static Unity.Mathematics.math;

namespace Jobs
{
    [BurstCompile(FloatPrecision.Standard, FloatMode.Fast, CompileSynchronously = true)]
    public struct HashJob: IJobFor {

        [WriteOnly]
        public NativeArray<uint> hashes;
        
        public int resolution;

        public float invResolution;
        
        public SmallXXHash hash;
        
        public void Execute(int i) {
            int v = (int)floor(invResolution * i + 0.00001f);
            int u = i - resolution * v - resolution / 2;
            v -= resolution / 2;

            hashes[i] = hash.Eat(u).Eat(v);
        }
    }
}