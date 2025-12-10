using System;
using UnityEngine;

namespace Onion_AI
{
    [Serializable]
    public class GameplayController
    {
        [Header("Gameplay Status")]
        [SerializeField] private GamePlayState gamePlayState;
        public event Action<GamePlayState> OnGameplayStateChanged;

        public GamePlayState CurrentState => gamePlayState;

        public GameplayController()
        {
            OnGameplayStateChanged = null;
            gamePlayState = GamePlayState.Active;
        }

        //Calculate Total Score and Coins

        public void SwitchGameState(GamePlayState gamePlayState)
        {
            if (CompareGameStatus(gamePlayState))
            {
                return;
            }
            this.gamePlayState = gamePlayState;

            try
            {
                OnGameplayStateChanged?.Invoke(gamePlayState);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private bool CompareGameStatus(GamePlayState gamePlayState)
        {
            return this.gamePlayState.Equals(gamePlayState);
        }

        public void AddStateListener(Action<GamePlayState> listener) => OnGameplayStateChanged += listener;

        public void RemoveStateListener(Action<GamePlayState> listener) => OnGameplayStateChanged -= listener;
    }
}
