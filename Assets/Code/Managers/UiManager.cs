using System;
using System.Collections.Generic;
using Assets.Code.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Managers
{
    public class UiManager : Singleton<UiManager>
    {
        public Text Score;

        public RectTransform HeartParent;
        public GameObject HeartPrefab;

        public float HeartGapRight;
        public float HeartGapTop;
        public float HeartGapBetween;

        private List<GameObject> _hearts = new List<GameObject>();

        private void Awake()
        {
            PlayerManager.Instance.ScoreChanged += OnScoreChange;
            PlayerManager.Instance.LivesChanged += OnLivesChange;

            OnScoreChange(this, EventArgs.Empty);
            OnLivesChange(this, EventArgs.Empty);
        }

        private void OnScoreChange(object sender, EventArgs e)
        {
            Score.text = $"Score: {PlayerManager.Instance.Score}";
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
