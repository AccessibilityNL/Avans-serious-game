using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultipleChoiceManager : MonoBehaviour
{
    [SerializeField]
    private string nextScene;
    [SerializeField]
    private Button rightAwnser;
    [SerializeField]
    private Color colorRightAwnser;
    [SerializeField]
    private Color colorWrongAwnser;
    [SerializeField]
    private int points = 25;
    [SerializeField]
    private string category = "dyslexia";
    private Button[] myButtons;
    SceneLoader sceneLoader;

    private void Start()
    {
        myButtons = GetComponentsInChildren<Button>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (!sceneLoader)
        {
            Debug.LogWarning("Won't load next scene because sceneloader was not found");
        }
    }

    public void RightAwnser()
    {
        DisableAndColorButtons();
        Debug.Log("Right awnser");
        StartCoroutine(NextScene(true));
    }

    public void WrongAwnser()
    {
        DisableAndColorButtons();
        Debug.Log("Wrong awnser");
        StartCoroutine(NextScene(false));
    }

    public IEnumerator NextScene(bool correct)
    {
        yield return new WaitForSeconds(3);
        if (sceneLoader)
        {
            if (nextScene.Equals("overworld"))
                sceneLoader.BackToOverworld("dyslexia", correct?points:0);
            else
                sceneLoader.ChangeToScene(nextScene, category, points);
        }
    }

    private void DisableAndColorButtons()
    {
        foreach (var b in myButtons)
        {

            var colorBlock = new ColorBlock();
            colorBlock.colorMultiplier = 1f;
            if (b == rightAwnser)
            {
                colorBlock.disabledColor = colorRightAwnser;
            }
            else
            {
                colorBlock.disabledColor = colorWrongAwnser;
            }
            b.colors = colorBlock;
            b.interactable = false;
        }
    }
}
