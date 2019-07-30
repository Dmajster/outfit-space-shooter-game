using UnityEngine;

namespace Assets.Code.Player
{
    public class ShootingComponent : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public Transform BulletSpawn;

        private void Update()
        {
            if (Input.GetButtonDown("Shoot"))
            {
                //Spawn a new bullet prefab at spawn location, rotation matches rotation of Player.
                Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.Euler(transform.eulerAngles));
            }
        }
    }
}