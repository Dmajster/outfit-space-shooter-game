using System;
using Assets.Code.Abstractions;
using Assets.Code.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public int StartingAmountOfLives;

        [SerializeField] private int _livesLeft;
        public int LivesLeft
        {
            get => _livesLeft;
            set => OnLivesChanged(value);
        }

        [SerializeField] private PlayerComponent _player;

        [SerializeField] private int _score;
        public int Score
        {
            get => _score;
            set => OnScoreChanged(value);
        }

        public float Health => _player.HealthComponent.Health;

        public event EventHandler RoundRestarted;
        public event EventHandler ScoreChanged;
        public event EventHandler LivesChanged;
        public event EventHandler HealthChanged;
        public event EventHandler GameOver;

        private void Awake()
        {
            LivesLeft = StartingAmountOfLives;

            _player = FindObjectOfType<PlayerComponent>();
            _player.Died += OnDeath;
            _player.HealthChanged += OnHealthChanged;
        }

        private void OnDeath(object sender, EventArgs e)
        {
            LivesLeft--;

            if (LivesLeft < 0)
            {
                GameOver?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (RoundRestarted == null)
            {
                Debug.LogError("RoundRestarted event handler is null.");
            }
            RoundRestarted?.Invoke(this, EventArgs.Empty);

            WaveManager.Instance.RestartRound();
        }

        private void OnHealthChanged(object sender, EventArgs e)
        {
            HealthChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnScoreChanged(int value)
        {
            _score = value;

            ScoreChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnLivesChanged(int value)
        {
            _livesLeft = value;

            LivesChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
