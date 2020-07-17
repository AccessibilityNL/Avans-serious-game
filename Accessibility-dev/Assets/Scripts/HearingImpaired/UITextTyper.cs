using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextTyper : MonoBehaviour
{
    public GameObject GameManager;
    public Image TextFieldBorder;
    public Image TextFieldInnerBorder;
    public TextMeshProUGUI TextField;

    public float TextFinishedDelay = 4f;
    public float Delay = 0.05f; 
    // Start is called before the first frame update
    void Start()
    {
        TextField.enabled = false;
        TextFieldBorder.enabled = false;
        TextFieldInnerBorder.enabled = false;
    }

    public void StartTypingText(string text, Action action = null)
    {
        StartCoroutine(TypeText(text, action));
    }
    public void StartChangeState(bool state, Action action = null)
    {
        ChangeState(state, action);
    }
    void ChangeState(bool state, Action action = null)
    {
        TextField.enabled = state;
        TextFieldBorder.enabled = state;
        TextFieldInnerBorder.enabled = state;
        //yield return new WaitForSeconds(0.1f);
        action?.Invoke();

    }
    IEnumerator TypeText(string text, Action action = null)
    {
        var currentText = "";
        foreach(var letter in text)
        {
            currentText += letter;
            TextField.text = currentText;
            yield return new WaitForSeconds(Delay);
        }
        yield return new WaitForSeconds(TextFinishedDelay);
        action?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
