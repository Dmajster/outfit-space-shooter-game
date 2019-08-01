using System;
using System.Collections.Generic;
using Assets.Code.Abstractions;
using Assets.Code.Wave_System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Code.Managers
{
    public class WaveManager : Singleton<WaveManager>
    {
        [Header("Preparation time settings")] public float PreparationTime;
        [SerializeField] private float _preparationStart;

        [Header("Wave settings")] public WaveStatus WaveStatus;
        public int Level;

        public event EventHandler YouWin;

        public GameObject HeartPrefab;
        public GameObject HealthPrefab;
        public GameObject SuperGunPrefab;

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
                    if (Level == _waves.Count)
                    {
                        YouWin?.Invoke(this,EventArgs.Empty);
                        return;
                    }
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

        public void SpawnPowerup(GameObject gameObject)
        {
            var rng = Random.Range(0, 100);

            if (rng < 70)
            {
                return;
            }
            else if (rng < 80)
            {
                Instantiate(HealthPrefab, gameObject.transform.position, Quaternion.identity);
            }
            else if (rng < 90)
            {
                Instantiate(HeartPrefab, gameObject.transform.position, Quaternion.identity);
            }
            else if (rng < 95)
            {
                Instantiate(SuperGunPrefab, gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}