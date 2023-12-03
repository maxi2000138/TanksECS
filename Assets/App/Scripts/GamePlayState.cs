using System.Collections.Generic;
using AB_Utility.FromSceneToEntityConverter;
using Cysharp.Threading.Tasks;
using Infrastructure.StateMachine;
using Leopotam.EcsLite;
using Zenject;

public class GamePlayState : IEnterState, IExitState, ITickable
{
    private EcsWorld _world;
    private EcsSystems _systems;
    
    private readonly IEnumerable<BaseEcsFeature> _ecsFeatures;

    public GamePlayState(IEnumerable<BaseEcsFeature> ecsFeatures)
    {
        _ecsFeatures = ecsFeatures;
    }

    public async UniTask Enter(IGameStateMachine gameStateMachine)
    {
        await InitECS();
    }

    public void Tick()
    {
        _systems.Run();      
    }

    public UniTask Exit()
    {
        _systems.Destroy();
        _world.Destroy();
        
        return UniTask.CompletedTask;
    }

    private async UniTask InitECS()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        
#if UNITY_EDITOR
        _systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
        _systems.Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem());
#endif
        
        foreach (BaseEcsFeature ecsFeature in _ecsFeatures) 
            await ecsFeature.InitializeFeaturesAsync(_systems);

        _systems.ConvertScene();
        _systems.Init();
    }
}
