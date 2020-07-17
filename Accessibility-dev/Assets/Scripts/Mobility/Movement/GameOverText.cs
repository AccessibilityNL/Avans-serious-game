using UnityEngine;
using TMPro;

namespace Mobility.Movement {
    public class GameOverText : MonoBehaviour {
        [SerializeField]
        private string gameLostText;

        [SerializeField]
        private string gameWonText;

        void Start() {
            var gameState = (IGameState) FindObjectOfType<GameState>();
            gameState.onGameEndListener = OnGameEnd;
        }

        private void OnGameEnd(bool won) {
            var textComponent = gameObject.GetComponent<TextMeshProUGUI>();
            textComponent.text = won ? gameWonText : gameLostText;
        }
    }
}
