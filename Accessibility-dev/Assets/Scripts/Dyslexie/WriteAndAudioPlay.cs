
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteAndAudioPlay : MonoBehaviour
{
    [SerializeField]
    private AudioSource myAudioSource;
    [SerializeField]
    private InputField iField;
    [SerializeField]
    private List<string> goodWords = new List<string>();
    [SerializeField]
    private List<string> dyslexiaWords = new List<string>();
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private string anwser;
    [SerializeField]
    private Color colorRightAwnser;
    [SerializeField]
    private Color colorWrongAwnser;

    [SerializeField]
    private CurtainManager curtain;
    private SceneLoader sceneLoader;

    private Coroutine currentAudioCoroutine;

    void Start()
    {
        currentAudioCoroutine = StartCoroutine(ChangeText());
        nextButton.interactable = false;
        sceneLoader = FindObjectOfType<SceneLoader>();
        nextButton.onClick.AddListener(CheckAnwser);
    }


    IEnumerator ChangeText()
    {
        yield return new WaitForSeconds(myAudioSource.clip.length + 1);

        string input = iField.text;
        for (int i = 0; i < goodWords.Count; i++)
        {
            input = input.Replace(goodWords[i], dyslexiaWords[i]);
        }
        iField.text = input;
        nextButton.interactable = true;
    }

    private void CheckAnwser()
    {
        //stupid spacing and comma mistakes don't get checked
        var checkAnwser = anwser.Replace(" ", "").Replace(".", "").Replace(",", "").ToLower();
        var givenAwnser = iField.text.Replace(" ", "").Replace(".", "").Replace(",", "").ToLower();
        if (checkAnwser.Equals(givenAwnser))
        {
            var colorBlock = new ColorBlock
            {
                colorMultiplier = 1f,
                disabledColor = colorRightAwnser
            };
            nextButton.colors = colorBlock;
            nextButton.interactable = false;
            StartCoroutine(NextScene());
        }
        else
        {
            var colorBlock = new ColorBlock
            {
                colorMultiplier = 1f,
                disabledColor = colorWrongAwnser
            };
            nextButton.colors = colorBlock;
            nextButton.interactable = false;
            StartCoroutine(NextScene());
        }
    }

    public IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3);
        Done();
    }

    private void Done()
    {
        curtain.gameObject.SetActive(true);
        curtain.Close();
    }

    public void ChangeBackToOverworld()
    {
        StartCoroutine(GoBackAfterWating());
    }

    private IEnumerator GoBackAfterWating()
    {
        yield return new WaitForSeconds(2);
        sceneLoader.BackToOverworld("dyslexia");
    }

    public void StartAudio()
    {
        myAudioSource.Play();
        StopCoroutine(currentAudioCoroutine);
        currentAudioCoroutine = StartCoroutine(ChangeText());
    }
}