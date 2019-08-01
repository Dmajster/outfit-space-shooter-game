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