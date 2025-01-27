using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace _Project.CodeBase.Hud
{
    public class KillCounterUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private IPersistentProgressService _progressService;

        private void Awake() => 
            _progressService = AllServices.Container.Single<IPersistentProgressService>();

        private void Update()
        {
            string PlayerKills = _progressService.Progress.KillCounter.PlayerKills.ToString();
            string EnemyKills = _progressService.Progress.KillCounter.EnemyKills.ToString();

            _text.text = $"{PlayerKills} : {EnemyKills}";
        }
    }
}