using Unity.Entities;
using UnityEngine;

namespace TMG.Live
{
    public class TurboLocalToWorldAuthoring : MonoBehaviour
    {
        public class TurboLocalToWorldBaker : Baker<TurboLocalToWorldAuthoring>
        {
            public override void Bake(TurboLocalToWorldAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<TurboLocalToWorldTag>(entity);
            }
        }
    }
}