using Zenject;
using UnityEngine;

public class HandlerInstaller : MonoInstaller
{
    [SerializeField] private ClickHandler _clickHandler;
    
    public override void InstallBindings()
    {
        Container.Bind<ClickHandler>().FromComponentInNewPrefab(_clickHandler).AsSingle();
    }
}
