using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMouse : MonoBehaviour
{

    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float moveSpeed = 10f;
    void Start()
    {
        Cursor.visible = false;
        Debug.Log(Cursor.visible);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {        
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }
}
