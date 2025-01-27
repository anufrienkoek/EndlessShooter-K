using System;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class KillCounter
    {
        public int PlayerKills { get; set; }
        public int EnemyKills { get; set; }
        
        public void AddPlayerKill() => PlayerKills++;
        public void AddEnemyKill() => EnemyKills++;
    }
}