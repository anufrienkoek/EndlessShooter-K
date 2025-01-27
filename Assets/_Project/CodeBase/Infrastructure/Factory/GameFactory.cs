using System;
using System.Collections.Generic;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.StateMachine;
using _Project.CodeBase.Logic.BulletLogic;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public event Action HeroCreated;
        public event Action EnemyCreated;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameObject HeroGameObject { get; private set; }

        private readonly IAssets _assets;
        
        private IBulletPool _playerBulletPool;
        private IBulletPool _enemyBulletPool;

        public GameFactory(IAssets assets) => 
            _assets = assets;

        public GameObject CreateHero(GameObject at, LoadLevelState loadLevelState)
        {
            GameObject hero = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
            HeroCreated?.Invoke();

            HeroGameObject = hero;

            return hero;
        }

        public GameObject CreateEnemy(GameObject at, LoadLevelState loadLevelState)
        {
            GameObject enemy = InstantiateRegistered(AssetPath.EnemyPath, at.transform.position);
            EnemyCreated?.Invoke();

            return enemy;
        }

        public void CreateHud() => 
            InstantiateRegistered(AssetPath.HudPath);

        public void CreateBulletPools(GameObject playerBulletPrefab, GameObject enemyBulletPrefab, int poolSize,
            KillCounter progressKillCounter)
        {
            _playerBulletPool = new BulletPool(playerBulletPrefab, poolSize, progressKillCounter);
            _enemyBulletPool = new BulletPool(enemyBulletPrefab, poolSize, progressKillCounter);

            _playerBulletPool.InitializePool();
            _enemyBulletPool.InitializePool();
        }

        public IBulletPool GetPlayerBulletPool() => 
            _playerBulletPool;

        public IBulletPool GetEnemyBulletPool() => 
            _enemyBulletPool;

        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at: position);
            RegisterProgressWatcher(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatcher(gameObject);
            
            return gameObject;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void RegisterProgressWatcher(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }
    }
}
