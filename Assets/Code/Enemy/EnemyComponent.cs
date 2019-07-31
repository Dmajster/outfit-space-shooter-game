using System;
using Assets.Code.Player;
using UnityEngine;

namespace Assets.Code.Enemy
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(DamageComponent))]
    public class EnemyComponent : MonoBehaviour
    {
        private HealthComponent _healthComponent;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _healthComponent.Died += OnDeath;

            PlayerManager.Instance.RoundRestarted += OnRoundRestarted;
        }

        private void OnDestroy()
        {
            _healthComponent.Died -= OnDeath;
            PlayerManager.Instance.RoundRestarted -= OnRoundRestarted;
        }

        private void OnDeath(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }

        private void OnRoundRestarted(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }
    }
}
