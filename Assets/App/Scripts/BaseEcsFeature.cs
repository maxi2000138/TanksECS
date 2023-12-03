using System;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;

[Serializable]
public abstract class BaseEcsFeature
{
    public abstract UniTask InitializeFeaturesAsync(EcsSystems ecsSystems);
}
