using UnityEngine;

namespace Enemy.EnemiesFactory
{
    public abstract class EnemiesFactory : MonoBehaviour
    {
        public abstract EnemyMovement CreateEnemy();
    }
}