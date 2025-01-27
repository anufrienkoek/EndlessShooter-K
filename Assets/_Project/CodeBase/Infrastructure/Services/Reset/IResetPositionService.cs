using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.Reset
{
    public interface IResetPositionService : IService
    {
        void InitPositions(Transform heroTransform, Transform enemyTransform);
        void ResetPosition();
    }
}