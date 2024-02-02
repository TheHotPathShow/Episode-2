using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.Live
{
    public partial struct EntitySpawnSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var spawnProperties in SystemAPI.Query<SpawnProperties>())
            {
                var newParent = ecb.Instantiate(spawnProperties.Parent);

                if (spawnProperties.ShouldParent && spawnProperties.AddLinkedEntityGroup)
                {
                    ecb.AddBuffer<LinkedEntityGroup>(newParent);
                    ecb.AppendToBuffer(newParent, new LinkedEntityGroup { Value = newParent });
                }
                
                var randomIndex = 0u;
                for (var y = 0; y < spawnProperties.SpawnDimensions.y; y++)
                {
                    for (var x = 0; x < spawnProperties.SpawnDimensions.x; x++)
                    {
                        var newEntity = ecb.Instantiate(spawnProperties.Prefab);
                        var newPosition = new float3(x * spawnProperties.SpawnSpacing, 0f,
                            y * spawnProperties.SpawnSpacing);
                        newPosition += spawnProperties.SpawnOffset;
                        var newTransform = LocalTransform.FromPosition(newPosition);
                        ecb.SetComponent(newEntity, newTransform);
                        ecb.AddComponent(newEntity, new EntityRandom{Random = Random.CreateFromIndex(randomIndex)});

                        if (spawnProperties.ShouldParent)
                        {
                            ecb.AddComponent(newEntity, new Parent { Value = newParent });
                            
                            if (spawnProperties.AddLinkedEntityGroup)
                            {
                                ecb.AppendToBuffer(newParent, new LinkedEntityGroup { Value = newEntity });
                            }
                        }
                        
                        randomIndex++;
                    }
                }
            }
            
            ecb.Playback(state.EntityManager);
        }
    }
}