using System;
using System.Collections.Generic;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.StateMachine;
using _Project.CodeBase.Logic.BulletLogic;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        IBulletPool GetPlayerBulletPool();
        IBulletPool GetEnemyBulletPool();
        
        event Action HeroCreated;
        event Action EnemyCreated;

        GameObject CreateHero(GameObject at, LoadLevelState loadLevelState);
        GameObject CreateEnemy(GameObject at, LoadLevelState loadLevelState);
        GameObject HeroGameObject { get; }
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        void CreateHud();
        void Cleanup();
        void CreateBulletPools(GameObject playerBulletPrefab, GameObject enemyBulletPrefab, int poolSize,
            KillCounter progressKillCounter);
    }
}