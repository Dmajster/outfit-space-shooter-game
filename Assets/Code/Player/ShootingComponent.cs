using UnityEngine;

namespace Assets.Code.Player
{
    public class ShootingComponent : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public Transform BulletSpawn;

        public float ShootingCooldown;
        [SerializeField] private float _lastShootingTime;

        public bool SuperGun;
        public float SuperGunSpread;

        public AudioSource AudioSource;

        private void Start()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Time.timeScale == 0)
            {
                return;
            }

            if (!Input.GetButton("Shoot"))
            {
                return;
            }

            if (Time.time - _lastShootingTime < ShootingCooldown)
            {
                return;
            }


            var shootingAngle = Quaternion.Euler(transform.eulerAngles + Vector3.forward * 90); 

            // Spawn a new bullet prefab at spawn location, rotation matches rotation of Player.
            Instantiate(BulletPrefab, BulletSpawn.position, shootingAngle);
            AudioSource.Play();

            if (SuperGun)
            {
                Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.Euler(shootingAngle.eulerAngles + Vector3.forward * SuperGunSpread));
                Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.Euler(shootingAngle.eulerAngles - Vector3.forward * SuperGunSpread));
            }

            _lastShootingTime = Time.time;
        }
    }
}