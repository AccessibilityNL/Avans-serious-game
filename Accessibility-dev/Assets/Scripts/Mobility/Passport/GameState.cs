using UnityEngine;
using TMPro;
using Mobility.Utils;

namespace Mobility.Passport {
    public enum RunningState {
        START, RUNNING, END
    }

    public delegate void OnGameStateChanged(int timeLeft, int correctAnswers);

    public class GameState : MonoBehaviour {
        [SerializeField]
        private CurtainManager curtainManager;

        [Range(10, 120)]
        [SerializeField]
        private int time = 60;

        [SerializeField]
        private GameObject nameFieldObject;

        private TextInputField nameField {
            get {
                return nameFieldObject.GetComponent<TextInputField>();
            }
        }

        [SerializeField]
        private GameObject placeOfBirthFieldObject;

        private TextInputField placeOfBirthField {
            get {
                return placeOfBirthFieldObject.GetComponent<TextInputField>();
            }
        }

        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private TextMeshProUGUI placeOfBirthText;

        public OnGameStateChanged onGameStateChanged { get; set; }

        public OnGameStateChanged onGameEnd { get; set; }

        private PassportManager passportManager { get; set; }

        private TimeTracker timeTracker { get; set; }

        public int correctAnswers { get; private set; } = 0;

        private RunningState runningState { get; set; } = RunningState.START;

        private SceneLoader sceneLoader { get; set; }

        public void Start() {
            sceneLoader = FindObjectOfType<SceneLoader>();
            passportManager = new PassportManager();
            timeTracker = new TimeTracker(time, OnTimeUp, true);

            var keyboard = new TextInputKeyboardManager();
            nameField.OnNewCharacter = keyboard.OnNewCharacter;
            placeOfBirthField.OnNewCharacter = keyboard.OnNewCharacter;

            nameField.OnFocusChange = (enterFocus, field) => {
                if (runningState == RunningState.END || enterFocus) {
                    return true;
                } else {
                    return !OnNameSubmitted(field.InputText);
                }
            };

            placeOfBirthField.OnFocusChange = (enterFocus, field) => {
                if (runningState == RunningState.END) {
                    return true;
                } else if (enterFocus) {
                    nameField.OnReturn();
                    return true;
                } else {
                    return !OnPlaceOfBirthSubmitted(field.InputText);
                }
            };

            GetNewPassport();
        }

        public void StartGame() {
            runningState = RunningState.RUNNING;
        }

        public void Update() {
            if (runningState == RunningState.RUNNING) {
                timeTracker.Tick(Time.deltaTime);

                InvokeGameStateChangedCallback();
            }
        }

        private void InvokeGameStateChangedCallback() {
            if(onGameStateChanged != null)
                onGameStateChanged((int) (timeTracker.timeLeft + 0.5f), correctAnswers);
        }

        public void OnTimeUp() {
            runningState = RunningState.END;

            nameField.HasFocus = false;
            placeOfBirthField.HasFocus = false;

            if (onGameEnd != null)
                onGameEnd(0, correctAnswers);

            curtainManager.Close();
        }

        private bool OnNameSubmitted(string text) {
            var nameCorrect = text.Trim() == passportManager.currentPassport.name;

            if (nameCorrect)
                placeOfBirthField.HasFocus = true;

            return nameCorrect;
        }

        private bool OnPlaceOfBirthSubmitted(string text) {
            var placeOfBirthCorrect = text.Trim() == passportManager.currentPassport.placeOfBirth;
            if (placeOfBirthCorrect) {
                correctAnswers++;
                InvokeGameStateChangedCallback();
                GetNewPassport(true);
                nameField.HasFocus = true;
            }
            return placeOfBirthCorrect;
        }

        private Passport GetNewPassport(bool resetFields = false) {
            var currentPassport = passportManager.GetNewPassport();

            nameText.text = currentPassport.name;
            placeOfBirthText.text = currentPassport.placeOfBirth;

            if(resetFields) {
                nameField.Reset();
                placeOfBirthField.Reset();
            }

            return currentPassport;
        }
    }
}
