using System;
using Assets.Code.Managers;
using UnityEngine;

namespace Assets.Code.Bullets
{
    public class BulletComponent : MonoBehaviour
    {
        public float Damage;
        public float Speed;

        public float BulletLifespan;

        public GameObject DestroyEffect;

        private void Start()
        {
            Destroy(gameObject, BulletLifespan);
            PlayerManager.Instance.RoundRestarted += OnRoundRestarted;
        }

        private void Update()
        {
            transform.position += transform.right * Speed * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Try and find if the object can be damaged
            var damageable = collision.gameObject.GetComponent<DamageComponent>();

            // If it can't stop this method
            if (damageable == null)
            {
                return;
            }

            // Call damage deal method on collided game object
            damageable.DealDamage(Damage);

            Destroy(gameObject);

            if (DestroyEffect != null)
            {
                Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            }
        }

        private void OnDestroy()
        {
            PlayerManager.Instance.RoundRestarted -= OnRoundRestarted;
        }

        private void OnRoundRestarted(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }
    }
}