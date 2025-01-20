using _Project.CodeBase.CameraLogic;
using _Project.CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            GameObject hero = _gameFactory.CreateHero(at: GameObject.FindWithTag(InitialPointTag), this);
            _gameFactory.CreateHud();
            
            CameraFollowTo(hero);
            
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