using Leopotam.EcsLite;
using UnityEngine;

public class MovementSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _moveRequesFilter;
    private EcsPool<Velocity> _velocityPool;
    private EcsPool<MoveRequest> _moveRequestPool;
    private EcsPool<RigidbodyPool> _movablePool;
    private EcsPool<RotationRequest> _rotationRequestPool;
    private Vector3 moveVector;
    private float _velocity = 3f;

    public void Init(IEcsSystems systems)
    {
        EcsWorld ecsWorld = systems.GetWorld();
        _moveRequesFilter = ecsWorld.Filter<MoveRequest>().End();
        _movablePool = ecsWorld.GetPool<RigidbodyPool>();
        _moveRequestPool = ecsWorld.GetPool<MoveRequest>();
        _rotationRequestPool = ecsWorld.GetPool<RotationRequest>();
        _velocityPool = ecsWorld.GetPool<Velocity>();
        
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _moveRequesFilter)
        {
            ref var movableComponent = ref _movablePool.Get(entity);
            ref var velocityComponent = ref _velocityPool.Get(entity);

            moveVector = new Vector3(velocityComponent.VelocityVector.x, 0f, velocityComponent.VelocityVector.y);
            movableComponent.Rigidbody.velocity = moveVector * _velocity;
            _moveRequestPool.Del(entity);
            _velocityPool.Del(entity);
        }
    }
}
