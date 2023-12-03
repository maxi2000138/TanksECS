using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;

public class InputTestFeature : BaseEcsFeature
{
    private readonly TestWorldSystem _helloWorldSystem;

    public InputTestFeature(TestWorldSystem helloWorldSystem)
    {
        _helloWorldSystem = helloWorldSystem;
    }
    
    public override UniTask InitializeFeaturesAsync(EcsSystems ecsSystems)
    {
        ecsSystems.Add(_helloWorldSystem);
        
        return UniTask.CompletedTask;
    }
    
    
}
