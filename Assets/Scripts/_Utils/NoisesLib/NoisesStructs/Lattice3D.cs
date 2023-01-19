using _Utils.Interfaces;
using _Utils.Structs.NoisesStructs;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace _Utils.NoisesLib.NoisesStructs
{
    public struct Lattice3D : INoise {

        public float4 GetNoise4(float4x3 positions, SmallXXHash4 hash) {
            LatticeSpan4 x = Noises.GetLatticeSpan4(positions.c0), y = Noises.GetLatticeSpan4(positions.c1), z = Noises.GetLatticeSpan4(positions.c2);
            
            SmallXXHash4 
                h0 = hash.Eat(x.p0), 
                h1 = hash.Eat(x.p1),
                h00 = h0.Eat(y.p0), 
                h01 = h0.Eat(y.p1),
                h10 = h1.Eat(y.p0), 
                h11 = h1.Eat(y.p1);
			
            return lerp(
                lerp(
                    lerp(h00.Eat(z.p0).Floats01A, h00.Eat(z.p1).Floats01A, z.t),
                    lerp(h01.Eat(z.p0).Floats01A, h01.Eat(z.p1).Floats01A, z.t),
                    y.t
                ),
                lerp(
                    lerp(h10.Eat(z.p0).Floats01A, h10.Eat(z.p1).Floats01A, z.t),
                    lerp(h11.Eat(z.p0).Floats01A, h11.Eat(z.p1).Floats01A, z.t),
                    y.t
                ),
                x.t
            ) * 2f - 1f;
        }
    }
}