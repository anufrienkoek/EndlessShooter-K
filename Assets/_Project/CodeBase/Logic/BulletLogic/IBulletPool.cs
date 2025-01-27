using UnityEngine;

namespace _Project.CodeBase.Logic.BulletLogic
{
    public interface IBulletPool
    {
        GameObject GetBullet();
        void ReturnBullet(GameObject bullet);
        void ReturnAllBullets();
        void InitializePool();
    }
}