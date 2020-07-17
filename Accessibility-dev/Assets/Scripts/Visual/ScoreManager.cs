using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static int score;
    [SerializeField]
    private BoxCollider[] gameObjects;
    [SerializeField]
    private GameObject MelonOnePice;
    [SerializeField]
    private GameObject Knife;
    [SerializeField]
    private GameObject Line;
    [SerializeField]
    private Text CountDown;
    [SerializeField]
    private float startingtime = 5f;
    private float currenttime = 0f;

    [SerializeField]
    private GameObject MelonSliceOne;
    [SerializeField]
    private GameObject MelonSliceTwo;

    [SerializeField]
    private CurtainManager curtain;
    private SceneLoader sceneLoader;
    [SerializeField] 
    private string nextScene;

    private bool done = false;

    private void Start()
    {
        currenttime = startingtime;
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (!sceneLoader)
            Debug.LogWarning("Not going to load next scene because SceneLoader was not found");
    }
    private void Update()
    {
        currenttime -= 1 * Time.deltaTime;
        CountDown.text = "Time left: " + currenttime.ToString("0");

        if(currenttime <= 0 && !done)
        {
            currenttime = 0;
            Done();
        }
        if (score == gameObjects.Length)
        {
            ChangeFruit();
            score += 8;
            IncreaceScore();//bad fix, but else you are calling the change fruit function a million times
        }
    }

    public static void IncreaceScore()
    {
        score++;
    }

    private void ChangeFruit()
    {
        MelonOnePice.SetActive(false);
        //Line.SetActive(false);
        Knife.SetActive(false);

        MelonSliceOne.SetActive(true);
        MelonSliceTwo.SetActive(true);

        Done();
    }

    public void EndGame()
    {
        StartCoroutine(NextScene(score));
    }

    public IEnumerator NextScene(int points)
    {
        yield return new WaitForSeconds(2);
        if (sceneLoader)
            sceneLoader.BackToOverworld("visual", points);
        
    }

    private void Done()
    {
        done = true;
        curtain.gameObject.SetActive(true);
        curtain.Close();
    }
}
