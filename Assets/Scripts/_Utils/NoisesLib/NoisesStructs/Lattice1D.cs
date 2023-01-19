using _Utils.Interfaces;
using _Utils.Structs.NoisesStructs;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace _Utils.NoisesLib.NoisesStructs
{
    public struct Lattice1D : INoise {

        public float4 GetNoise4(float4x3 positions, SmallXXHash4 hash) {
            LatticeSpan4 x = Noises.GetLatticeSpan4(positions.c0);
            return lerp(
                hash.Eat(x.p0).Floats01A, hash.Eat(x.p1).Floats01A, x.t
            ) * 2f - 1f;
        }
    }
}