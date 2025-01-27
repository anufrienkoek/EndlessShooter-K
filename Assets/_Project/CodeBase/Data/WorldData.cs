using System;
using UnityEngine;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public KillCounter KillCounter;
        
        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
            KillCounter = new KillCounter();
        }

        public Vector3Data HeroPosition;
        public Vector3Data EnemyPosition;
    }
}