using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Systems.GameState
{
    public enum GameState
    {
        Undefined,
        Playing,
        Pause,
        Victory,
        Defeat
    }

    public class GameStateSystem : MonoBehaviour
    {
        public static GameStateSystem Instance { get; private set; }

        public GameState CurrentState { get; private set; }

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            CurrentState = GameState.Playing;
        }

        public void TriggerVictory()
        {
            if (CurrentState != GameState.Playing) return;

            CurrentState = GameState.Victory;
            UnityEngine.Debug.Log("Victory!");
        }

        public void TriggerDefeat()
        {
            if (CurrentState != GameState.Playing) return;

            CurrentState = GameState.Defeat;
            UnityEngine.Debug.Log("Defeat!");
        }

        public void TriggerPause()
        {
            if (CurrentState != GameState.Playing) return;

            CurrentState = GameState.Pause;
            UnityEngine.Debug.Log("Paused");
        }

        public void TriggerPlay()
        {
            CurrentState = GameState.Playing;
            UnityEngine.Debug.Log("Playing");
        }
    }

}
