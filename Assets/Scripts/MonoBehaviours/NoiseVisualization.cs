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
	static Noises.ScheduleDelegate[,] noiseJobs =
	{
		{
			NoiseJob<Lattice1D<PerlinNoise>>.ScheduleParallel,
			NoiseJob<Lattice2D<PerlinNoise>>.ScheduleParallel,
			NoiseJob<Lattice3D<PerlinNoise>>.ScheduleParallel
		},
		{
			NoiseJob<Lattice1D<GradientNoise>>.ScheduleParallel,
			NoiseJob<Lattice2D<GradientNoise>>.ScheduleParallel,
			NoiseJob<Lattice3D<GradientNoise>>.ScheduleParallel
		}
	};
	
	private static int noiseId  = Shader.PropertyToID("_Noise");

	private enum NoiseType { Perlin, Value }

	[SerializeField] NoiseType type;
	
	[SerializeField]
	NoiseSettings noiseSettings = NoiseSettings.Default;

	[SerializeField]
	SpaceTRS domain = new() {
		scale = 8f
	};
	
	[SerializeField, Range(1, 3)]
	int dimensions = 3;

	NativeArray<float4> noise;

	ComputeBuffer noiseBuffer;

	protected override void EnableVisualization (int dataLength, MaterialPropertyBlock propertyBlock) {
		noise  = new NativeArray<float4>(dataLength, Allocator.Persistent);
		noiseBuffer  = new ComputeBuffer(dataLength * 4,4);
		propertyBlock.SetBuffer(noiseId, noiseBuffer);
	}

	protected override void DisableVisualization () {
		noise .Dispose();
		noiseBuffer .Release();
		noiseBuffer  = null;
	}
	
	protected override void UpdateVisualization (
		NativeArray<float3x4> positions, int resolution, JobHandle handle
	) {
		noiseJobs[(int)type, dimensions - 1](
			positions, noise, noiseSettings, domain, resolution, handle
		).Complete();
		noiseBuffer.SetData(noise.Reinterpret<float>(4 * 4));
	}
}