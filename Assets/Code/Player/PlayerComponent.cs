using System;
using UnityEngine;

namespace Assets.Code.Player
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(DamageComponent))]
    public class PlayerComponent : MonoBehaviour
    {
        private HealthComponent _healthComponent;
        
        public event EventHandler Died;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _healthComponent.Died += OnDeath;
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
