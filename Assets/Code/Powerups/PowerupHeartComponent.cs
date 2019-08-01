using Assets.Code.Managers;
using Assets.Code.Player;
using UnityEngine;

namespace Assets.Code.Powerups
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collision2D))]
    public class PowerupHeartComponent : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if it's the player
            var playerComponent = collision.gameObject.GetComponent<PlayerComponent>();

            if (playerComponent == null)
            {
                return;
            }

            PlayerManager.Instance.LivesLeft++;

            Destroy(gameObject);
        }
    }
}