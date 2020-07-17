using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private MoleManager mm;
    public GameObject description;
    void Start()
    {
        mm = GetComponent<MoleManager>();
        GameObject camera = GameObject.Find("Main Camera");
        Behaviour script = (Behaviour) camera.GetComponent("MoleManager");
        if(script)
        script.enabled = false;
    }

    public void PrintTest()
    {
        print("Clicked");
    }
    public void StartGame()
    {
        GameObject button = GameObject.Find("Play");
        button.SetActive(false);
        description.gameObject.SetActive(false);
        GameObject camera = GameObject.Find("Main Camera");
        Behaviour script = (Behaviour)camera.GetComponent("MoleManager");
        script.enabled = true;
    }

    public void StartDelayed()
    {
        StartCoroutine("StartDelayedRoutine", 2);
    }

    IEnumerator StartDelayedRoutine(float time)
    {
        //Function for curtains
        yield return new WaitForSeconds(time);
        StartGame();
        yield return null;
    }
}
