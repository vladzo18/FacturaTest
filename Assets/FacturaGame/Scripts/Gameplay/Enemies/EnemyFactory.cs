using FacturaGame.Scripts.GameRoot;
using FacturaGame.Scripts.Utils.Assets;
using FacturaGame.Scripts.Utils.ObjectPooling;
using UnityEngine;
using Zenject;

namespace FacturaGame.Scripts.Gameplay.Enemies
{
    public class EnemyFactory
    {
        private static string EnemyPrefamPath = "EnemyStickman";
        
        private readonly EnemyView _enemyPrefab;
        private readonly WorldSpaceView _worldSpaceView;
        private readonly DiContainer _container;

        public EnemyFactory(IAssetProvider assetProvider, WorldSpaceView worldSpaceView, DiContainer container)
        {
            _enemyPrefab = assetProvider.GetAsset<EnemyView>(EnemyPrefamPath);
            _worldSpaceView = worldSpaceView;
            _container = container;
        }

        public EnemyView CreateEnemy(Vector3 position, Transform parent)
        {
            EnemyView enemyView = ObjectPool.Instance.GetObject(_enemyPrefab);
            enemyView.Transform.position = position;
            enemyView.Transform.SetParent(parent);
            
            _container.Inject(enemyView.EnemyBehaviour);
            _container.Inject(enemyView.EnemyAnimator);
            _container.Inject(enemyView.EnemyMover);
            
            enemyView.HealthBarView = _worldSpaceView.
                CreateHealthBar(enemyView.HealthBarPosition).
                Bind(enemyView.HealthCarrier).
                SetAsEnemyBar().
                HideBar();
            
            return enemyView;
        }
    }
}
