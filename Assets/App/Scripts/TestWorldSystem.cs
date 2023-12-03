using Leopotam.EcsLite;
using UnityEngine;

public class TestWorldSystem : IEcsInitSystem
{
    public void Init(IEcsSystems systems)
    {
        Debug.Log("InitTest");        
    }
}
