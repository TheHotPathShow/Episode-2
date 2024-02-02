using Unity.Entities;
using UnityEngine;

namespace TMG.Live
{
    public class SpawnPrefabAuthoring : MonoBehaviour
    {
        public GameObject SpawnPrefab;

        public class SpawnPrefabBaker : Baker<SpawnPrefabAuthoring>
        {
            public override void Bake(SpawnPrefabAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new SpawnPrefab
                {
                    Value = GetEntity(authoring.SpawnPrefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}