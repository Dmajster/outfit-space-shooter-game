using UnityEngine;

namespace Assets.Code.Wave_System
{
    public class WaveSystem : MonoBehaviour
    {
        [Header("Preparation time settings")]
        public float PreparationTime;
        [SerializeField] private float PreparationStart;

        [Header("Wave settings")]
        public WaveStatus WaveStatus;
        [SerializeField] private int Level;
        
        [Header("Prefabs")]
        public GameObject ObstaclePrefab;
        public GameObject EnemyPrefab;

        [Header("Level 0")]
        [SerializeField] private int _levelZeroObstaclesCount;

        [Header("Level 1")]
        [SerializeField] private int _levelOneObstaclesCount;
        [SerializeField] private int _levelOneEnemiesCount;

        private void Start()
        {
            Level = 0;
        }

        private void FixedUpdate()
        {
            if (WaveStatus == WaveStatus.Preparation)
            {
                var timeSincePreparationStart = Time.time - PreparationStart;

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

        private void StartPreparation()
        {
            WaveStatus = WaveStatus.Preparation;
            PreparationStart = Time.time;
        }

        private void StartWave()
        {
            WaveStatus = WaveStatus.InProgress;

            switch (Level)
            {
                case 0:
                    SpawnLevelZero();
                    break;

                case 1:
                    SpawnLevelOne();
                    break;

                case 2:
                    SpawnLevelTwo();
                    break;

                case 3:
                    SpawnLevelThree();
                    break;

                case 4:
                    SpawnLevelFour();
                    break;
            }

            Level++;
        }

        private void SpawnLevelZero()
        {
            var position = Vector2.zero;

            for (var i = 0; i < _levelZeroObstaclesCount; i++)
            {
                Instantiate(ObstaclePrefab, position, Quaternion.identity);
            }
        }

        private void SpawnLevelOne()
        {
            var position = Vector2.zero;

            for (var i = 0; i < _levelOneObstaclesCount; i++)
            {
                Instantiate(ObstaclePrefab, position, Quaternion.identity);
            }

            for (var i = 0; i < _levelOneEnemiesCount; i++)
            {
                Instantiate(EnemyPrefab, position, Quaternion.identity);
            }
        }

        private void SpawnLevelTwo()
        {
        }

        private void SpawnLevelThree()
        {
        }

        private void SpawnLevelFour()
        {
        }
    }
}