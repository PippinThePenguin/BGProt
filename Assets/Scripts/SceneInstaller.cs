using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller {
  [SerializeField] private Draw _input;
  [SerializeField] private MobManager _mobManager;

  public override void InstallBindings() {
    Container.Bind<GameState>().FromNew().AsSingle().NonLazy();
    Container.Bind<Draw>().FromInstance(_input);
    Container.Bind<MobManager>().FromInstance(_mobManager);
    
  }
}