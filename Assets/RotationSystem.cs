using Leopotam.EcsLite;
using UnityEngine;

public class RotationSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _rotationRequestFilter;
    private EcsPool<RigidbodyPool> _movablePool;
    private EcsPool<RotationRequest> _rotationRequestPool;

    public void Init(IEcsSystems systems)
    {
        EcsWorld ecsWorld = systems.GetWorld();
        _rotationRequestFilter = ecsWorld.Filter<RotationRequest>().End();
        _movablePool = ecsWorld.GetPool<RigidbodyPool>();
        _rotationRequestPool = ecsWorld.GetPool<RotationRequest>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _rotationRequestFilter)
        {
            ref RigidbodyPool rigidbodyPool = ref _movablePool.Get(entity);
            rigidbodyPool.Rigidbody.rotation = Quaternion.Lerp(rigidbodyPool.Rigidbody.rotation,Quaternion.LookRotation(rigidbodyPool.Rigidbody.velocity), 0.1f);
            _rotationRequestPool.Del(entity);
        }
    }
}
