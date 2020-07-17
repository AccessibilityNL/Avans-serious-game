using UnityEngine;

namespace Mobility.Movement {
    public class CameraPosition : MonoBehaviour {
        [SerializeField]
        private GameObject followObject;

        private Vector3 offset;

        void Start() {
            offset = transform.position - followObject.transform.position;
        }

        void LateUpdate() {
            transform.position = followObject.transform.position + offset;
        }
    }
}
