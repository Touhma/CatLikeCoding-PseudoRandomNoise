using _Utils;
using _Utils.Interfaces;
using _Utils.Structs.ShapesStructs;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Jobs
{
    [BurstCompile(FloatPrecision.Standard, FloatMode.Fast, CompileSynchronously = true)]
    public struct ShapeJob<S> : IJobFor where S : struct, IShape
    {
        [WriteOnly] NativeArray<float3x4> positions, normals;

        public float resolution, invResolution;

        public float3x4 positionTRS, normalTRS;

        float4x3 TransformVectors(float3x4 trs, float4x3 p, float w = 1f) => float4x3(
            trs.c0.x * p.c0 + trs.c1.x * p.c1 + trs.c2.x * p.c2 + trs.c3.x * w,
            trs.c0.y * p.c0 + trs.c1.y * p.c1 + trs.c2.y * p.c2 + trs.c3.y * w,
            trs.c0.z * p.c0 + trs.c1.z * p.c1 + trs.c2.z * p.c2 + trs.c3.z * w
        );

        public void Execute(int i)
        {
            Point4 p = default(S).GetPoint4(i, resolution, invResolution);

            positions[i] = transpose(TransformVectors(positionTRS, p.positions));
            float3x4 n = transpose(TransformVectors(normalTRS, p.normals, 0f));
            normals[i] = float3x4(normalize(n.c0), normalize(n.c1), normalize(n.c2), normalize(n.c3));
        }

        public static JobHandle ScheduleParallel(
            NativeArray<float3x4> positions, NativeArray<float3x4> normals, int resolution, float4x4 trs, JobHandle dependency
        )
        {
            float4x4 tim = transpose(inverse(trs));
            
            return new ShapeJob<S>
            {
                positions = positions,
                normals = normals,
                resolution = resolution,
                invResolution = 1f / resolution,
                positionTRS = float3x4(trs.c0.xyz, trs.c1.xyz, trs.c2.xyz, trs.c3.xyz),
                normalTRS = float3x4(tim.c0.xyz, tim.c1.xyz, tim.c2.xyz, tim.c3.xyz)
            }.ScheduleParallel(positions.Length, resolution, dependency);
        }
    }
}