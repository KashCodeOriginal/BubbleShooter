using Zenject;

public class GameInstanseInstaller : MonoInstaller
{
   public override void InstallBindings()
   {
      BindGameInstance();
   }

   private void BindGameInstance()
   {
      Container.Bind<GameInstance>().AsSingle().NonLazy();
   }
}
