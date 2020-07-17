using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CurtainManager : MonoBehaviour
{
    public bool OpenOnSpace = false;
    [SerializeField]
    private RectTransform leftCurtain;
    [SerializeField]
    private RectTransform rightCurtain;
    [SerializeField]
    private float timeUntillOpen;
    [SerializeField]
    private float openingTime;
    [SerializeField]
    private float closingTime;
    [SerializeField]
    private GameObject[] elementsToDisableOnOpen;
    [SerializeField]
    private GameObject[] elementsToEnableOnClose;
    [SerializeField]
    private UnityEvent eventsOnOpen;
    [SerializeField]
    private UnityEvent eventsOnClose;
    private bool curtainsHaveOpened = false;
    private void Start()
    {
        if(!OpenOnSpace)
        {
            StartCoroutine(Open());
        }
        var myTransform = GetComponent<RectTransform>();
        if (myTransform)
        {
            leftCurtain.sizeDelta = new Vector2(myTransform.rect.width / 2, leftCurtain.sizeDelta.y);
            rightCurtain.sizeDelta = new Vector2(myTransform.rect.width / 2, rightCurtain.sizeDelta.y);
        }
    }

    private void Update()
    {
        if (OpenOnSpace && !curtainsHaveOpened && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Open());
            curtainsHaveOpened = true;
        }
    }

    private IEnumerator Open()
    {
        yield return new WaitForSeconds(timeUntillOpen);
        foreach (var obj in elementsToDisableOnOpen)
        {
            obj.SetActive(false);
        }
        eventsOnOpen.Invoke();
        yield return null;
        var timePassed = 0f;
        while (timePassed < openingTime)
        {
            var movePerTime = leftCurtain.sizeDelta.x / openingTime * Time.deltaTime;
            leftCurtain.anchoredPosition -= new Vector2(movePerTime, 0);
            rightCurtain.anchoredPosition += new Vector2(movePerTime, 0);
            yield return null;
            timePassed += Time.deltaTime;
        }
        leftCurtain.anchoredPosition = new Vector2(-leftCurtain.sizeDelta.x, 0);
        rightCurtain.anchoredPosition = new Vector2(leftCurtain.sizeDelta.x, 0);
    }

    public void Close()
    {
        Debug.Log("closing curtains");
        StartCoroutine(CloseAsync());
    }

    private IEnumerator CloseAsync()
    {
        var timePassed = 0f;
        while (timePassed < closingTime)
        {
            var movePerTime = leftCurtain.sizeDelta.x / openingTime * Time.deltaTime;
            leftCurtain.anchoredPosition += new Vector2(movePerTime, 0);
            rightCurtain.anchoredPosition -= new Vector2(movePerTime, 0);
            if (rightCurtain.anchoredPosition.x <= 0 || leftCurtain.anchoredPosition.x >= 0)
            {
                leftCurtain.anchoredPosition = new Vector3(0, leftCurtain.anchoredPosition.y);
                rightCurtain.anchoredPosition = new Vector3(0, rightCurtain.anchoredPosition.y);
                break;
            }
            yield return null;
            timePassed += Time.deltaTime;
        }
        leftCurtain.anchoredPosition = new Vector3(0, leftCurtain.anchoredPosition.y);
        rightCurtain.anchoredPosition = new Vector3(0, rightCurtain.anchoredPosition.y);
        foreach (var obj in elementsToEnableOnClose)
        {
            obj.SetActive(true);
        }
        eventsOnClose.Invoke();
    }
}
