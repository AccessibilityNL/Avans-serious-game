using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDoneTimer : MonoBehaviour
{
    [SerializeField]
    private float timeUntilDone = 30;
    [SerializeField]
    private CurtainManager curtainManager;
    [SerializeField]
    private Text timeText;
    void Start()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        while (timeUntilDone > 0)
        {
            yield return new WaitForSeconds(1);
            timeUntilDone--;
        }
        curtainManager.gameObject.SetActive(true);
        curtainManager.Close();
    }

    void Update()
    {
        timeText.text = timeUntilDone.ToString();
    }
}
