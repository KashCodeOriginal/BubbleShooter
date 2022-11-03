using Zenject;
using UnityEngine;
using KasherOriginal.Settings;
using KasherOriginal.AssetsAddressable;
using KasherOriginal.Factories.UIFactory;
using KasherOriginal.Factories.BallFactory;

public class ServiceInstaller : MonoInstaller
{
    [SerializeField] private GameSettings _gameSettings;
    
    public override void InstallBindings()
    {
        BindUIFactory();
        BindBallsFactory();
        BindBallRelation();
        BindGameSettings();
        BindAbstractFactory();
        BindAssetsAddressable();
        BindCellMatrixWatcher();
        BindBallInstancesWatcher();
        BindShootableBallsContainer();
        BindGeneratedLevelsCreation();
    }

    private void BindAssetsAddressable()
    {
        Container.BindInterfacesTo<AssetsAddressableService>().AsSingle();
    }

    private void BindUIFactory()
    {
        Container.BindInterfacesTo<UIFactory>().AsSingle();
    }

    private void BindBallsFactory()
    {
        Container.BindInterfacesTo<BallsFactory>().AsSingle();
    }

    private void BindAbstractFactory()
    {
        Container.BindInterfacesTo<AbstractFactory>().AsSingle();
    }

    private void BindGameSettings()
    {
        Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle();
    }
    
    private void BindBallRelation()
    {
        Container.BindInterfacesTo<BallTypesRelation>().AsSingle();
    }

    private void BindCellMatrixWatcher()
    {
        Container.BindInterfacesTo<CellsMatrixWatcher>().AsSingle();
    }
    
    private void BindBallInstancesWatcher()
    {
        Container.BindInterfacesTo<BallInstancesWatcher>().AsSingle();
    }

    private void BindShootableBallsContainer()
    {
        Container.BindInterfacesTo<ShootableBallsContainer>().AsSingle();
    }
    
    private void BindGeneratedLevelsCreation()
    {
        Container.BindInterfacesTo<GeneratedLevelCreator>().AsSingle();
    }
}
