using UnityEngine;

namespace Assets.Code.Enemy
{
    public class EnemyShootComponent : MonoBehaviour
    {
        public GameObject Target;

        public float ShootCooldownTime;

        public GameObject BulletPrefab;

        private float _lastShootTime;

        private void Update()
        {
            if (Target == null)
            {
                return;
            }

            HandleOrientation();

            if (Time.time - _lastShootTime < ShootCooldownTime)
            {
                return;
            }

            HandleShooting();
        }

        private void HandleOrientation()
        {
            // Normalized for Atan2 function to return correct angle
            var targetDirection = new Vector2(
                Target.transform.position.x - transform.position.x,
                Target.transform.position.y - transform.position.y
            ).normalized;

            // Converted to Degrees as atan2 returns Radians
            var mouseAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            // Mouse angle on Z because 2D uses Z axis for rotation
            transform.rotation = Quaternion.Euler(0, 0, mouseAngle + 90);
        }

        private void HandleShooting()
        {
            _lastShootTime = Time.time;

            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles - Vector3.forward * 90));
        }
    }
}
