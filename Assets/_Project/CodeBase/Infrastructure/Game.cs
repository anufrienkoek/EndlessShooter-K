using _Project.CodeBase.Infrastructure.Services;
using _Project.CodeBase.Infrastructure.StateMachine;

namespace _Project.CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container);
            
        }
    }
}