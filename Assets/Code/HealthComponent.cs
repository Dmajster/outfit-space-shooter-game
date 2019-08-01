using System;
using Assets.Code.Managers;
using UnityEngine;

namespace Assets.Code
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float _startingHealth;

        [SerializeField] private float _health;

        public event EventHandler Died;
        public event EventHandler HealthChanged;

        public float Health
        {
            get => _health;
            set => OnHealthChanged(value);
        }

        private void Start()
        {
            SetInitialHealth();

            PlayerManager.Instance.RoundRestarted += OnRoundRestarted;
        }

        private void Update()
        {
            HandleOutOfBounds();
        }

        private void HandleOutOfBounds()
        {
            if (Vector2.Distance(transform.position, Vector2.zero) > 50)
            {
                Debug.Log(transform.name);
                Health = 0;
            }
        }

        private void SetInitialHealth()
        {
            Health = _startingHealth;
        }

        private void OnRoundRestarted(object sender, EventArgs e)
        {
            SetInitialHealth();
        }

        private void OnHealthChanged(float value)
        {
            _health = value;

            HealthChanged?.Invoke(this, EventArgs.Empty);

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