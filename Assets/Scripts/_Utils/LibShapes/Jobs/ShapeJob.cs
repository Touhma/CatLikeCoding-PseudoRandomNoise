using _Utils;
using _Utils.Extensions;
using _Utils.Interfaces;
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

        public void Execute(int i)
        {
            Point4 p = default(S).GetPoint4(i, resolution, invResolution);

            positions[i] = transpose(positionTRS.TransformVectors(p.positions));
            float3x4 n = transpose(normalTRS.TransformVectors( p.normals, 0f));
            normals[i] = float3x4(normalize(n.c0), normalize(n.c1), normalize(n.c2), normalize(n.c3));
        }

        public static JobHandle ScheduleParallel(NativeArray<float3x4> positions, NativeArray<float3x4> normals, int resolution, float4x4 trs, JobHandle dependency)
        {
            float4x4 tim = transpose(inverse(trs));
            
            return new ShapeJob<S>
            {
                positions = positions,
                normals = normals,
                resolution = resolution,
                invResolution = 1f / resolution,
                positionTRS = trs.Get3x4(),
                normalTRS = transpose(inverse(trs)).Get3x4()
            }.ScheduleParallel(positions.Length, resolution, dependency);
        }
    }
}