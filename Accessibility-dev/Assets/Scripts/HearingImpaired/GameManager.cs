using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BasicSceneChanger basicSceneChanger;
    [SerializeField] private CurtainManager curtainManager;
    public Canvas Canvas;
    public TextAsset Script;
    public AudioSource AudioSource;
    public int GameCount = 1;

    private IDictionary<string, string> translations;
    private IList<GameAction> gameActions;
    private IList<AudioClip> audioClips;
    private int score = 0;
    private int counter = 0;
    private int gameCounter = 0;
    private int correctAnswered = 0;
    private int maxAnswers = 0;

    private string correctObjectName;
    public bool GameOn { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        translations = new Dictionary<string, string> { 
            ["Laptop_On"] = "Laptop",
            ["Tablet"] = "Tablet",
            ["Briefcase"] = "Aktetas",
            ["Chair_Conference"] = "Stoel",
            ["PlantPotLargeA"] = "Plantenpot",
            ["Umbrella_Closed"] = "Paraplu"
        };
        ProcessScript();
        Next();
    }

    public void Next()
    {
        var gameAction = gameActions.ElementAtOrDefault(counter);
        counter++;
        if (gameAction != null)
        {
            switch (gameAction.Action)
            {
                case "WAIT":
                    StartCoroutine(WaitDuration(gameAction.Payload, Next));
                    break;
                case "TEXT":
                    Canvas
                        .GetComponent<UITextTyper>()
                        .StartTypingText(gameAction.Payload, Next);
                    break;
                case "SHOW":
                    Canvas.GetComponent<UITextTyper>().StartChangeState(true, Next);
                    break;
                case "HIDE":
                    Canvas.GetComponent<UITextTyper>().StartChangeState(false, Next);
                    break;
                case "VOLUME":
                    SetVolume(gameAction.Payload, Next);
                    break;
                case "GAME":
                    StartGame(gameAction.Payload);
                    break;
                default:
                    break;
            }
        } else
        {
            Debug.Log("End of game script reached");
            score = (correctAnswered / maxAnswers) * (1/4);

            curtainManager.gameObject.SetActive(true);
            curtainManager.Close();
        }
    }

    public void FinishGame()
    {
        basicSceneChanger.BackToOverworld(score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitDuration(string payload, Action action = null)
    {
        var duration = float.Parse(payload);
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }
    void SetVolume(string payload, Action action = null)
    {
        AudioSource.volume = float.Parse(payload);
        Debug.Log(float.Parse(payload));
        action?.Invoke();
    }
    void StartGame(string category)
    {
        gameCounter = 0;
        maxAnswers += GameCount;
        audioClips = Resources.LoadAll<AudioClip>($"Audio/HearingImpaired/{category.Trim()}").ToList();
        Game();
    }

    void Game()
    { 
        while(true)
        {
            var objects = translations.Keys;
            var randomIndex = new System.Random().Next(0, objects.Count);
            var newObjectName = translations.Keys.ElementAt(randomIndex);
            if (newObjectName.Equals(correctObjectName, StringComparison.OrdinalIgnoreCase)) continue;
            else 
            { 
                correctObjectName = newObjectName; 
                break; 
            }
        }
        var audioClip = audioClips.FirstOrDefault(clip => clip.name.Equals(correctObjectName, StringComparison.OrdinalIgnoreCase));

        AudioSource.PlayOneShot(audioClip);

        GameOn = true;
    }
    public IEnumerator OnClickableClicked(string objectName)
    {
        gameCounter++;
        Canvas.GetComponent<UITextTyper>().StartChangeState(true);
        if (objectName.Equals(correctObjectName, StringComparison.OrdinalIgnoreCase))
        {
            correctAnswered++;
            Canvas.GetComponent<UITextTyper>().StartTypingText("Correct");
        } else
        {
            Canvas.GetComponent<UITextTyper>().StartTypingText($"Helaas, het moest zijn {translations[correctObjectName]}");
        }
        GameOn = false;
        yield return new WaitForSeconds(4f);
        Canvas.GetComponent<UITextTyper>().StartChangeState(false);
        if (gameCounter == GameCount) Next();
        else Game();
    }
    void ProcessScript()
    {
        gameActions = new List<GameAction>();

        var fileText = Script.text;
        var textSplit = fileText.Split('\n');
        foreach(var line in textSplit)
        {
            gameActions.Add(ProcessGameAction(line));
        }
    }

    GameAction ProcessGameAction(string line)
    {
        var lineSplit = line.Split(new char[] { '\t' }, 2);
        var gameAction = new GameAction
        {
            Action = lineSplit[0]
        };
        if (lineSplit.Length == 2) gameAction.Payload = lineSplit[1];
        return gameAction;
    }
    class GameAction
    {
        public string Action { get; set; }
        public string Payload { get; set; }
    }
}
