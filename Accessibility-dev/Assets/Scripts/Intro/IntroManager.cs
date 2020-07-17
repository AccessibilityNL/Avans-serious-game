using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public CurtainManager CurtainManager;
    public Canvas Canvas;
    public TextAsset Script;

    private int counter = 0;
    private IList<IntroAction> introActions;
    // Start is called before the first frame update
    void Start()
    {
        ProcessScript();
        Next();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Next()
    {
        var introAction = introActions.ElementAtOrDefault(counter);
        counter++;
        if (introAction != null)
        {
            switch (introAction.Action)
            {
                case "WAIT":
                    StartCoroutine(WaitDuration(introAction.Payload, Next));
                    break;
                case "TEXT":
                    Canvas
                        .GetComponent<IntroTextTyper>()
                        .StartTypingText(introAction.Payload, Next);
                    break;
                case "GAME":
                    //StartGame(gameAction.Payload);
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.Log("End of game script reached");
            CurtainManager.gameObject.SetActive(true);
            CurtainManager.Close();
        }
    }

    public void FinishIntro()
    {
        Debug.Log("finished");
        SceneManager.LoadScene("OverWorld");
    }

    IEnumerator WaitDuration(string payload, Action action = null)
    {
        var duration = float.Parse(payload);
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }
    void ProcessScript()
    {
        introActions = new List<IntroAction>();

        var fileText = Script.text;
        var textSplit = fileText.Split('\n');
        foreach (var line in textSplit)
        {
            introActions.Add(ProcessIntroAction(line));
        }
    }
    IntroAction ProcessIntroAction(string line)
    {
        var lineSplit = line.Split(new char[] { '\t' }, 2);
        var introAction = new IntroAction
        {
            Action = lineSplit[0]
        };
        if (lineSplit.Length == 2) introAction.Payload = lineSplit[1];
        return introAction;
    }
    class IntroAction
    {
        public string Action { get; set; }
        public string Payload { get; set; }
    }
}
