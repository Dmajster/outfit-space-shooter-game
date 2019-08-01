using UnityEngine;

namespace Assets.Code.Player
{
    public class ShootingComponent : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public Transform BulletSpawn;

        public float ShootingCooldown;
        [SerializeField] private float _lastShootingTime;

        private void Update()
        {
            if (!Input.GetButton("Shoot"))
            {
                return;
            }

            if (Time.time - _lastShootingTime < ShootingCooldown)
            {
                return;
            }

            // Spawn a new bullet prefab at spawn location, rotation matches rotation of Player.
            Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.Euler(transform.eulerAngles));
            _lastShootingTime = Time.time;
        }
    }
}