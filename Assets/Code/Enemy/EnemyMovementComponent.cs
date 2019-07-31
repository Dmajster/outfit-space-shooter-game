﻿using UnityEngine;

namespace Assets.Code.Enemy
{
    public class EnemyMovementComponent : MonoBehaviour
    {
        public float MovementSpeed;
        public Vector2 TargetPosition;

        private void Update()
        {
            var targetDirection = ((Vector3) TargetPosition - transform.position).normalized;
            transform.position += targetDirection * MovementSpeed * Time.deltaTime;
        }
    }
}