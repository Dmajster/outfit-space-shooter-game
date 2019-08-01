using System;
using Assets.Code.Abstractions;
using Assets.Code.Player;
using UnityEngine;

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

        public event EventHandler RoundRestarted;
        public event EventHandler ScoreChanged;
        public event EventHandler LivesChanged;

        public void Awake()
        {
            LivesLeft = StartingAmountOfLives;

            _player = FindObjectOfType<PlayerComponent>();
            _player.Died += OnDeath;
        }

        private void OnDeath(object sender, EventArgs e)
        {
            LivesLeft--;

            if (RoundRestarted == null)
            {
                Debug.LogError("RoundRestarted event handler is null.");
            }
            RoundRestarted?.Invoke(this, EventArgs.Empty);

            WaveManager.Instance.RestartRound();
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
