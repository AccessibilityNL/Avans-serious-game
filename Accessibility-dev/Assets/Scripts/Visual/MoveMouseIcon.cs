using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveMouseIcon : MonoBehaviour
{
    public float speed;
    void Update()
    {
        Debug.Log(this.transform.position);
        RectTransform myRectTransform = GetComponent<RectTransform>();
        myRectTransform.localPosition += Vector3.down * speed * Time.deltaTime;
        if(myRectTransform.localPosition.y < -200)
        {
            this.gameObject.active = false;
        }
    }

}
