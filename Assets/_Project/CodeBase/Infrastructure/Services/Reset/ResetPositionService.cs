using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.Reset
{
    public class ResetPositionService : IResetPositionService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        private Transform _heroTransform;
        private Transform _enemyTransform;
        
        public ResetPositionService(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }
        
        public void InitPositions(Transform heroTransform, Transform enemyTransform)
        {
            _heroTransform = heroTransform;
            _enemyTransform = enemyTransform;

            _progressService.Progress.WorldData.HeroPosition = heroTransform.position.AsVectorData();
            _progressService.Progress.WorldData.EnemyPosition = enemyTransform.position.AsVectorData();

            _saveLoadService.SaveProgress();
        }

        public void ResetPosition()
        {
            Vector3 heroPos = _progressService.Progress.WorldData.HeroPosition.AsUnityVector();
            Vector3 enemyPos = _progressService.Progress.WorldData.EnemyPosition.AsUnityVector();

            if (_heroTransform != null)
            {
                _heroTransform.TryGetComponent<CharacterController>(out CharacterController controller);
                
                controller.enabled = false;
                _heroTransform.position = heroPos;
                controller.enabled = true;
            }
                
            if (_enemyTransform != null)
                _enemyTransform.position = enemyPos;

            _progressService.Progress.WorldData.HeroPosition = _heroTransform.position.AsVectorData();
            _progressService.Progress.WorldData.EnemyPosition = _enemyTransform.position.AsVectorData();

            _saveLoadService.SaveProgress();
        }
    }
}