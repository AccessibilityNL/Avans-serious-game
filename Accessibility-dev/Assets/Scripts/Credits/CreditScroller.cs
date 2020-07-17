using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private TextMeshProUGUI text = null;
    private void Start()
    {
        var sceneLoader = FindObjectOfType<SceneLoader>();
        if (sceneLoader)
            text.text = "Points: \n" + sceneLoader.GetPointPrintout() + "\n" + text.text;
    }
    void Update()
    {
        transform.position += new Vector3(0, scrollSpeed * Time.deltaTime, 0);
    }
}
