using Unity.Entities;
using Unity.Transforms;

namespace TMG.Live
{
    [WriteGroup(typeof(LocalToWorld))]
    public struct TurboLocalToWorldTag : IComponentData {}
}