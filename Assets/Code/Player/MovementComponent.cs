using Assets.Code.Managers;
using UnityEngine;

namespace Assets.Code.Player
{
    public class MovementComponent : MonoBehaviour
    {
        public float MovementSpeed;

        private void Update()
        {
            if (Time.timeScale == 0)
            {  
                return;
            }

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

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, ViewManager.Instance.MinimumView.x, ViewManager.Instance.MaximumView.x),
                Mathf.Clamp(transform.position.y, ViewManager.Instance.MinimumView.y, ViewManager.Instance.MaximumView.y),
                0
            );
        }

        private void HandleOrientation()
        {
            // We need to convert from screen space to world space for proper direction normal calculation
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Normalized for Atan2 function to return correct angle
            var mouseDirection = new Vector2(
                mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y
            ).normalized;

            // Converted to Degrees as atan2 returns Radians
            var mouseAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

            // Mouse angle on Z because 2D uses Z axis for rotation
            transform.rotation = Quaternion.Euler(0, 0, mouseAngle - 90);
        }
    }
}