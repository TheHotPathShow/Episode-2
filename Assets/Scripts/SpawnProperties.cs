using Unity.Entities;
using Unity.Mathematics;

namespace TMG.Live
{
    public struct SpawnProperties : IComponentData
    {
        public int2 SpawnDimensions;
        public float3 SpawnOffset;
        public float SpawnSpacing;
        public Entity Prefab;
        public Entity Parent;
        public bool ShouldParent;
        public bool AddLinkedEntityGroup;
    }
}