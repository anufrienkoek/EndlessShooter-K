using _Project.CodeBase.Infrastructure.Services;
using _Project.CodeBase.Infrastructure.Services.Input;
using _Project.CodeBase.Logic.Attack;
using _Project.CodeBase.Logic.BulletLogic;
using UnityEngine;

namespace _Project.CodeBase.Hero
{
    public class HeroAttack : BaseAttacker
    {
        private IInputService _inputService;

        protected override void Awake()
        {
            base.Awake();
            _inputService = AllServices.Container.Single<IInputService>();
        }

        protected override bool AdditionalShootCondition() =>
            _inputService.IsAttackButtonUp();

        protected override IBulletPool BulletPool =>
            GameFactory.GetPlayerBulletPool();
    }
}
