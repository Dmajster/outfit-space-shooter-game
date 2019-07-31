using System;
using Assets.Code.Abstractions;
using Assets.Code.Wave_System;
using UnityEngine;

namespace Assets.Code.Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public int StartingAmountOfLives;
        [SerializeField] private int _amountOfLives;

        [SerializeField] private PlayerComponent _player;

        public event EventHandler RoundRestarted;

        public void Awake()
        {
            _amountOfLives = StartingAmountOfLives;

            _player = FindObjectOfType<PlayerComponent>();
            _player.Died += OnDeath;
        }

        private void OnDeath(object sender, EventArgs e)
        {
            _amountOfLives--;

            if (RoundRestarted == null)
            {
                Debug.LogError("RoundRestarted event handler is null.");
            }
            RoundRestarted?.Invoke(this, EventArgs.Empty);

            WaveManager.Instance.RestartRound();
        }
    }
}
