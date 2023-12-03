using Zenject;

public class EcsFeaturesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
            Container.Bind<BaseEcsFeature>()
                .To(typeof(InputReadFeature))
                .AsSingle();
    }
}
