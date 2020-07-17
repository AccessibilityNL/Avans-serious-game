using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class TextFastSentince : MonoBehaviour
{
    [SerializeField]
    private Text questionDisplay;
    [SerializeField]
    private float secondsToWait = 5f;
    [SerializeField]
    private int howManyTimes = 5;
    [SerializeField]
    private string input = "Dit is een faketekst. Alles wat hier staat is slechts om een indruk te geven van het grafische effect van tekst op deze plek. Wat u hier leest is een voorbeeldtekst. Deze wordt later vervangen door de uiteindelijke tekst, die nu nog niet bekend is. De faketekst is dus een tekst die eigenlijk nergens over gaat. Het grappige is, dat mensen deze toch vaak lezen. Zelfs als men weet dat het om een faketekst gaat, lezen ze toch door.";
    [SerializeField]
    private CurtainManager curtainManager;

    void Start()
    {
        string[] sentences = Regex.Split(input, @"(?<=[\.!\?])\s+");
        StartCoroutine(ChangeText(sentences));
    }


    IEnumerator ChangeText(string[] sentences)
    {
        for (int i = 0; i < sentences.Length; i++)
        {
            yield return new WaitForSeconds(secondsToWait);
            questionDisplay.text = sentences[i];
        }
        curtainManager.gameObject.SetActive(true);
        curtainManager.Close();
    }
}