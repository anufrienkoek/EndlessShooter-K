using System;

namespace _Project.CodeBase.Infrastructure.StateMachine
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public GameLoopState(GameStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}