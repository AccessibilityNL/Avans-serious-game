using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    public float speed = 100f;
    public Vector3 targetPos;
    public bool isMoving;
    public Material transparant;
    public Material metal;
    public GameObject knife;
    public GameObject melon;
    void Start()
    {
        targetPos = knife.transform.position;
        isMoving = false;
    }

    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            var colorRenderer = knife.GetComponent<Renderer>();
            colorRenderer.material = metal;
            SetTarggetPosition();
        } else
        {
            var colorRenderer = knife.GetComponent<Renderer>();
            colorRenderer.material = transparant;
        }
        if (isMoving)
        {
            MoveObject();
        }
    }
    void SetTarggetPosition()
    {
        Plane plane = new Plane(Vector3.up, knife.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0f;

        if (plane.Raycast(ray, out point))
            targetPos = ray.GetPoint(point);

        isMoving = true;
    }
    void MoveObject()
    {
        knife.transform.LookAt(melon.transform.position);
        knife.transform.position = Vector3.MoveTowards(knife.transform.position, targetPos, speed * Time.deltaTime);

        if (knife.transform.position == targetPos)
            isMoving = false;
        Debug.DrawLine(transform.position, targetPos, Color.red);

    }
}
