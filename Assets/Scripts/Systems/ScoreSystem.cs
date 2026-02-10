using System;
using UnityEngine;

namespace Scripts.Systems
{

    public class ScoreSystem : MonoBehaviour
    {
        public static ScoreSystem Instance { get; private set; }

        public event Action<int> OnScoreChanged;

        private int currentLevelScore;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

        }

        public void ResetLevelScore()
        {
            currentLevelScore = 0;
            OnScoreChanged?.Invoke(currentLevelScore);
        }

        public void AddScore(int points)
        {
            currentLevelScore += points;
            Debug.Log($"ScoreSystem: score = {currentLevelScore}");
            OnScoreChanged?.Invoke(currentLevelScore);
        }

        public int GetCurrentLevelScore()
        {
            return currentLevelScore;
        }
    }
}