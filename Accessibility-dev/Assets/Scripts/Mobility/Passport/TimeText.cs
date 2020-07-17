using UnityEngine;
using TMPro;
using Mobility.Utils;

namespace Mobility.Passport {
    public class TimeText : MonoBehaviour {
        private TextMeshProUGUI timeLeftText  { get; set; }

        public void Start() {
            timeLeftText = GetComponent<TextMeshProUGUI>();
            var gameState = FindObjectOfType<GameState>();
            gameState.onGameStateChanged = OnGameStateChanged;
        }

        public void OnGameStateChanged(int timeLeft, int correctAnswers) {
            timeLeftText.text = $"nog {timeLeft}s\n{correctAnswers} voltooid";
        }
    }
}
