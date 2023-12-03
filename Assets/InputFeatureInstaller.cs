using Zenject;

public class InputFeatureInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InputReadSystem>().AsSingle();
        Container.Bind<MovementSystem>().AsSingle();
        Container.Bind<RotationSystem>().AsSingle();
    }
}
