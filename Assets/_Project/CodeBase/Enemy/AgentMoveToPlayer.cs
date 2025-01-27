using System;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.CodeBase.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float MinimalDistance = 1;
        
        public NavMeshAgent Agent;
        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null)
                InitializeHeroTransform();
            else
                _gameFactory.HeroCreated += HeroCreated;
        }

        private void Update()
        {
            if(Initialized() && HeroNotReached())
                Agent.destination = _heroTransform.position;
        }

        private void HeroCreated() => 
            InitializeHeroTransform();

        private void InitializeHeroTransform() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool Initialized() => 
            _heroTransform != null;

        private bool HeroNotReached() => 
            Vector3.Distance(Agent.transform.position,_heroTransform.position) >= MinimalDistance;
    }
}
