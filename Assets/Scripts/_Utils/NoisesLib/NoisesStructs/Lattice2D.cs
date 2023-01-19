using _Utils.Interfaces;
using _Utils.Structs.NoisesStructs;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace _Utils.NoisesLib.NoisesStructs
{
    public struct Lattice2D : INoise {

        public float4 GetNoise4(float4x3 positions, SmallXXHash4 hash) {
            LatticeSpan4 x = Noises.GetLatticeSpan4(positions.c0), z = Noises.GetLatticeSpan4(positions.c2);
            
            SmallXXHash4 h0 = hash.Eat(x.p0), h1 = hash.Eat(x.p1);
			
            return lerp(
                lerp(h0.Eat(z.p0).Floats01A, h0.Eat(z.p1).Floats01A, z.t),
                lerp(h1.Eat(z.p0).Floats01A, h1.Eat(z.p1).Floats01A, z.t),
                x.t
            ) * 2f - 1f;
        }
    }
}