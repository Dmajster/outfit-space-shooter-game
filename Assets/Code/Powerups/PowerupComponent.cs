using Assets.Code.Player;
using UnityEngine;

namespace Assets.Code.Powerups
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collision2D))]
    public class PowerupComponent : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if it's the player
            var playerComponent = collision.gameObject.GetComponent<PlayerComponent>();

            if (playerComponent == null)
            {
                return;
            }

            playerComponent.HealthComponent.Health = 100;

            Destroy(gameObject);
        }
    }
}