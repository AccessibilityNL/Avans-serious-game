using UnityEngine;

namespace Mobility.Movement {
    public class GameState : MonoBehaviour, IGameState {
        [SerializeField]
        private CurtainManager curtainManager;

        public bool isGameOver { get; private set; } = false;

        public OnGameEnd onGameEndListener { get; set; } = null;

        public bool won { get; private set; } = false;

        public void EndGame(bool won) {
            this.won = won;

            if (onGameEndListener != null)
                onGameEndListener(won);

            isGameOver = true;
            curtainManager.Close();
        }
    }
}
