using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    private bool hasStarted = false;
    private Clickable storedClickable = null;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
            CheckClickable();
            if(Input.GetMouseButtonDown(0) && storedClickable != null)
            {
                storedClickable.OnClicked();
            }
        }
    }

    void CheckClickable()
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        if (Physics.Raycast(ray, out RaycastHit hit, 2f))
        {
            var gameObject = hit.collider.gameObject;
            if (gameObject.TryGetComponent(out Clickable clickable))
            {
                if (storedClickable == null)
                {
                    clickable.OnRaycastHit();
                    storedClickable = clickable;
                }
                else if (!storedClickable.Equals(clickable))
                {
                    clickable.OnRaycastHit();
                    storedClickable.OnRayCastMiss();
                    storedClickable = clickable;
                }
            }
            else if (storedClickable != null)
            {
                storedClickable.OnRayCastMiss();
                storedClickable = null;
            }
        }
    }

    public void StartMouseLooking()
    {
        hasStarted = true;
    }
}
