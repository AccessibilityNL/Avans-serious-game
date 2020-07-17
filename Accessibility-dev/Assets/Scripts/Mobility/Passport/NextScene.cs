using System.Collections;
using UnityEngine;

namespace Mobility.Passport {
    public class NextScene : MonoBehaviour {
        [SerializeField]
        private string nextSceneName = "Movement";

        [SerializeField]
        private string categoryName = "mobility";

        [SerializeField]
        private int maxScore = 10;

        [SerializeField]
        private int waitingTime = 2;

        private GameState gameState { get; set; }

        private SceneLoader sceneLoader { get; set; }

        void Start() {
            gameState = FindObjectOfType<GameState>();
            sceneLoader = FindObjectOfType<SceneLoader>();
        }

        private float PercentOfMax {
            get {
                return (float) gameState.correctAnswers * 100f / (float) maxScore;
            }
        }

        private int Score {
            get {
                return (int) ((PercentOfMax * 25f / 100f) + 0.5f);
            }
        }

        public void GoToNextScene() {
            StartCoroutine(GoToNextSceneAsync());
        }

        public IEnumerator GoToNextSceneAsync() {
            yield return new WaitForSeconds(waitingTime);

            if(sceneLoader)
                sceneLoader.ChangeToScene(nextSceneName, categoryName, Score >= 25 ? 25 : Score);
        }
    }
}
