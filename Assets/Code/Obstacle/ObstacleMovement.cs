using UnityEngine;

namespace Assets.Code.Obstacle
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class ObstacleMovement : MonoBehaviour
    {
        public float MovementVelocity;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            // pick a random angle to launch the meteor in
            var angle = Random.Range(0, 360);

            // convert to radians because Mathf Cos/Sin use radians as input
            var radianAngle = angle * Mathf.Deg2Rad;

            // Launch the projectile with velocity to avoid mass problems.
            _rigidbody.velocity = new Vector2(
                Mathf.Cos(radianAngle),
                Mathf.Sin(radianAngle)
            ) * MovementVelocity * Time.deltaTime;
        }
    }
}