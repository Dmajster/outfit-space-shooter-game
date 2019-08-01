using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Code.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Managers
{
    public class UiManager : Singleton<UiManager>
    {
        public Text Score;
        public Text Health;
        public Text Wave;

        public RectTransform HeartParent;
        public GameObject HeartPrefab;

        public Text GameOver;
        public Text YouWin;

        public float HeartGapRight;
        public float HeartGapTop;
        public float HeartGapBetween;

        private List<GameObject> _hearts = new List<GameObject>();

        private void Start()
        {
            PlayerManager.Instance.ScoreChanged += OnScoreChange;
            PlayerManager.Instance.LivesChanged += OnLivesChange;
            PlayerManager.Instance.HealthChanged += OnHealthChange;
            PlayerManager.Instance.GameOver += OnGameOver;
            WaveManager.Instance.WaveChanged += OnWaveChange;
            WaveManager.Instance.YouWin += OnYouWin;

            OnScoreChange(this,EventArgs.Empty);
            OnHealthChange(this, EventArgs.Empty);
            OnLivesChange(this, EventArgs.Empty);
            OnWaveChange(this, EventArgs.Empty);
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            Debug.Log("Game Over!");
            GameOver.gameObject.SetActive(true);
            StartCoroutine(ExitGame());
        }

        private IEnumerator ExitGame()
        {
            yield return new WaitForSeconds(5);

            Application.Quit();
        }

        private void OnYouWin(object sender, EventArgs e)
        {
            Debug.Log("You Win!");
            YouWin.gameObject.SetActive(true);
            StartCoroutine(ExitGame());
        }

        private void OnScoreChange(object sender, EventArgs e)
        {
            Score.text = $"Score: {PlayerManager.Instance.Score}";
        }

        private void OnHealthChange(object sender, EventArgs e)
        {
            Health.text = $"Health: {PlayerManager.Instance.Health}";
        }

        private void OnWaveChange(object sender, EventArgs e)
        {
            Wave.text = $"Wave: {WaveManager.Instance.Level}";
        }

        private void OnLivesChange(object sender, EventArgs e)
        {
            _hearts.ForEach(Destroy);
            _hearts.Clear();

            var livesLeft = PlayerManager.Instance.LivesLeft;

            for (var i = 0; i < livesLeft; i++)
            {
                var heart = Instantiate(HeartPrefab, Vector3.zero, Quaternion.identity);
                var hearthRectTransform = heart.GetComponent<RectTransform>();

                hearthRectTransform.SetParent(HeartParent);
                hearthRectTransform.anchoredPosition = - new Vector2(
                    HeartGapRight + i * HeartGapBetween,
                    HeartGapTop
                );

                _hearts.Add(heart);
            }
        }
    }
}
