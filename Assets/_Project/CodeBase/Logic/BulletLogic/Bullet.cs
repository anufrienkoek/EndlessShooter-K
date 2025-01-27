using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Enemy;
using _Project.CodeBase.Hero;
using _Project.CodeBase.Infrastructure.Services;
using _Project.CodeBase.Infrastructure.Services.Reset;
using UnityEngine;

namespace _Project.CodeBase.Logic.BulletLogic
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 3f;

        private KillCounter _killCounter;
        private IBulletPool _bulletPool;
        private IResetPositionService _resetPositionService;
        private float _timer;

        private void Awake() => 
            _resetPositionService = AllServices.Container.Single<IResetPositionService>();

        public void Initialize(IBulletPool bulletPool, KillCounter killCounter)
        {
            _bulletPool = bulletPool;
            _killCounter = killCounter;
        }

        private void OnEnable() =>
            _timer = 0f;

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _lifeTime)
                ReturnToPool();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Damageable>(out var damageable))
            {
                Debug.Log($"Player:{_killCounter.PlayerKills}");
                Debug.Log($"Enemy:{_killCounter.EnemyKills}");

                if (damageable.GetComponent<EnemyAttack>())
                {
                    _killCounter.AddPlayerKill();
                    _resetPositionService.ResetPosition();
                }
                else if (damageable.GetComponent<HeroAttack>())
                {
                    _killCounter.AddEnemyKill();
                    _resetPositionService.ResetPosition();
                }
                
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            if (_bulletPool != null)
                _bulletPool.ReturnBullet(gameObject);
            else
                Debug.LogError(
                    "BulletPool reference is missing! Make sure to initialize the bullet with a valid pool.");
        }
    }
}