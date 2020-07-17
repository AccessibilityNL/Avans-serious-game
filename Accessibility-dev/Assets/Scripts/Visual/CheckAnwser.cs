using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CheckAnwser : MonoBehaviour
{
    [SerializeField]
    private string nextScene;
    [SerializeField]
    private Image fistImage;
    [SerializeField]
    private Text oparation;
    [SerializeField]
    private Image secondImage;
    [SerializeField]
    private InputField iField;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Color colorRightAwnser;
    [SerializeField]
    private Color colorWrongAwnser;
    private int anwser;
    private bool isAnswered;
    private SceneLoader sceneLoader;
    private int points;
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (!sceneLoader)
            Debug.LogWarning("Not going to load next scene because SceneLoader was not found");

        isAnswered = false;
        nextButton.interactable = false;
        
        Button btn = nextButton.GetComponent<Button>();
        btn.onClick.AddListener(checkAnwser);
    }

    private void Update()
    {
        if (iField.text != "" && !isAnswered)
        {
            nextButton.interactable = true;
        }
    }

    private void checkAnwser()
    {
        isAnswered = true;
        calculateAnwser();

        int inputAnwser = int.Parse(iField.text);
        

        if (anwser.Equals(inputAnwser))
        {
            Debug.Log("Good anwser");
            var colorBlock = new ColorBlock();
            colorBlock.colorMultiplier = 1f;
            colorBlock.disabledColor = colorRightAwnser;
            nextButton.colors = colorBlock;
            nextButton.interactable = false;
            if (Math.Abs(anwser - inputAnwser) == 1)
                points = 10;
            else
                points = 0;
        }
        else
        {
            Debug.Log("worng anwser");
            // change collor of button
            var colorBlock = new ColorBlock();
            colorBlock.colorMultiplier = 1f;
            colorBlock.disabledColor = colorWrongAwnser;
            nextButton.colors = colorBlock;
            nextButton.interactable = false;
            points = 20;
        }
    }

    public void EndGame()
    {
        StartCoroutine(ChangeToNextScene(points));
    }

    private void calculateAnwser()
    {
        int firstNumber = int.Parse(fistImage.sprite.name);
        int secondtNumber = int.Parse(secondImage.sprite.name);
        if (oparation.text.Equals("+"))
        {
            anwser = firstNumber + secondtNumber;
        }
        else
        {
            anwser = firstNumber - secondtNumber;
        }

    }
    private IEnumerator ChangeToNextScene(int points)
    {
        yield return new WaitForSeconds(2f);
        if (sceneLoader)
            sceneLoader.ChangeToScene(nextScene, "visual", points);
    }
    //public IEnumerator NextScene()
    //{
    //    yield return new WaitForSeconds(3);
    //    SceneManager.LoadScene(nextScene);
    //}
}
