using Leopotam.EcsLite;
using UnityEngine;
using Input = UnityEngine.Input;

public class InputReadSystem : IEcsInitSystem, IEcsRunSystem
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    private EcsFilter _playerComponentFilter = default;
    private EcsPool<MoveRequest> _moveRequestsPool = default;
    private EcsPool<Velocity> _velocityPool;
    private EcsPool<RotationRequest> _rotationRequestPool;
    
    private Vector2 _moveVector;

    public void Init(IEcsSystems systems)
    {
        EcsWorld ecsWorld = systems.GetWorld();

        _playerComponentFilter = ecsWorld.Filter<RigidbodyPool>().Inc<PlayerTag>().End();
        _moveRequestsPool = ecsWorld.GetPool<MoveRequest>();
        _rotationRequestPool = ecsWorld.GetPool<RotationRequest>();
        _velocityPool = ecsWorld.GetPool<Velocity>();
    }

    public void Run(IEcsSystems systems)
    {
        _moveVector = Vector2.zero;
        _moveVector.x = Input.GetAxis(HORIZONTAL_AXIS);
        _moveVector.y = Input.GetAxis(VERTICAL_AXIS);

        if (_moveVector != Vector2.zero)
        {
            foreach (var entity in _playerComponentFilter)
            {
                ref var moveRequest = ref _moveRequestsPool.Add(entity);
                ref var rotationRequest = ref _rotationRequestPool.Add(entity);
                ref Velocity velocity = ref _velocityPool.Add(entity);
                velocity.VelocityVector = _moveVector;
            }
        }
    }
}
