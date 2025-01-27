using _Project.CodeBase.CameraLogic;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.Services.Reset;
using _Project.CodeBase.Logic.BulletLogic;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string PlayerInitialPointTag = "PlayerInitialPoint";
        private const string EnemyInitialPointTag = "EnemyInitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IResetPositionService _resetPositionService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory,
            IPersistentProgressService progressService, IResetPositionService resetPositionService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _resetPositionService = resetPositionService;
        }

        public void Enter(string sceneName)
        {
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() {
        }

        private void OnLoaded()
        {
            GameObject hero = _gameFactory.CreateHero(at: GameObject.FindWithTag(PlayerInitialPointTag), this);
            GameObject enemy =_gameFactory.CreateEnemy(at: GameObject.FindWithTag(EnemyInitialPointTag), this);
            _resetPositionService.InitPositions(hero.transform, enemy.transform);

            CameraFollowTo(hero);
            _gameFactory.CreateHud();

            GameObject playerBulletPrefab = Resources.Load<GameObject>("Bullet/PlayerBulletPrefab");
            GameObject enemyBulletPrefab = Resources.Load<GameObject>("Bullet/EnemyBulletPrefab");

            _gameFactory.CreateBulletPools(playerBulletPrefab, enemyBulletPrefab, poolSize: 10, _progressService.Progress.KillCounter);
            
            IBulletPool playerBulletPool = _gameFactory.GetPlayerBulletPool();
            IBulletPool enemyBulletPool = _gameFactory.GetEnemyBulletPool();

            var killCounter = _progressService.Progress.KillCounter; 

            playerBulletPrefab.GetComponent<Bullet>().Initialize(playerBulletPool, killCounter);
            enemyBulletPrefab.GetComponent<Bullet>().Initialize(enemyBulletPool, killCounter);
            
            _stateMachine.Enter<GameLoopState>();
        }

        private static void CameraFollowTo(GameObject hero)
        {
            if (Camera.main is null) 
                return;
            
            Camera.main.TryGetComponent<CameraFollow>(out var cameraFollow);
            cameraFollow.Follow(hero);
        }
    }
}