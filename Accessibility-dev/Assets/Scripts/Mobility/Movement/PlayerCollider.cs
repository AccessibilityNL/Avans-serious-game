using UnityEngine;

namespace Mobility.Movement {
    public class PlayerCollider : MonoBehaviour {
        void OnTriggerEnter(Collider collider) {
            Destroy(collider.gameObject);

            var gameState = (IGameState) FindObjectOfType<GameState>();

            gameState.EndGame(false);
        }
    }
}
