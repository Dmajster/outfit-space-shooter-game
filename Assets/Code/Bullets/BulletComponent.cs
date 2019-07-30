using Assets.Code.Enemy;
using UnityEngine;

namespace Assets.Code.Bullets
{
    public class BulletComponent : MonoBehaviour
    {
        public float Damage;
        public float Speed;

        private void Update()
        {
            transform.position += transform.right * Speed * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var damageable = collision.gameObject.GetComponent<DamageComponent>();

            if (damageable == null)
            {
                return;
            }

            damageable.DealDamage(Damage);
        }
    }
}