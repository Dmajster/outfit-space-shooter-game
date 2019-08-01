using System;
using UnityEngine;

namespace Assets.Code.Player
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(DamageComponent))]
    public class PlayerComponent : MonoBehaviour
    {
        public HealthComponent HealthComponent;
        
        public event EventHandler Died;
        public event EventHandler HealthChanged;

        private void Awake()
        {
            HealthComponent = GetComponent<HealthComponent>();
            HealthComponent.Died += OnDeath;
            HealthComponent.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(object sender, EventArgs e)
        {
            Debug.Log("pc health changed");
            if (HealthChanged == null)
            {
                // This shouldn't happen.
                Debug.LogError("HealthChanged event handler is null.");
                return;
            }

            HealthChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnDeath(object sender, EventArgs e)
        {
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
