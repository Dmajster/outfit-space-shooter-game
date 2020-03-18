using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Code.Abstractions;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        public GameObject PausePanel;
        public GameObject OptionsPanel;
        public Slider MusicLoudnessSlider;

        public bool GameIsPaused;
        public float GameInitialTimeScale;

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

            OnScoreChange(this, EventArgs.Empty);
            OnHealthChange(this, EventArgs.Empty);
            OnLivesChange(this, EventArgs.Empty);
            OnWaveChange(this, EventArgs.Empty);

            GameInitialTimeScale = Time.timeScale;
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
                hearthRectTransform.anchoredPosition = -new Vector2(
                    HeartGapRight + i * HeartGapBetween,
                    HeartGapTop
                );

                _hearts.Add(heart);
            }
        }

        public void OnPauseMenuOptionsButtonClicked()
        {
            Debug.Log("Pause panel");
            OptionsPanel.gameObject.SetActive(true);
            PausePanel.gameObject.SetActive(false);
        }

        public void OnMusicLoudnessChanged()
        {
            SoundManager.Instance.MusicSource.volume = MusicLoudnessSlider.value;
        }

        public void OnResumePressed()
        {
            GameIsPaused = !GameIsPaused;
        }

        public void OnQuitPressed()
        {
            SceneManager.LoadScene(0);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameIsPaused = !GameIsPaused;
                Time.timeScale = GameIsPaused ? 0 : GameInitialTimeScale;

                if (GameIsPaused)
                {
                    PausePanel.gameObject.SetActive(GameIsPaused);
                }
                else
                {
                    PausePanel.gameObject.SetActive(GameIsPaused);
                    OptionsPanel.gameObject.SetActive(GameIsPaused);
                }
            }
        }
    }
}
