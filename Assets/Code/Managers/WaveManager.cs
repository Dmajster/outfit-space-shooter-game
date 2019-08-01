using System;
using System.Collections.Generic;
using Assets.Code.Abstractions;
using Assets.Code.Wave_System;
using UnityEngine;

namespace Assets.Code.Managers
{
    public class WaveManager : Singleton<WaveManager>
    {
        [Header("Preparation time settings")] public float PreparationTime;
        [SerializeField] private float _preparationStart;

        [Header("Wave settings")] public WaveStatus WaveStatus;
        public int Level;

        [SerializeField] private List<Wave> _waves = new List<Wave>()
        {
            new WaveAsteroids(),
            new WaveAsteroids(),
            new WaveAsteroidsAndEnemies(),
            new WaveAsteroidsAndEnemies()
        };

        public event EventHandler WaveChanged;

        private void FixedUpdate()
        {
            if (WaveStatus == WaveStatus.Preparation)
            {
                var timeSincePreparationStart = Time.time - _preparationStart;

                if (timeSincePreparationStart > PreparationTime)
                {
                    StartWave();
                }
            }
            else if (WaveStatus == WaveStatus.InProgress)
            {
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");

                if (enemies.Length == 0)
                {
                    StartPreparation();
                }
            }
        }

        public void RestartRound()
        {
            // Decrement level so it sets us back to same level again
            Level--;

            // Start preparation again
            StartPreparation();
        }

        private void StartPreparation()
        {
            WaveStatus = WaveStatus.Preparation;
            _preparationStart = Time.time;
        }

        private void StartWave()
        {
            WaveStatus = WaveStatus.InProgress;

            _waves[Level].Spawn();

            WaveChanged?.Invoke(this, EventArgs.Empty);

            Level++;
        }
    }
}