using System;
using Assets.Code.Managers;
using UnityEngine;

namespace Assets.Code.Obstacle
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(DamageComponent))]
    public class ObstacleComponent : MonoBehaviour
    {
        public int ScoreWorth;

        private HealthComponent _healthComponent;

        private AudioSource _deathAudioSource;

        private void Awake()
        {
            _deathAudioSource = GetComponent<AudioSource>();
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

            WaveManager.Instance.SpawnPowerup(this.gameObject);

            _deathAudioSource.Play();

            Destroy(gameObject);
        }

        private void OnRoundRestarted(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }
    }
}
