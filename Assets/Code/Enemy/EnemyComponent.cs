using System;
using Assets.Code.Managers;
using UnityEngine;

namespace Assets.Code.Enemy
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(DamageComponent))]
    public class EnemyComponent : MonoBehaviour
    {
        public int ScoreWorth;

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
            PlayerManager.Instance.Score += ScoreWorth;
            Destroy(gameObject);
        }

        private void OnRoundRestarted(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }
    }
}
