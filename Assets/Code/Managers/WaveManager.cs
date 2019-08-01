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
        [SerializeField] private int _level;

        [SerializeField] private List<Wave> _waves = new List<Wave>()
        {
            new WaveAsteroids(),
            new WaveAsteroidsAndEnemies()
        };

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
            _level--;

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

            _waves[_level].Spawn();

            _level++;
        }
    }
}