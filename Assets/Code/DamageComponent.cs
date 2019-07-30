using UnityEngine;

namespace Assets.Code
{
    [RequireComponent(typeof(HealthComponent))]
    public class DamageComponent : MonoBehaviour
    {
        private HealthComponent _healthComponent;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
        }

        public void DealDamage(float amount)
        {
            _healthComponent.Health -= amount;
        }
    }
}
