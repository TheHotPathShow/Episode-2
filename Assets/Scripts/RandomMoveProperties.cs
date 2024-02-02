using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace TMG.Live
{
    public struct RandomMoveProperties : IComponentData
    {
        public float Timer;
        public float2 MinMaxTimer;
        public float Speed;
        public float2 MinMaxSpeed;
        public float Direction;
    }

    public struct EntityRandom : IComponentData
    {
        public Random Random;
    }
}