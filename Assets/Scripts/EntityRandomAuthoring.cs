using Unity.Entities;
using UnityEngine;

namespace TMG.Live
{
    public class EntityRandomAuthoring : MonoBehaviour
    {
        public uint RandomIndex;

        public class EntityRandomBaker : Baker<EntityRandomAuthoring>
        {
            public override void Bake(EntityRandomAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new EntityRandom
                {
                    Random = Unity.Mathematics.Random.CreateFromIndex(authoring.RandomIndex)
                });
            }
        }
    }
}