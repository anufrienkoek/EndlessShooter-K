using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private const string Attack = "Fire1";
        private const string Vertical = "Vertical";
        private const string Horizontal = "Horizontal";

        public Vector2 Axis =>
            new Vector2(UnityEngine.Input.GetAxis(Horizontal),UnityEngine.Input.GetAxis(Vertical));
        
        public bool IsAttackButtonUp() => 
            UnityEngine.Input.GetButtonUp(Attack);
    }
}