using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChangingWords : MonoBehaviour
{
    [SerializeField]
    private Text questionDisplay;
    [SerializeField]
    private int secondsToWait = 5;
    [SerializeField]
    private int howManyTimes = 20;
    [SerializeField]
    [TextArea]
    private string text1 = "jij hebt een leesprobleem!";
    [SerializeField]
    [TextArea]
    private string text2 = "jij hebt een leerprobleem!";

    void Start()
    {
        text1.Replace("\\n", "\n");
        text2.Replace("\\n", "\n");
        questionDisplay.text = text1;
        StartCoroutine(ChangeText());
    }


    IEnumerator ChangeText()
    {
       for(int i = 0; i < howManyTimes; i++)
        {
            yield return new WaitForSeconds(secondsToWait);
            if (questionDisplay.text == text1)
            {
                questionDisplay.text = text2;
            }
            else
            {
                questionDisplay.text = text1;
            }
        }
    }


}
