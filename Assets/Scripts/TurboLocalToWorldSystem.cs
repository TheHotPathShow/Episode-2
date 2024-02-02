using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.Live
{
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct TurboLocalToWorldSystem : ISystem
    {
        private float4x4 _f4x4;

        public void OnCreate(ref SystemState state)
        {
            var quaternionIdentity = new float3x3(quaternion.identity);
            _f4x4 = new float4x4(new float4(quaternionIdentity.c0, 0f),
                new float4(quaternionIdentity.c1, 0f),
                new float4(quaternionIdentity.c2, 0f),
                new float4(float3.zero, 1f));
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Dependency = new TurboLocalToWorldJob
            {
                F4X4 = _f4x4
            }.ScheduleParallel(state.Dependency);
        }
    }
    
    [BurstCompile]
    [WithAll(typeof(TurboLocalToWorldTag))]
    public partial struct TurboLocalToWorldJob : IJobEntity
    {
        [ReadOnly] public float4x4 F4X4;
        private void Execute(ref LocalToWorld localToWorld, in LocalTransform transform)
        {
            localToWorld.Value = F4X4;
            localToWorld.Value.c3 = new float4(transform.Position, 1f);
        }
    }
}