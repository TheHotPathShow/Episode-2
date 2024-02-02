using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.Live
{
    public partial struct RandomMoveSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            state.Dependency = new RandomMoveJob { DeltaTime = deltaTime }.ScheduleParallel(state.Dependency);
        }
    }
    
    [BurstCompile]
    public partial struct RandomMoveJob : IJobEntity
    {
        [ReadOnly] public float DeltaTime;

        private void Execute(ref LocalTransform transform, ref RandomMoveProperties moveProperties, ref EntityRandom random)
        {
            moveProperties.Timer -= DeltaTime;
            if (moveProperties.Timer <= 0f)
            {
                moveProperties.Timer = random.Random.NextFloat(moveProperties.MinMaxTimer.x, moveProperties.MinMaxTimer.y);
                moveProperties.Direction = random.Random.NextFloat(360f);
                moveProperties.Speed = random.Random.NextFloat(moveProperties.MinMaxSpeed.x, moveProperties.MinMaxSpeed.y);
                transform.Rotation = quaternion.Euler(0f, moveProperties.Direction, 0f);
            }

            transform.Position += transform.Forward() * moveProperties.Speed * DeltaTime;
        }
    }
}