using System;
using UnityEngine;

namespace Assets.Code.Obstacle
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(DamageComponent))]
    public class ObstacleComponent : MonoBehaviour
    {
        private HealthComponent _healthComponent;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _healthComponent.Died += OnDeath;
        }

        private void OnDeath(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }
    }
}
