using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Mobility.Passport {
    public delegate bool OnNewCharacter(char character, TextInputField inputField);

    public delegate bool OnFocusChange(bool enterFocus, TextInputField inputField);

    public interface TextInputField {
        bool HasFocus { get; set; }

        bool HasError { get; set; }

        string LabelText { get; set; }

        string InputText { get; set; }

        OnNewCharacter OnNewCharacter { get; set; }

        OnFocusChange OnFocusChange { get; set; }

        void UpdateState();

        void Reset();

        bool RequestFocus();

        void OnReturn();
    }

    class InputField : MonoBehaviour, TextInputField {
        [SerializeField]
        private Color32 activeColor = new Color32(21, 66, 115, 255);

        [SerializeField]
        private Color32 inactiveColor = new Color32(0, 0, 0, 255);

        [SerializeField]
        private Color32 errorColor = new Color32(176, 0, 32, 255);

        [SerializeField]
        private Color32 textColor = new Color32(0, 0, 0, 255);

        public Color32 TextColor {
            get { return textColor; }
            set {
                textColor = value;

                labelField.color = textColor;
                labelField.faceColor = textColor;
                inputTextField.color = textColor;
                inputTextField.faceColor = textColor;
            }
        }

        [SerializeField]
        private int activeHeight = 2;

        [SerializeField]
        private int inactiveHeight = 1;

        [SerializeField]
        private bool focussed = false;

        public bool HasFocus {
            get { return focussed; }
            set {
                if (focussed != value) {
                    focussed = value;
                    UpdateState();
                }
            }
        }

        private bool error = false;

        public bool HasError {
            get { return error; }
            set {
                if (error != value) {
                    error = value;
                    UpdateState();
                }
            }
        }

        private TextMeshProUGUI inputTextField;

        public string InputText {
            get { return inputTextField.text; }
            set { inputTextField.text = value; }
        }

        private TextMeshProUGUI labelField;

        public string LabelText {
            get { return inputTextField.text; }
            set { inputTextField.text = value; }
        }

        private Image line;

        private Color32 LineColor {
            get { return line.color; }
            set { line.color = value; }
        }

        private float LineHeight {
            get {
                return line.gameObject.transform.localScale.y;
            }
            set {
                var scale = line.gameObject.transform.localScale;
                line.gameObject.transform.localScale = new Vector3(scale.x, value, scale.z);
            }
        }

        public OnNewCharacter OnNewCharacter { get; set; } = (c, i) => false;

        public OnFocusChange OnFocusChange { get; set; } = (e, i) => false;

        void Start() {
            line = transform.Find("Line").GetComponent<Image>();
            inputTextField = transform.Find("Input").GetComponent<TextMeshProUGUI>();
            labelField = transform.Find("Label").GetComponent<TextMeshProUGUI>();

            TextColor = TextColor;
            UpdateState();
        }

        void Update() {
            if (!HasFocus) return;

            foreach (char c in Input.inputString) {
                HasError = false;

                if (c == '\b') {
                    if (InputText.Length != 0)
                        InputText = InputText.Substring(0, InputText.Length - 1);
                } else if (c == '\n' || c == '\r') {
                    OnReturn();
                    return;
                } else if (!OnNewCharacter(c, this)) {
                    InputText += c;
                }
            }
        }

        public void OnReturn() {
            if (!OnFocusChange(false, this)) {
                HasFocus = false;
            } else if(InputText.Length > 0) {
                HasError = true;
            }
        }

        public void UpdateState() {
            LineColor = HasError ? errorColor : HasFocus ? activeColor : inactiveColor;
            LineHeight = HasFocus ? activeHeight : inactiveHeight;
            labelField.fontStyle = HasFocus ? FontStyles.Bold : FontStyles.Normal;
        }

        public void OnPointerClick() {
            RequestFocus();
        }

        public bool RequestFocus() {
            if (!OnFocusChange(true, this)) {
                HasFocus = true;
                return true;
            }

            return false;
        }

        public void Reset() {
            InputText = "";
            HasError = false;
        }
    }
}
