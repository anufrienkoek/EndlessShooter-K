using System;
using System.Collections;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.Reset;
using UnityEngine;

namespace _Project.CodeBase.Logic
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] private Vector3 _initialPosition;
        private KillCounter _killCounter;
        private bool _isAlive = true;
        private IGameFactory _gameFactory;

        public void TakeDamage()
        {
            if (_isAlive)
            {
                _isAlive = false;
                ResetPosition();
                
                _isAlive = true;
            }
        }

        public void ResetPosition() => 
            transform.position = _initialPosition;
    }
}