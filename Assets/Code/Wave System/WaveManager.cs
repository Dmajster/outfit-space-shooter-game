using Assets.Code.Abstractions;
using Assets.Code.Enemy;
using Assets.Code.Obstacle;
using Assets.Code.Player;
using UnityEngine;

namespace Assets.Code.Wave_System
{
    public class WaveManager : Singleton<WaveManager>
    {
        [Header("Preparation time settings")] public float PreparationTime;
        [SerializeField] private float PreparationStart;

        [Header("Wave settings")] public WaveStatus WaveStatus;
        [SerializeField] private int Level;

        [Header("Prefabs")] public GameObject ObstaclePrefab;
        public GameObject EnemyPrefab;

        [Header("Level 0")] [SerializeField] private int _levelZeroObstaclesCount;

        [Header("Level 1")] [SerializeField] private int _levelOneObstaclesCount;
        [SerializeField] private int _levelOneEnemiesCount;

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

        protected Vector3 GetRandomPositionInView()
        {
            var view = ViewManager.Instance;

            return new Vector3(
                Random.Range(view.MinimumView.x, view.MaximumView.x),
                Random.Range(view.MinimumView.y, view.MaximumView.y),
                0
            );
        }

        protected Vector2 GetRandomDirectionVector()
        {
            // Pick a random angle to launch the meteor in
            var angle = Random.Range(0, 360);

            // Convert to radians because Mathf Cos/Sin use radians as input
            var radianAngle = angle * Mathf.Deg2Rad;

            // No need to normalize, since cos(a) + sin(a) = 1
            return new Vector2(
                Mathf.Cos(radianAngle),
                Mathf.Sin(radianAngle)
            );
        }

        private void SpawnLevelZero()
        {
            for (var i = 0; i < _levelZeroObstaclesCount; i++)
            {
                var position = GetRandomPositionInView();
                var direction = GetRandomDirectionVector();

                var obstacleGameObject = Instantiate(ObstaclePrefab, position, Quaternion.identity);
                obstacleGameObject.GetComponent<ObstacleMovementComponent>().MovementDirection = direction;
            }
        }

        private void SpawnLevelOne()
        {
            for (var i = 0; i < _levelOneObstaclesCount; i++)
            {
                var position = GetRandomPositionInView();
                var direction = GetRandomDirectionVector();

                var obstacleGameObject = Instantiate(ObstaclePrefab, position, Quaternion.identity);
                obstacleGameObject.GetComponent<ObstacleMovementComponent>().MovementDirection = direction;
            }

            for (var i = 0; i < _levelOneEnemiesCount; i++)
            {
                var position = GetRandomPositionInView();

                var enemyGameObject = Instantiate(EnemyPrefab, position, Quaternion.identity);
                enemyGameObject.GetComponent<EnemyMovementComponent>().TargetPosition = enemyGameObject.transform.position;
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