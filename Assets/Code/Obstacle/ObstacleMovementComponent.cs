using Assets.Code.Managers;
using Assets.Code.Player;
using UnityEngine;

namespace Assets.Code.Obstacle
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class ObstacleMovementComponent : MonoBehaviour
    {
        public Vector2 MovementDirection;

        public float MovementVelocity;

        public float Damage;

        public float MinAngularVelocity;
        public float MaxAngularVelocity;

        private Rigidbody2D _rigidbody;

        [SerializeField] private ViewManager _view;
        [SerializeField] private bool _hasEnteredView;
        [SerializeField] private bool _isInView;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _view = ViewManager.Instance;
        }

        private void Start()
        {
            // Launch the projectile with velocity to avoid mass problems.
            _rigidbody.velocity = MovementDirection * MovementVelocity * Time.deltaTime;

            // Make it spin
            _rigidbody.angularVelocity = Random.Range(MinAngularVelocity, MaxAngularVelocity);
        }

        private void Update()
        {
            HandleIsInView();
            HandleTeleportation();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if it's the player
            if (collision.gameObject.GetComponent<PlayerComponent>() == null)
            {
                return;
            }

            // Try and find if the object can be damaged
            var damageable = collision.gameObject.GetComponent<DamageComponent>();

            // If it can't stop this method
            if (damageable == null)
            {
                return;
            }

            // Call damage deal method on collided game object
            damageable.DealDamage(Damage);
        }

        private void HandleIsInView()
        {
            // Basic bounding box check to determine if it's inside the view
            _isInView = transform.position.x >= _view.MinimumView.x && transform.position.x <= _view.MaximumView.x &&
                        transform.position.y >= _view.MinimumView.y && transform.position.y <= _view.MaximumView.y;
            // Check if it's the first time it's entering the view
            if (_hasEnteredView || !_isInView)
            {
                return;
            }

            _hasEnteredView = _isInView;
        }

        private void HandleTeleportation()
        {
            // If it hasn't entered the view yet don't teleport it
            if (!_hasEnteredView)
            {
                return;
            }

            // If it's still in view don't teleport it
            if (_isInView)
            {
                return;
            }

            // Separated into 2 if's in case we step out on both X and Y
            if (transform.position.x < _view.MinimumView.x)
            {
                transform.position += Vector3.right * _view.Dimensions.x;
            }
            else if (transform.position.x > _view.MaximumView.x)
            {
                transform.position -= Vector3.right * _view.Dimensions.x;
            }

            if (transform.position.y < _view.MinimumView.y)
            {
                transform.position += Vector3.up * _view.Dimensions.y;
            }
            else if (transform.position.y > _view.MaximumView.y)
            {
                transform.position -= Vector3.up * _view.Dimensions.y;
            }
        }
    }
}