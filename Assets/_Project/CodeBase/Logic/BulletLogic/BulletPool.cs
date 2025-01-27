using System.Collections.Generic;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.Reset;
using UnityEngine;

namespace _Project.CodeBase.Logic.BulletLogic
{
    public class BulletPool : IBulletPool
    {
        private readonly Queue<GameObject> _currentInPool;
        private readonly List<GameObject> _activeBullets;
        private readonly GameObject _bulletPrefab;
        private readonly int _poolSize;
        
        private readonly KillCounter _killCounter;

        public BulletPool(GameObject bulletPrefab, int poolSize, KillCounter killCounter)
        {
            _bulletPrefab = bulletPrefab;
            _poolSize = poolSize;
            _killCounter = killCounter;
            _currentInPool = new Queue<GameObject>();
            _activeBullets = new List<GameObject>();
        }

        public void InitializePool()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                GameObject bullet = Object.Instantiate(_bulletPrefab);
                bullet.SetActive(false);
                _currentInPool.Enqueue(bullet);
            }
        }

        public GameObject GetBullet()
        {
            if (_currentInPool.Count > 0)
            {
                GameObject bullet = _currentInPool.Dequeue();
                bullet.SetActive(true);
                
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.Initialize(this, _killCounter);
                
                _activeBullets.Add(bullet);
                return bullet;
            }

            return null;
        }

        public void ReturnBullet(GameObject bullet)
        {
            if (_activeBullets.Contains(bullet))
            {
                bullet.SetActive(false);
                _currentInPool.Enqueue(bullet);
                _activeBullets.Remove(bullet);
            }
        }

        public void ReturnAllBullets()
        {
            foreach (var bullet in _activeBullets.ToArray()) 
                ReturnBullet(bullet);
        }
    }
}