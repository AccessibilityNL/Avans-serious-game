using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public GameManager GameManager;
    public Material Normal;
    public Material Hovering;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRaycastHit()
    {
        if (GameManager.GetComponent<GameManager>().GameOn) GetComponent<Renderer>().material = Hovering;
        else GetComponent<Renderer>().material = Normal;
    }

    public void OnRayCastMiss()
    {
        GetComponent<Renderer>().material = Normal;
    }

    public void OnClicked()
    {
        if (GameManager.GetComponent<GameManager>().GameOn)
        {
            var objectName = GetComponent<MeshFilter>().sharedMesh.name;
            StartCoroutine(GameManager.GetComponent<GameManager>().OnClickableClicked(objectName));
        }
    }

    //void OnMouseDown()
    //{
    //    if(GameManager.GetComponent<GameManager>().GameOn)
    //    {
    //        var objectName = GetComponent<MeshFilter>().sharedMesh.name;
    //        StartCoroutine(GameManager.GetComponent<GameManager>().OnClickableClicked(objectName));
    //    }
    //}
    //void OnMouseExit()
    //{
    //    GetComponent<Renderer>().material = Normal;
    //}
    //void OnMouseOver()
    //{
    //    if(GameManager.GetComponent<GameManager>().GameOn) GetComponent<Renderer>().material = Hovering;
    //    else GetComponent<Renderer>().material = Normal;
    //}
}
