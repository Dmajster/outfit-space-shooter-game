using System;
using System.Collections;
using Assets.Code.Enemy;
using Assets.Code.Obstacle;
using UnityEngine;

namespace Assets.Code.Wave_System
{
    [Serializable]
    public class WaveAsteroidsAndEnemies : Wave
    {
        public override void Spawn()
        {   
            for (var i = 0; i < ObstacleCount; i++)
            {
                var position = GetRandomPositionInView();
                var direction = GetRandomDirectionVector();

                var obstacleGameObject = GameObject.Instantiate(ObstaclePrefab, position, Quaternion.identity);
                obstacleGameObject.GetComponent<ObstacleMovementComponent>().MovementDirection = direction;
            }

            for (var i = 0; i < EnemyCount; i++)
            {
                var position = GetRandomPositionInView();
                var target = GetRandomPositionInView();

                var enemyGameObject = GameObject.Instantiate(EnemyPrefab, position, Quaternion.identity);
                var enemyMovementComponent = enemyGameObject.GetComponent<EnemyMovementComponent>();
                enemyMovementComponent.TargetPosition = target;
                enemyMovementComponent.TargetReached += OnEnemyReachedTarget;
            }
        }

        private void OnEnemyReachedTarget(object sender, EventArgs e)
        {
            var enemyMovementComponent = (EnemyMovementComponent)sender;
            enemyMovementComponent.TargetPosition = GetRandomPositionInView();
        }
    }
}
