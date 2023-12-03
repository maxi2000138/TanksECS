using AB_Utility.FromSceneToEntityConverter;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;

public class InputReadFeature : BaseEcsFeature
{
    private readonly InputReadSystem _inputReadSystem;
    private readonly RotationSystem _rotationSystem;
    private readonly MovementSystem _movementSystem;

    public InputReadFeature(InputReadSystem inputReadSystem, RotationSystem rotationSystem, MovementSystem movementSystem)
    {
        _inputReadSystem = inputReadSystem;
        _rotationSystem = rotationSystem;
        _movementSystem = movementSystem;
    }
    
    public override UniTask InitializeFeaturesAsync(EcsSystems ecsSystems)
    {
        ecsSystems.Add(_inputReadSystem);
        ecsSystems.Add(_movementSystem);
        ecsSystems.Add(_rotationSystem);
        
        return UniTask.CompletedTask;
    }
    
    
}
