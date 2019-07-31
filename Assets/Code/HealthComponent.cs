using System;
using UnityEngine;

namespace Assets.Code
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float _startingHealth;

        [SerializeField] private float _health;

        public event EventHandler Died;

        public float Health
        {
            get => _health;
            set => OnHealthChanged(value);
        }

        private void Start()
        {
            Health = _startingHealth;
        }

        private void OnHealthChanged(float value)
        {
            _health = value;

            if (_health > 0)
            {
                return;
            }

            if (Died == null)
            {
                // This shouldn't happen.
                Debug.LogError("Died event handler is null.");
                return;
            }

            Died(this, EventArgs.Empty);
        }
    }
}