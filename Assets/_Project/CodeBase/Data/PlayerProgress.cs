namespace _Project.CodeBase.Data
{
    public class PlayerProgress
    {
        public WorldData WorldData;
        public KillCounter KillCounter;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            KillCounter = new KillCounter();
        }
    }
}