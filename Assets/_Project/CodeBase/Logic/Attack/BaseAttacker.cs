using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services;
using _Project.CodeBase.Logic.BulletLogic;
using UnityEngine;

namespace _Project.CodeBase.Logic.Attack
{
    public abstract class BaseAttacker : MonoBehaviour
    {
        [SerializeField] private Transform _pointer;
        [SerializeField] private float _bulletForce = 10f;
        [SerializeField] private float _delayBeforeShoot = 1f;
        
        private float _currentTime;
        protected IGameFactory GameFactory;
        
        protected virtual void Awake() => 
            GameFactory = AllServices.Container.Single<IGameFactory>();

        protected virtual void Update()
        {
            _currentTime += Time.deltaTime;

            if (CanShoot())
            {
                Shoot();
                _currentTime = 0f;
            }
        }
        
        private bool CanShoot() =>
            _currentTime >= _delayBeforeShoot && AdditionalShootCondition();
        
         private void Shoot()
        {
            GameObject bullet = TryGetBullet();
            if (bullet == null)
                return; 

            bullet.transform.position = _pointer.position;
            bullet.transform.rotation = _pointer.rotation;

            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody == null)
            {
                Debug.LogError("Bullet prefab does not have a Rigidbody component!");
                return;
            }

            bulletRigidbody.linearVelocity = Vector3.zero;
            bulletRigidbody.angularVelocity = Vector3.zero;

            bulletRigidbody.AddForce(_pointer.forward * _bulletForce, ForceMode.Impulse);
        }
         
        private GameObject TryGetBullet()
        {
            IBulletPool bulletPool = BulletPool;
            if (bulletPool == null)
            {
                Debug.LogError("BulletPool is null in " + gameObject.name);
                return null;
            }

            GameObject bullet = bulletPool.GetBullet();
            if (bullet == null)
            {
                bulletPool.ReturnAllBullets();

                bullet = bulletPool.GetBullet();
                if (bullet == null)
                {
                    Debug.LogError("No bullets left in the pool even after returning them!");
                }
            }

            return bullet;
        }
        
        protected abstract bool AdditionalShootCondition();

        protected abstract IBulletPool BulletPool { get; }
    }
}