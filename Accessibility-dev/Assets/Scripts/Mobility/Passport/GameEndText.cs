using UnityEngine;
using TMPro;
using Mobility.Utils;

namespace Mobility.Passport {
    public class GameEndText : MonoBehaviour {
        private TextMeshProUGUI gameEndText  { get; set; }

        public void Start() {
            gameEndText = GetComponent<TextMeshProUGUI>();
            var gameState = FindObjectOfType<GameState>();
            gameState.onGameEnd = OnGameEnd;
        }

        public void OnGameEnd(int _, int correctAnswers) {
            var isSingular = correctAnswers == 1;
            gameEndText.text = $"Er {(isSingular ? "is" : "zijn")} {correctAnswers} paspoort{(isSingular ? "" : "en")} correct ingevuld op de website";
        }
    }
}
