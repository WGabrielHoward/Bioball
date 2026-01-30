using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.Systems
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

        public GameState CurrentState { get; private set; } = GameState.Undefined;

        public event System.Action<GameState, GameState> OnStateChanged;

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

        private void SetState(GameState newState)
        {
            if(!TransitionLegal(CurrentState, newState))
            {
                return;
            }

            var previous = CurrentState;
            CurrentState = newState;

            OnStateChanged?.Invoke(previous, newState);
        }

        public void TriggerPlay() => SetState(GameState.Playing);
        public void TriggerPause() => SetState(GameState.Pause);
        public void TriggerDefeat() => SetState(GameState.Defeat);
        public void TriggerVictory() => SetState(GameState.Victory);


        private bool TransitionLegal(GameState from, GameState to)
        {
            // No repeating
            if (to == from)
            {
                return false;
            }

            // Playing can always be entered
            if (to == GameState.Playing)
            {
                return true;
            }

            // Only allow these transitions FROM Playing
            if (from == GameState.Playing)
            {
                return (to == GameState.Pause || to == GameState.Victory || to == GameState.Defeat);
            }

            return false;
        }

    }
}


