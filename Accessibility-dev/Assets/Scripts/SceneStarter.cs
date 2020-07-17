using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneStarter : MonoBehaviour
{
    [SerializeField]
    private GameObject startScreen;
    [SerializeField]
    private float timeUntillEnableObjects;
    [SerializeField]
    private GameObject[] objectsToEnable;
    [SerializeField]
    private UnityEvent eventsAfterStart;

    private void Start()
    {
        StartCoroutine(EnableAndDisableAfterTime());
    }

    private IEnumerator EnableAndDisableAfterTime()
    {
        yield return new WaitForSeconds(timeUntillEnableObjects);
        if(startScreen)
            startScreen.SetActive(false);
        foreach (var obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
        eventsAfterStart.Invoke();
    }
}
