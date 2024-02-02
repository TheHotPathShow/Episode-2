using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine.InputSystem;

namespace TMG.Live
{
    public partial struct ParentModifierSystem : ISystem
    {
        private bool _isEnabled;
        private EntityQuery _parentQuery;

        public void OnCreate(ref SystemState state)
        {
            _parentQuery = new EntityQueryBuilder(Allocator.Temp).WithAll<ParentTag>()
                .WithOptions(EntityQueryOptions.IncludeDisabledEntities).Build(ref state);
            state.RequireForUpdate(_parentQuery);
            
            _isEnabled = true;
        }

        public void OnUpdate(ref SystemState state)
        {
            if (Keyboard.current[Key.Digit1].wasPressedThisFrame)
            {
                _isEnabled = !_isEnabled;
                var parent = _parentQuery.GetSingletonEntity();
                state.EntityManager.SetEnabled(parent, _isEnabled);
            }
            
            if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
            {
                var parent = _parentQuery.GetSingletonEntity();
                state.EntityManager.DestroyEntity(parent);
            }
        }
    }
}