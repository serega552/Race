using UnityEngine;

namespace Enemy.EnemiesFactory
{
    public class DefaultEnemiesFactory : EnemiesFactory
    {
        [SerializeField] private EnemyMovement _prefab;

        public override EnemyMovement CreateEnemy()
        {
            return Instantiate(_prefab);
        }
    }
}
