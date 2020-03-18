using System;
using UnityEngine;

namespace Assets.Code.Enemy
{
    public class EnemyMovementComponent : MonoBehaviour
    {
        public float MovementSpeed;
        public Vector2 TargetPosition;

        public float StayStillTime = 0.5f;

        public event EventHandler TargetReached;
        [SerializeField] private float _targetReachedTime;
        [SerializeField] private float _targetReachedTolerance;

       

        

        private void Update()
        {
            if (Time.time - _targetReachedTime < StayStillTime)
            {
                return;
            }

            // Normalized so distance doesn't affect movement speed.
            var targetDirection = ((Vector3) TargetPosition - transform.position).normalized;
            transform.position += targetDirection * MovementSpeed * Time.deltaTime;

            if (Vector2.Distance(transform.position, TargetPosition) < _targetReachedTolerance)
            {
                _targetReachedTime = Time.time;
                TargetReached?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}