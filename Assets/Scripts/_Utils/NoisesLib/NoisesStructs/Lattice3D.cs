using _Utils.Interfaces;
using _Utils.Structs.NoisesStructs;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace _Utils.NoisesLib.NoisesStructs
{
    public struct Lattice3D<G> : INoise where G : struct, IGradient
    {
        public float4 GetNoise4(float4x3 positions, SmallXXHash4 hash)
        {
            LatticeSpan4 x = Noises.GetLatticeSpan4(positions.c0), y = Noises.GetLatticeSpan4(positions.c1), z = Noises.GetLatticeSpan4(positions.c2);

            SmallXXHash4
                h0 = hash.Eat(x.p0),
                h1 = hash.Eat(x.p1),
                h00 = h0.Eat(y.p0),
                h01 = h0.Eat(y.p1),
                h10 = h1.Eat(y.p0),
                h11 = h1.Eat(y.p1);

            G g = default;

            return g.EvaluateAfterInterpolation(lerp(
                lerp(
                    lerp(g.Evaluate(h00.Eat(z.p0), x.g0, y.g0, z.g0), g.Evaluate(h00.Eat(z.p1), x.g0, y.g0, z.g1), z.t),
                    lerp(g.Evaluate(h01.Eat(z.p0), x.g0, y.g1, z.g0), g.Evaluate(h01.Eat(z.p1), x.g0, y.g1, z.g1), z.t),
                    y.t
                ),
                lerp(
                    lerp(g.Evaluate(h10.Eat(z.p0), x.g1, y.g0, z.g0), g.Evaluate(h10.Eat(z.p1), x.g1, y.g0, z.g1), z.t),
                    lerp(g.Evaluate(h11.Eat(z.p0), x.g1, y.g1, z.g0), g.Evaluate(h11.Eat(z.p1), x.g1, y.g1, z.g1), z.t),
                    y.t
                ),
                x.t
            ));
        }
    }
}