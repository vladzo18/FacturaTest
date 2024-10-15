using FacturaGame.Scripts.Gameplay.Car;
using FacturaGame.Scripts.Gameplay.Enemies;
using FacturaGame.Scripts.Gameplay.Services.InputService;
using FacturaGame.Scripts.Gameplay.Services.StoperService;
using FacturaGame.Scripts.Gameplay.UI;
using FacturaGame.Scripts.GameRoot;
using FacturaGame.Scripts.Utils.Assets;
using UnityEngine;
using Zenject;

namespace FacturaGame.Scripts.Gameplay.Root
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private MobileInput _inputService;
        [SerializeField] private CameraViewChanger _cameraViewChanger;
        [SerializeField] private CameraFollover _cameraFollover;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private ParticleSpawner.ParticleSpawner _particleSpawner;

        public override void InstallBindings()
        {
            BindInput();
            BindCameraEntities();
            BindUIEntities();
            BindEnemyEntities();
            BindPlayer();
            BindImportantSomeOtherEntities();
        }

        private void BindInput()
        {
            Container.
                Bind<IInputService>().
                To<MobileInput>().
                FromInstance(_inputService).
                AsSingle().
                NonLazy();
        }

        private void BindPlayer()
        {
            Container.
                Bind<CarView>().
                FromComponentInNewPrefabResource("CarView").
                AsSingle().
                NonLazy();
        }

        private void BindCameraEntities()
        {
            Container.
                Bind<CameraViewChanger>().
                FromInstance(_cameraViewChanger).
                AsSingle().
                NonLazy();

            Container.
                Bind<CameraFollover>().
                FromInstance(_cameraFollover).
                AsSingle();
        }

        private void BindUIEntities()
        {
            Container.
                Bind<WorldSpaceView>().
                FromComponentInNewPrefabResource("WorldSpaceView").
                AsSingle().
                NonLazy();
            
            Container.
                Bind<UIRootSceneView>().
                FromComponentInNewPrefabResource("UIRootVIew").
                AsSingle().
                NonLazy();
            
            Container.
                Bind<GameplaySceneUI>().
                FromComponentInNewPrefabResource("GameplaySceneUI").
                AsSingle().
                NonLazy();
        }

        private void BindEnemyEntities()
        {
            Container.
                Bind<EnemyFactory>().
                FromNew().
                AsTransient();

            Container.
                Bind<EnemySpawner>().
                FromInstance(_enemySpawner).
                AsSingle().
                NonLazy();
        }

        private void BindImportantSomeOtherEntities()
        {
            Container.
                Bind<IAssetProvider>().
                To<AssetProvider>().
                AsTransient();
            
            Container.
                Bind<SystemsStopService>().
                FromNew().
                AsSingle();

            Container.
                Bind<ParticleSpawner.ParticleSpawner>().
                FromInstance(_particleSpawner).
                AsSingle().
                NonLazy();
        }
    }
}