using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumGameManager : MonoBehaviour
{
    [SerializeField] DrumController drumController;
    [SerializeField] DrumGameUIManager drumGameUIManager;
    [SerializeField] CurtainManager curtainManager;
 
    private GameObject[] sequencePlayedByPlayer;
    private GameObject[] sequenceToBePlayed;

    private bool isCorrect = true;
    private bool isPlayerPlayingRound;
    private int currentRound = 1;
    private int i; //currentNoteWithinPlayerSequence
    private int points = 0;

    /* Start is called on the very first frame */
    private void Start()
    {

    }

    /* Update is called every consecutive frame */
    void Update()
    {
        if (!drumController.GetIsCPUPlayingRound())
        {
            if (!isPlayerPlayingRound)
            {
                //Keep track of the game phase
                this.isPlayerPlayingRound = true;

                //Retrieve the sequence to be played
                sequenceToBePlayed = drumController.GetDrumSequence();

                //Build list of drums played by user
                sequencePlayedByPlayer = new GameObject[sequenceToBePlayed.Length];

            }

            else if (i < sequencePlayedByPlayer.Length)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Debug.Log("Player plays the blue drum");
                    GameObject drum = drumController.GetSpecificDrum("Blue");
                    drumController.PlayDrum(drum);
                    sequencePlayedByPlayer[i] = drum;
                    i++;
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    Debug.Log("Player plays the green drum");
                    GameObject drum = drumController.GetSpecificDrum("Green");
                    drumController.PlayDrum(drum);
                    sequencePlayedByPlayer[i] = drum;
                    i++;
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    Debug.Log("Player plays the red drum");
                    GameObject drum = drumController.GetSpecificDrum("Red");
                    drumController.PlayDrum(drum);
                    sequencePlayedByPlayer[i] = drum;
                    i++;
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("Player plays the yellow drum");
                    GameObject drum = drumController.GetSpecificDrum("Yellow");
                    drumController.PlayDrum(drum);
                    sequencePlayedByPlayer[i] = drum;
                    i++;
                }
            }

            else
            {
                //Reset round state
                isPlayerPlayingRound = false;

                //Compare CPU sequence & player sequence
                for (int i = 0; i < sequenceToBePlayed.Length; i++)
                {
                    if (sequencePlayedByPlayer[i].Equals(sequenceToBePlayed[i]))
                    {
                        Debug.Log("Note " + i + " was correct!");
                        if (i == (sequenceToBePlayed.Length - 1))
                        {
                            if (currentRound == 1)
                            {
                                points += 3;
                            }
                            else if (currentRound == 2)
                            {
                                points += 7;
                            }
                            else 
                            {
                                points += 10;
                            }
                            Debug.Log("Sequence CORRECT!");
                        }
                    }
                    else 
                    {
                        Debug.Log("Player was incorrect at note " + i);
                        isCorrect = false;
                    }
                }

                //Show win/loss
                if (isCorrect)
                {
                    drumGameUIManager.EngageWinOrLossUI("Well done!");
                }
                else 
                {
                    drumGameUIManager.EngageWinOrLossUI("Too bad!");
                    isCorrect = true;
                }

                //Start new round
                currentRound++;
                i = 0;
                StartRound(currentRound);
            }
        }
    }

    /* StartAfterCurtains is called when the curtains open */
    public void StartAfterCurtains()
    {
        StartRound(currentRound);
    }

    public void StartRound(int roundNumber)
    {
        switch (roundNumber)
        {
            case 1:
                drumController.PlayRound(3);
                break;

            case 2:
                drumController.SetIsCPUPlayingRound(true);
                drumController.PlayRound(4);
                break;

            case 3:
                drumController.PlayRound(5);
                drumController.SetIsCPUPlayingRound(true);
                break;

            default:
                curtainManager.gameObject.SetActive(true);
                curtainManager.Close();
                break;
        }
    }

    public void FinishGame()
    {
        var sceneLoader = FindObjectOfType<SceneLoader>();

        if (sceneLoader)
        {
            sceneLoader.ChangeToScene("CarGame", "visual", points);
        }
    }
}
