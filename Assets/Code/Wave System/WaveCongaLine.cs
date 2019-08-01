using System;
using System.Collections.Generic;
using Assets.Code.Enemy;
using Assets.Code.Managers;
using Assets.Code.Player;
using UnityEngine;

namespace Assets.Code.Wave_System
{
    [Serializable]
    public class WaveCongaLine : Wave
    {
        private class CongaLineData
        {
            public GameObject GameObject;
            public int NextIndex;
        }

        private List<Vector3> _congaLine = new List<Vector3>();
        private List<CongaLineData> _congaLineData = new List<CongaLineData>();

        private int congaLinePositions = 10;

        public override void Spawn()
        {   
            var player = GameObject.FindObjectOfType<PlayerComponent>();

            for (var i = 0; i < congaLinePositions; i++)
            {
                _congaLine.Add(GetRandomPositionInView());
            }

            for (var i = 0; i < EnemyCount; i++)
            {
                var position = new Vector3(ViewManager.Instance.Center.x, ViewManager.Instance.Dimensions.y + i);
                var target = _congaLine[0];

                var enemyGameObject = GameObject.Instantiate(EnemyPrefab, position, Quaternion.identity);
                var enemyMovementComponent = enemyGameObject.GetComponent<EnemyMovementComponent>();
                enemyMovementComponent.TargetPosition = target;
                enemyMovementComponent.TargetReached += OnEnemyReachedTarget;

                var enemyShootingComponent = enemyGameObject.GetComponent<EnemyShootComponent>();
                enemyShootingComponent.Target = player.gameObject;

                _congaLineData.Add(new CongaLineData()
                {
                    GameObject = enemyGameObject,
                    NextIndex = 1
                });
            }
        }

        private void OnEnemyReachedTarget(object sender, EventArgs e)
        {
            var enemyMovementComponent = (EnemyMovementComponent)sender;
            var congaLineData =_congaLineData.Find(checkedCongaLineData =>
                enemyMovementComponent.gameObject == checkedCongaLineData.GameObject);

            enemyMovementComponent.TargetPosition = _congaLine[congaLineData.NextIndex];
            congaLineData.NextIndex = (congaLineData.NextIndex + 1) % _congaLine.Count;
        }
    }
}
