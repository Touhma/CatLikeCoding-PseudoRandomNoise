using _Utils;
using Jobs;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class HashVisualization : Visualization
{
	private static int hashesId = Shader.PropertyToID("_Hashes");

	[SerializeField]
	int seed;

	[SerializeField]
	SpaceTRS domain = new SpaceTRS {
		scale = 8f
	};

	NativeArray<uint4> hashes;

	ComputeBuffer hashesBuffer;

	protected override void EnableVisualization (int dataLength, MaterialPropertyBlock propertyBlock) {
		hashes = new NativeArray<uint4>(dataLength, Allocator.Persistent);
		hashesBuffer = new ComputeBuffer(dataLength * 4,4);
		propertyBlock.SetBuffer(hashesId, hashesBuffer);
	}

	protected override void DisableVisualization () {
		hashes.Dispose();
		hashesBuffer.Release();
		hashesBuffer = null;
	}
	
	protected override void UpdateVisualization(NativeArray<float3x4> positions, int resolution, JobHandle handle)
	{
		new HashJob {
			positions = positions,
			hashes = hashes,
			hash = SmallXXHash.Seed(seed),
			domainTRS = domain.Matrix
		}.ScheduleParallel(hashes.Length, resolution, handle).Complete();

		hashesBuffer.SetData(hashes.Reinterpret<uint>(4 * 4));
	}
}