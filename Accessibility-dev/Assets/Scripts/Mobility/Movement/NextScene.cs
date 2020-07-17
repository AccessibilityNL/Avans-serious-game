using System.Collections;
using UnityEngine;

namespace Mobility.Movement {
    public class NextScene : MonoBehaviour {
        [SerializeField]
        private string nextSceneName = "WhackAMole";

        [SerializeField]
        private string categoryName = "mobility";

        [SerializeField]
        private int waitingTime = 2;

        private GameState gameState { get; set; }

        private SceneLoader sceneLoader { get; set; }

        void Start() {
            gameState = FindObjectOfType<GameState>();
            sceneLoader = FindObjectOfType<SceneLoader>();
        }

        private int Score {
            get {
                return gameState.won ? 25 : 0;
            }
        }

        public void GoToNextScene() {
            StartCoroutine(GoToNextSceneAsync());
        }

        public IEnumerator GoToNextSceneAsync() {
            yield return new WaitForSeconds(waitingTime);

            if(sceneLoader)
                sceneLoader.ChangeToScene(nextSceneName, categoryName, Score);
        }
    }
}
