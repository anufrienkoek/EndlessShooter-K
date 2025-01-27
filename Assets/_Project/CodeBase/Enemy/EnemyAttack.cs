using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Logic.Attack;
using _Project.CodeBase.Logic.BulletLogic;
using UnityEngine;

namespace _Project.CodeBase.Enemy
{
    public class EnemyAttack : BaseAttacker
    {
        
        protected override bool AdditionalShootCondition() => true;

        protected override IBulletPool BulletPool =>
            GameFactory.GetEnemyBulletPool();
    }
}