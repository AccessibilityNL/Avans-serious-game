using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitCountingManager : MonoBehaviour
{
    [SerializeField] private Destroyer destroyer;
    [SerializeField] private CurtainManager curtainManager;
    [SerializeField] private InputField inputField;
    [SerializeField] private string nextScene;
    [SerializeField] private Text gameCompletedText;
    private BoxSpawner currentSpawner;
    private string selectedFruit = "Apple";
    private SceneLoader sceneLoader;
    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (!sceneLoader)
            Debug.LogWarning("Not going to load next scene because SceneLoader was not found");
    }


    public void OnDoneSpawning(BoxSpawner spawner)
    {
        StartCoroutine(CheckEverythingSpawned(spawner));
        currentSpawner = spawner;
    }

    private IEnumerator CheckEverythingSpawned(BoxSpawner spawner)
    {
        while (spawner.GetAmountSpawned() > destroyer.GetAmountDestroyed())
        {
            yield return new WaitForSeconds(0.2f);
        }
        curtainManager.gameObject.SetActive(true);
        //very ugly way of doing this, but it works
        curtainManager.gameObject.transform.parent.gameObject.SetActive(true);
        yield return null;
        curtainManager.Close();
    }

    public void SubmitAwnser()
    {
        var amount = int.Parse(inputField.text);
        var actualAmount = currentSpawner.GetAmountSpawnedOf(selectedFruit);
        gameCompletedText.gameObject.SetActive(true);
        int points;
        if (actualAmount == amount)
        {
            gameCompletedText.text = "Correct!";
            points = 20;
        }
        else
        {
            gameCompletedText.text = "Incorrect het was eigenlijk: " + actualAmount;
            if (Math.Abs(actualAmount - amount) == 1)
                points = 10;
            else
                points = 0;
        }
        StartCoroutine(ChangeToNextScene(points));
    }

    private IEnumerator ChangeToNextScene(int points)
    {
        yield return new WaitForSeconds(2f);
        if (sceneLoader)
            sceneLoader.ChangeToScene(nextScene, "visual", points);
    }

}
