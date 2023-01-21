using _Utils;
using _Utils.NoisesLib.NoisesStructs;
using _Utils.Structs.NoiseStruct;
using Jobs;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class NoiseVisualization : Visualization
{
    private static Noises.ScheduleDelegate[,] noiseJobs =
    {
        {
            NoiseJob<Lattice1D<LatticeNormal, NoisePerlin>>.ScheduleParallel,
            NoiseJob<Lattice1D<LatticeTiling, NoisePerlin>>.ScheduleParallel,
            NoiseJob<Lattice2D<LatticeNormal, NoisePerlin>>.ScheduleParallel,
            NoiseJob<Lattice2D<LatticeTiling, NoisePerlin>>.ScheduleParallel,
            NoiseJob<Lattice3D<LatticeNormal, NoisePerlin>>.ScheduleParallel,
            NoiseJob<Lattice3D<LatticeTiling, NoisePerlin>>.ScheduleParallel
        },
        {
            NoiseJob<Lattice1D<LatticeNormal, NoiseTurbulence<NoisePerlin>>>.ScheduleParallel,
            NoiseJob<Lattice1D<LatticeTiling, NoiseTurbulence<NoisePerlin>>>.ScheduleParallel,
            NoiseJob<Lattice2D<LatticeNormal, NoiseTurbulence<NoisePerlin>>>.ScheduleParallel,
            NoiseJob<Lattice2D<LatticeTiling, NoiseTurbulence<NoisePerlin>>>.ScheduleParallel,
            NoiseJob<Lattice3D<LatticeNormal, NoiseTurbulence<NoisePerlin>>>.ScheduleParallel,
            NoiseJob<Lattice3D<LatticeTiling, NoiseTurbulence<NoisePerlin>>>.ScheduleParallel
        },
        {
            NoiseJob<Lattice1D<LatticeNormal, NoiseGradient>>.ScheduleParallel,
            NoiseJob<Lattice1D<LatticeTiling, NoiseGradient>>.ScheduleParallel,
            NoiseJob<Lattice2D<LatticeNormal, NoiseGradient>>.ScheduleParallel,
            NoiseJob<Lattice2D<LatticeTiling, NoiseGradient>>.ScheduleParallel,
            NoiseJob<Lattice3D<LatticeNormal, NoiseGradient>>.ScheduleParallel,
            NoiseJob<Lattice3D<LatticeTiling, NoiseGradient>>.ScheduleParallel
        },
        {
            NoiseJob<Lattice1D<LatticeNormal, NoiseTurbulence<NoiseGradient>>>.ScheduleParallel,
            NoiseJob<Lattice1D<LatticeTiling, NoiseTurbulence<NoiseGradient>>>.ScheduleParallel,
            NoiseJob<Lattice2D<LatticeNormal, NoiseTurbulence<NoiseGradient>>>.ScheduleParallel,
            NoiseJob<Lattice2D<LatticeTiling, NoiseTurbulence<NoiseGradient>>>.ScheduleParallel,
            NoiseJob<Lattice3D<LatticeNormal, NoiseTurbulence<NoiseGradient>>>.ScheduleParallel,
            NoiseJob<Lattice3D<LatticeTiling, NoiseTurbulence<NoiseGradient>>>.ScheduleParallel
        },
        {
            NoiseJob<Voronoi1D<LatticeNormal, Worley, F1>>.ScheduleParallel,
            NoiseJob<Voronoi1D<LatticeTiling, Worley, F1>>.ScheduleParallel,
            NoiseJob<Voronoi2D<LatticeNormal, Worley, F1>>.ScheduleParallel,
            NoiseJob<Voronoi2D<LatticeTiling, Worley, F1>>.ScheduleParallel,
            NoiseJob<Voronoi3D<LatticeNormal, Worley, F1>>.ScheduleParallel,
            NoiseJob<Voronoi3D<LatticeTiling, Worley, F1>>.ScheduleParallel
        },
        {
            NoiseJob<Voronoi1D<LatticeNormal, Worley, F2>>.ScheduleParallel,
            NoiseJob<Voronoi1D<LatticeTiling, Worley, F2>>.ScheduleParallel,
            NoiseJob<Voronoi2D<LatticeNormal, Worley, F2>>.ScheduleParallel,
            NoiseJob<Voronoi2D<LatticeTiling, Worley, F2>>.ScheduleParallel,
            NoiseJob<Voronoi3D<LatticeNormal, Worley, F2>>.ScheduleParallel,
            NoiseJob<Voronoi3D<LatticeTiling, Worley, F2>>.ScheduleParallel
        },
        {
            NoiseJob<Voronoi1D<LatticeNormal, Worley, F2MinusF1>>.ScheduleParallel,
            NoiseJob<Voronoi1D<LatticeTiling, Worley, F2MinusF1>>.ScheduleParallel,
            NoiseJob<Voronoi2D<LatticeNormal, Worley, F2MinusF1>>.ScheduleParallel,
            NoiseJob<Voronoi2D<LatticeTiling, Worley, F2MinusF1>>.ScheduleParallel,
            NoiseJob<Voronoi3D<LatticeNormal, Worley, F2MinusF1>>.ScheduleParallel,
            NoiseJob<Voronoi3D<LatticeTiling, Worley, F2MinusF1>>.ScheduleParallel
        },
        {
            NoiseJob<Voronoi1D<LatticeNormal, Worley, F1>>.ScheduleParallel,
            NoiseJob<Voronoi1D<LatticeTiling, Worley, F1>>.ScheduleParallel,
            NoiseJob<Voronoi2D<LatticeNormal, Chebyshev, F1>>.ScheduleParallel,
            NoiseJob<Voronoi2D<LatticeTiling, Chebyshev, F1>>.ScheduleParallel,
            NoiseJob<Voronoi3D<LatticeNormal, Chebyshev, F1>>.ScheduleParallel,
            NoiseJob<Voronoi3D<LatticeTiling, Chebyshev, F1>>.ScheduleParallel
        },
        {
            NoiseJob<Voronoi1D<LatticeNormal, Worley, F2>>.ScheduleParallel,
            NoiseJob<Voronoi1D<LatticeTiling, Worley, F2>>.ScheduleParallel,
            NoiseJob<Voronoi2D<LatticeNormal, Chebyshev, F2>>.ScheduleParallel,
            NoiseJob<Voronoi2D<LatticeTiling, Chebyshev, F2>>.ScheduleParallel,
            NoiseJob<Voronoi3D<LatticeNormal, Chebyshev, F2>>.ScheduleParallel,
            NoiseJob<Voronoi3D<LatticeTiling, Chebyshev, F2>>.ScheduleParallel
        },
        {
            NoiseJob<Voronoi1D<LatticeNormal, Worley, F2MinusF1>>.ScheduleParallel,
            NoiseJob<Voronoi1D<LatticeTiling, Worley, F2MinusF1>>.ScheduleParallel,
            NoiseJob<Voronoi2D<LatticeNormal, Chebyshev, F2MinusF1>>.ScheduleParallel,
            NoiseJob<Voronoi2D<LatticeTiling, Chebyshev, F2MinusF1>>.ScheduleParallel,
            NoiseJob<Voronoi3D<LatticeNormal, Chebyshev, F2MinusF1>>.ScheduleParallel,
            NoiseJob<Voronoi3D<LatticeTiling, Chebyshev, F2MinusF1>>.ScheduleParallel
        }
    };

    private static int noiseId = Shader.PropertyToID("_Noise");

    [SerializeField] private NoiseSettings noiseSettings = NoiseSettings.Default;

    private enum NoiseType
    {
        Perlin,
        PerlinTurbulence,
        Value,
        ValueTurbulence,
        VoronoiWorleyF1,
        VoronoiWorleyF2,
        VoronoiWorleyF2MinusF1,
        VoronoiChebyshevF1,
        VoronoiChebyshevF2,
        VoronoiChebyshevF2MinusF1
    }

    [SerializeField] private NoiseType type;

    [SerializeField, Range(1, 3)] private int dimensions = 3;

    [SerializeField] private bool tiling;

    [SerializeField] private SpaceTRS domain = new SpaceTRS
    {
        scale = 8f
    };

    private NativeArray<float4> noise;

    private ComputeBuffer noiseBuffer;

    protected override void EnableVisualization(
        int dataLength, MaterialPropertyBlock propertyBlock
    )
    {
        noise = new NativeArray<float4>(dataLength, Allocator.Persistent);
        noiseBuffer = new ComputeBuffer(dataLength * 4, 4);
        propertyBlock.SetBuffer(noiseId, noiseBuffer);
    }

    protected override void DisableVisualization()
    {
        noise.Dispose();
        noiseBuffer.Release();
        noiseBuffer = null;
    }

    protected override void UpdateVisualization(
        NativeArray<float3x4> positions, int resolution, JobHandle handle
    )
    {
        noiseJobs[(int)type, 2 * dimensions - (tiling ? 1 : 2)](
            positions, noise, noiseSettings, domain, resolution, handle
        ).Complete();
        noiseBuffer.SetData(noise.Reinterpret<float>(4 * 4));
    }
}