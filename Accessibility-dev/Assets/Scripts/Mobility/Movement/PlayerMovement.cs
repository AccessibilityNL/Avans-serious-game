using UnityEngine;
using Mobility.Utils;
using Mobility.Movement.Utils;

namespace Mobility.Movement {
    public class PlayerMovement : MonoBehaviour {
        [Range(0.5f, 10f)]
        [SerializeField]
        private float regularSpeed = 4f;

        [Range(1f, 10f)]
        [SerializeField]
        private float maxSpeed = 6f;

        [Range(0.1f, 1f)]
        [SerializeField]
        private float minSpeed = 0.2f;

        [Range(1, 4)]
        [SerializeField]
        private int speedChangeInSeconds = 1;

        [SerializeField]
        private int endX = 74;

        private VariableSpeed speed { get; set; }

        private TimeTracker timeTracker { get; set; }

        private CharacterController characterController { get; set; }

        private IGameState gameState { get; set; }

        void Start() {
            characterController = GetComponent<CharacterController>();

            speed = new VariableSpeed(regularSpeed, minSpeed, maxSpeed);
            timeTracker = new TimeTracker(speedChangeInSeconds, speed.UpdateSpeed);

            gameState = FindObjectOfType<GameState>();
        }

        void Update() {
            if (gameState.isGameOver)
                return;

            timeTracker.Tick(Time.deltaTime);

            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontalMovement, 0, verticalMovement);

            characterController.SimpleMove(movement * speed.current);

            if (transform.position.x >= this.endX)
                gameState.EndGame(true);

            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}
