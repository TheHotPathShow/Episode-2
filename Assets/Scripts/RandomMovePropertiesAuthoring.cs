using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace TMG.Live
{
    public class RandomMovePropertiesAuthoring : MonoBehaviour
    {
        public float2 MinMaxTimer;
        public float2 MinMaxSpeed;

        public class RandomMovePropertiesBaker : Baker<RandomMovePropertiesAuthoring>
        {
            public override void Bake(RandomMovePropertiesAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new RandomMoveProperties
                {
                    MinMaxTimer = authoring.MinMaxTimer,
                    MinMaxSpeed = authoring.MinMaxSpeed
                });
            }
        }
    }
}