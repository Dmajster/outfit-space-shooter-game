using System;
using Assets.Code.Obstacle;
using Assets.Code.Player;
using UnityEngine;

namespace Assets.Code.Wave_System
{
    [Serializable]
    public class WaveAsteroids : Wave
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
        }
    }
}
