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

        private void Awake()
        {
            HealthComponent = GetComponent<HealthComponent>();
            HealthComponent.Died += OnDeath;
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
