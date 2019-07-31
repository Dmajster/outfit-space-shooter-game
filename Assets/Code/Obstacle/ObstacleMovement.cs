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
            var angle = Random.Range(0, 360);
            var radianAngle = angle * Mathf.Deg2Rad;

            _rigidbody.velocity = new Vector2(
                Mathf.Cos(radianAngle),
                Mathf.Sin(radianAngle)
            ) * MovementVelocity * Time.deltaTime;
        }
    }
}