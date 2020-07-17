using TMPro;
using UnityEngine;

namespace Mobility.Utils {
    public class TMProTextColor : MonoBehaviour {
        [SerializeField]
        private Color32 color;

        [ContextMenu("Change color")]
        public void Start() {
            var text = GetComponent<TextMeshProUGUI>();
            text.color = color;
            text.faceColor = color;
        }
    }
}
