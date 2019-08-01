using System;
using Assets.Code.Enemy;
using Assets.Code.Obstacle;
using Assets.Code.Player;
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
                var position = GetRandomPositionOutsideView();
                var direction = GetRandomDirectionInView(position);

                var obstacleGameObject = GameObject.Instantiate(ObstaclePrefab, position, Quaternion.identity);
                obstacleGameObject.GetComponent<ObstacleMovementComponent>().MovementDirection = direction;
            }

            var player = GameObject.FindObjectOfType<PlayerComponent>();

            for (var i = 0; i < EnemyCount; i++)
            {
                var position = GetRandomPositionOutsideView();
                var target = GetRandomDirectionVector();

                var enemyGameObject = GameObject.Instantiate(EnemyPrefab, position, Quaternion.identity);
                var enemyMovementComponent = enemyGameObject.GetComponent<EnemyMovementComponent>();
                enemyMovementComponent.TargetPosition = target;
                enemyMovementComponent.TargetReached += OnEnemyReachedTarget;

                var enemyShootingComponent = enemyGameObject.GetComponent<EnemyShootComponent>();
                enemyShootingComponent.Target = player.gameObject;
            }
        }

        private void OnEnemyReachedTarget(object sender, EventArgs e)
        {
            var enemyMovementComponent = (EnemyMovementComponent)sender;
            enemyMovementComponent.TargetPosition = GetRandomPositionInView();
        }
    }
}
