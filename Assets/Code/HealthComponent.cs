using UnityEngine;

namespace Assets.Code
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField]private float _startingHealth;

        [SerializeField]private float _health;
        public float Health
        {
            get => _health;
            set => HealthChanged(value);
        }

        private void Start()
        {
            Health = _startingHealth;
        }

        private void HealthChanged(float value)
        {
            _health = value;

            if (_health < 0)
            {
                Die();
            }
        }

        private void Die()
        {

        }
    }
}
