using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TMG.Live
{
    public partial struct SpawnPrefabSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<SpawnPrefab>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var entityPrefab = SystemAPI.GetSingleton<SpawnPrefab>().Value;
            var localECB = new EntityCommandBuffer(Allocator.Temp);
            
            // Pro - spawns in correct place on this frame
            // Con - creates sync point in middle of player loop
            if (Keyboard.current[Key.Digit1].wasPressedThisFrame)
            {
                var newEntity = localECB.Instantiate(entityPrefab);
                localECB.SetComponent(newEntity, LocalTransform.FromPosition(2, 2, 2));
                
                Debug.Break();
            }

            localECB.Playback(state.EntityManager);

            // Pro - spawns with correct LocalTransform this frame
            // Con - spawns with incorrect LocalToWorld and is rendered at prefab default position
            var endECB = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
            {
                var newEntity = endECB.Instantiate(entityPrefab);
                endECB.SetComponent(newEntity, LocalTransform.FromPosition(4, 4, 4));
                
                Debug.Break();
            }
            
            // Pro - spawns with correct LocalTransform and LocalToWorld this frame with no additional sync point
            // Con - 2 extra lines of code, child entities appear at their default position
            if (Keyboard.current[Key.Digit3].wasPressedThisFrame)
            {
                var newEntity = endECB.Instantiate(entityPrefab);
                var transform = LocalTransform.FromPosition(-2, 2, 2);
                endECB.SetComponent(newEntity, transform);
                endECB.SetComponent(newEntity, new LocalToWorld { Value = transform.ToMatrix() });
                
                Debug.Break();
            }
            
            // Pro - spawns with correct LocalTransform and LocalToWorld
            // Con - spawns on the next frame
            var beginECB = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            if (Keyboard.current[Key.Digit4].wasPressedThisFrame)
            {
                var newEntity = beginECB.Instantiate(entityPrefab);
                beginECB.SetComponent(newEntity, LocalTransform.FromPosition(-4, 4, 4));
                
                Debug.Break();
            }
        }
    }
}