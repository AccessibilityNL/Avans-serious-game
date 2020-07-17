using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BasicSceneChanger : MonoBehaviour
{
    [SerializeField]
    private string nextScene;
    [SerializeField]
    private string category = "visual";
    private SceneLoader sceneLoader;
    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (!sceneLoader)
        {
            Debug.LogWarning("Not going to transition to next scene, because cannot find SceneLoader");
        }
    }

    public void NextScene()
    {
        if (sceneLoader)
            StartCoroutine(DoAfterSeconds(() => sceneLoader.ChangeToScene(nextScene)));
    }
    public void NextScene(int points)
    {
        Debug.LogError("Do not use this function, it is depricated");
        if (sceneLoader)
            StartCoroutine(DoAfterSeconds(() => sceneLoader.ChangeToScene(nextScene, category, points)));
    }
    private IEnumerator DoAfterSeconds(Action afterDone)
    {
        yield return new WaitForSeconds(0.5f);
        afterDone.Invoke();
    }

    public void BackToOverworld()
    {
        Debug.LogError("Do not use this function, it is depricated");
        if (sceneLoader)
            StartCoroutine(DoAfterSeconds(() => sceneLoader.BackToOverworld(category)));
    }
    public void BackToOverworld(int points)
    {
        if (sceneLoader)
            StartCoroutine(DoAfterSeconds(() => sceneLoader.BackToOverworld(category, points)));
    }
}
