using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroTextTyper : MonoBehaviour
{
    public Image TextFieldBorder;
    public Image TextFieldInnerBorder;
    public TextMeshProUGUI TextField;

    public float TextFinishedDelay = 4f;
    public float Delay = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTypingText(string text, Action action = null)
    {
        StartCoroutine(TypeText(text, action));
    }
    IEnumerator TypeText(string text, Action action = null)
    {
        var currentText = "";
        foreach (var letter in text)
        {
            currentText += letter;
            TextField.text = currentText;
            yield return new WaitForSeconds(Delay);
        }
        yield return new WaitForSeconds(TextFinishedDelay);
        action?.Invoke();
    }
}
