using _Utils;
using Jobs;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace MonoBehaviours
{
    public class HashVisualization : MonoBehaviour
    {
        private static int
            hashesId = Shader.PropertyToID("_Hashes"),
            configId = Shader.PropertyToID("_Config");

        [SerializeField] private Mesh instanceMesh;

        [SerializeField] private Material material;

        [SerializeField, Range(1, 512)] private int resolution = 16;
        
        [SerializeField] private int seed;
        
        [SerializeField, Range(-2f, 2f)] private float verticalOffset = 1f;

        private NativeArray<uint> hashes;

        private ComputeBuffer hashesBuffer;

        private MaterialPropertyBlock propertyBlock;

        private void OnEnable()
        {
            Debug.Log("OnEnable");
            int length = resolution * resolution;
            hashes = new NativeArray<uint>(length, Allocator.Persistent);
            hashesBuffer = new ComputeBuffer(length, 4);

            new HashJob
            {
                hashes = hashes,
                resolution = resolution,
                invResolution = 1f / resolution,
                hash = SmallXXHash.Seed(seed)
            }.ScheduleParallel(hashes.Length, resolution, default).Complete();

            hashesBuffer.SetData(hashes);

            propertyBlock ??= new MaterialPropertyBlock();
            propertyBlock.SetBuffer(hashesId, hashesBuffer);
            propertyBlock.SetVector(configId, new Vector4(resolution, 1f / resolution, verticalOffset / resolution));
        }

        private void OnDisable()
        {
            Debug.Log("OnDisable");
            hashes.Dispose();
            hashesBuffer.Release();
            hashesBuffer = null;
        }
        
        void Update () {
            Graphics.DrawMeshInstancedProcedural(
                instanceMesh, 0, material, new Bounds(Vector3.zero, Vector3.one),
                hashes.Length, propertyBlock
            );
        }

        private void OnValidate()
        {
            Debug.Log("OnValidate");
            if (hashesBuffer != null && enabled)
            {
                OnDisable();
                OnEnable();
            }
        }
    }
}