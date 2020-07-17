using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAndAudioPlay : MonoBehaviour
{
    [SerializeField]
    private AudioSource myAudioSource;
    [SerializeField]
    private Text questionDisplay;
    [SerializeField]
    private string input = "Dit is een test audio opnamen, let niet op de opnamen....";
    [SerializeField]
    private CurtainManager curtainManager;

    void Start()
    {
        questionDisplay.text = input;
        StartCoroutine(CloseAfterAudioFinished());
    }

    IEnumerator CloseAfterAudioFinished()
    {
        yield return new WaitForSeconds(myAudioSource.clip.length);
        curtainManager.gameObject.SetActive(true);
        curtainManager.Close();
    }
}
