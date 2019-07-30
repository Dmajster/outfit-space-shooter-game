using UnityEngine;

namespace Assets
{
    public class MovementComponent : MonoBehaviour
    {
        public float MovementSpeed;

        private void Update()
        {
            HandleMovement();
            HandleOrientation();
        }

        private void HandleMovement()
        {
            // GetAxisRaw because we want accurate movement without smoothing
            // Normalized because we don't want diagonal movement to be faster
            var movementInput = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            ).normalized;

            // Delta time to make it frame rate independent
            transform.position += (Vector3) movementInput * MovementSpeed * Time.deltaTime;
        }

        private void HandleOrientation()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var mouseDirection = new Vector2(
                mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y
            ).normalized;

            var mouseAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
           
            transform.rotation = Quaternion.Euler(0, 0, mouseAngle);
        }
    }
}