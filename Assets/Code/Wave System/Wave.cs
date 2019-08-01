using System;
using System.Collections.Generic;
using Assets.Code.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Code.Wave_System
{
    [Serializable]
    public class Wave
    {
        public GameObject ObstaclePrefab;
        public int ObstacleCount;

        public GameObject EnemyPrefab;
        public int EnemyCount;

        private List<GameObject> _enemies;

        public virtual void Spawn()
        {
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

        protected Vector3 GetRandomPositionOutsideView()
        {
            var view = ViewManager.Instance;
            var maxDistance = 10f;

            var positionVector = new Vector3();

            var xSide = Random.Range(0, 2);
            if (xSide == 0)
            {
                positionVector.x = Random.Range(view.MinimumView.x - maxDistance, view.MinimumView.x);
            }
            else
            {
                positionVector.x = Random.Range(view.MaximumView.x, view.MaximumView.x + maxDistance);
            }

            var ySide = Random.Range(0, 2);

            if (ySide == 0)
            {
                positionVector.y = Random.Range(view.MinimumView.y - maxDistance, view.MinimumView.y);
            }
            else
            {
                positionVector.y = Random.Range(view.MaximumView.y, view.MaximumView.y + maxDistance);
            }

            return positionVector;
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
    }
}