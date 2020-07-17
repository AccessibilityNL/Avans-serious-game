using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerObject;
    private Vector3 offset;

    /* Start is called on the first frame that the script is active */
    void Start()
    {
        offset = transform.position - playerObject.transform.position;
    }

    /* LateUpdate is called once per frame once every item in Update() has been processed */
    void LateUpdate()
    {
        transform.position = playerObject.transform.position + offset;
    }
}
