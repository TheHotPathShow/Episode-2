using Unity.Entities;
using UnityEngine;

namespace TMG.Live
{
    public class ParentTagAuthoring : MonoBehaviour
    {
        public class ParentTagBaker : Baker<ParentTagAuthoring>
        {
            public override void Bake(ParentTagAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent<ParentTag>(entity);
            }
        }
    }
}