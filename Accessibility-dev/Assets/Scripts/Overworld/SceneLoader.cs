using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsToUnload;
    [SerializeField]
    private string mySceneName;
    private string previousScene = null;
    [SerializeField]
    private List<LevelCompleted> levelCompletedList;

    public void ChangeToScene(string sceneName)
    {
        Debug.LogError("Do not use this function, it is depricated");
        StartCoroutine(ChangeToSceneAsync(sceneName, "", -1));
    }
    public void ChangeToScene(string sceneName, string category, int points)
    {
        StartCoroutine(ChangeToSceneAsync(sceneName, category, points));
    }

    private IEnumerator ChangeToSceneAsync(string sceneName, string category, int points)
    {

        if (!mySceneName.Equals(sceneName))
        {
            var levelCompleted = GetLevelFromName(category);
            if (levelCompleted != null)
            {
                levelCompleted.points += points;
            }
            else
            {
                Debug.LogError("Given category is not correct. Must be dyslexia, hearing, visual or mobility");
            }
            var loader = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            Cursor.lockState = CursorLockMode.None;
            while (!loader.isDone)
            {
                yield return null;
            }
            if (previousScene != null)
                SceneManager.UnloadSceneAsync(previousScene);
            foreach (var obj in objectsToUnload)
            {
                obj.SetActive(false);
            }
            previousScene = sceneName;
        }
    }
    public void BackToOverworld(string from)
    {
        Debug.LogError("Do not use this function, it is depricated");
        BackToOverworld(from, 0);
    }
    public void BackToOverworld(string from, int points)
    {
        var complededEverything = true;
        Debug.Log(from);
        if (!from.Equals("pause"))
        {
            foreach (var lc in levelCompletedList)
            {
                if (lc.levelName.Equals(from))
                {
                    Debug.Log(from);
                    lc.gameObject.SetActive(true);
                    lc.points += points;
                }
                complededEverything = complededEverything && lc.gameObject.activeSelf;
            }
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            complededEverything = false;
            foreach (var lc in levelCompletedList)
            {
                if (!lc.gameObject.activeSelf)
                {
                    lc.points = 0;
                }
            }
        }
        if (previousScene != null)
            SceneManager.UnloadSceneAsync(previousScene);
        if (complededEverything)
        {
            SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
        }
        else
        {
            foreach (var obj in objectsToUnload)
            {
                obj.SetActive(true);
            }
            previousScene = null;
        }
    }

    private LevelCompleted GetLevelFromName(string name)
    {
        foreach (var lc in levelCompletedList)
        {
            if (lc.levelName.Equals(name))
            {
                return lc;
            }
        }
        return null;
    }

    public string GetPointPrintout()
    {
        var points = "";
        foreach (var lc in levelCompletedList)
        {
            var name = char.ToUpper(lc.levelName[0]) + lc.levelName.Substring(1);
            points += name + ": " + lc.points + " / " + lc.maxPoints + "\n";
        }
        return points;
    }
}
[Serializable]
class LevelCompleted
{
    [SerializeField]
    public string levelName;
    [SerializeField]
    public GameObject gameObject;
    [SerializeField]
    public int maxPoints = 100;
    public int points = 0;
}