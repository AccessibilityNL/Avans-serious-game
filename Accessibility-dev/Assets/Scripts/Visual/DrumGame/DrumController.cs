using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DrumController : MonoBehaviour
{
    
    [SerializeField] private float timeBetweenDrums;
    [SerializeField] private GameObject blueDrum;
    [SerializeField] private GameObject greenDrum;
    [SerializeField] private GameObject redDrum;
    [SerializeField] private GameObject yellowDrum;

    private GameObject[] drums;
    private AudioSource audioSource;
    private GameObject[] drumSequence;
    private bool isCPUPlayingRound = true;
    Random random;

    // Start is called before the first frame update
    void Start()
    {
        /* Set up the list of drums */
        drums = new GameObject[4];
        drums[0] = blueDrum;
        drums[1] = greenDrum;
        drums[2] = redDrum;
        drums[3] = yellowDrum;

        /* Set up new Random for getting a random Drum */
        random = new Random();

    }

    public GameObject GetRandomDrum(GameObject[] drums) {
        int randomIndex = random.Next(drums.Length);
        return drums[randomIndex];
    }

    public void PlayDrum(GameObject drum) {
        if (drum.name == "Blue Drum")
        {
            Debug.Log("Playing blue drum");
            EmphasizeDrum(drum);
        }

        else if (drum.name == "Green Drum")
        {
            Debug.Log("Playing green drum");
            EmphasizeDrum(drum);
        }

        else if (drum.name == "Red Drum")
        {
            Debug.Log("Playing red drum");
            EmphasizeDrum(drum);
        }

        else if (drum.name == "Yellow Drum")
        {
            Debug.Log("Playing yellow drum");
            EmphasizeDrum(drum);
        }

        else 
        {
            Debug.Log("Drum not recognized");
        }
    }

    public void PlayRound(int sequenceLength)
    {
        StartCoroutine(PlayDrumSequence(sequenceLength));
    }

    private IEnumerator PlayDrumSequence(int sequenceLength)
    {
        drumSequence = new GameObject[sequenceLength];
        isCPUPlayingRound = true;

        for (var i = 0; i < sequenceLength; i++)
        {
            yield return new WaitForSeconds(timeBetweenDrums);

            GameObject drum = GetRandomDrum(drums);
            PlayDrum(drum);
            drumSequence[i] = drum;
        }

        isCPUPlayingRound = false;
    }

    public void EmphasizeDrum(GameObject drum)
    {
        StartCoroutine(EmphasizeDrumWhilePlaying(drum));
    }

    private IEnumerator EmphasizeDrumWhilePlaying(GameObject drum)
    {
        Vector3 originalScale = drum.transform.localScale;
        drum.transform.localScale = new Vector3(0.26f, 0.26f, 0.26f);

        audioSource = drum.GetComponent<AudioSource>();
        audioSource.Play();
        
        yield return new WaitForSeconds(1f);
        drum.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    }

    public bool GetIsCPUPlayingRound()
    {
        return isCPUPlayingRound;
    }

    public void SetIsCPUPlayingRound(bool isPlayingRound)
    {
        this.isCPUPlayingRound = isPlayingRound;
    }

    public GameObject[] GetDrumSequence()
    {
        return drumSequence;
    }

    public GameObject GetSpecificDrum(string drumColour)
    {
        switch (drumColour)
        {
            case "Blue":
                return blueDrum;

            case "Green":
                return greenDrum;

            case "Red":
                return redDrum;

            case "Yellow":
                return yellowDrum;

            default:
                Debug.Log("GetDrum() request not recognized");
                return null;
        }
    }
}
