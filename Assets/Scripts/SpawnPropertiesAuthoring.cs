using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace TMG.Live
{
    public class SpawnPropertiesAuthoring : MonoBehaviour
    {
        public int2 SpawnDimensions;
        public Vector3 SpawnOffset;
        public float SpawnSpacing;
        public GameObject Prefab;
        public GameObject Parent;
        public bool ShouldParent;
        public bool AddLEG;
        
        public class SpawnPropertiesBaker : Baker<SpawnPropertiesAuthoring>
        {
            public override void Bake(SpawnPropertiesAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new SpawnProperties
                {
                    SpawnDimensions = authoring.SpawnDimensions,
                    SpawnOffset = authoring.SpawnOffset,
                    SpawnSpacing = authoring.SpawnSpacing,
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    Parent = GetEntity(authoring.Parent, TransformUsageFlags.Dynamic),
                    ShouldParent = authoring.ShouldParent,
                    AddLinkedEntityGroup = authoring.AddLEG
                });
            }
        }
    }
}