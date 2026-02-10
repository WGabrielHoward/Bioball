using System;
using UnityEngine;

namespace Scripts.Systems
{

    public class ScoreSystem : MonoBehaviour
    {
        public static ScoreSystem Instance { get; private set; }

        public event Action<int> OnScoreAdded;

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

        public void AddScore(int points)
        {
            UnityEngine.Debug.Log($"ScoreSystem: AddScore {points}");
            OnScoreAdded?.Invoke(points);
        }
    }
}