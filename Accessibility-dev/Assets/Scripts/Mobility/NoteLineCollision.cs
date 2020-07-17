using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteLineCollision : MonoBehaviour
{
    public AudioSource errorSound;
    public GameObject right;
    public GameObject left;
    public AudioSource[] notes;
    public Text scoreText;
    public float score = 0;

    string currentNote;
    System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GetRandomArrow", 1.0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        DetectPressedKeyOrButton();
        //TestKeys();
    }

    void GetRandomArrow()
    {
        int currentRandom = rnd.Next(1, 100);

        if (currentRandom <= 30)
        {
            left.SetActive(false);
            right.SetActive(true);
        }
        else if (currentRandom > 30 && currentRandom <= 60)
        {
            right.SetActive(false);
            left.SetActive(true);
        }
        else
        {
            left.SetActive(false);
            right.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        currentNote = col.gameObject.tag;
    }
    void OnTriggerExit(Collider col)
    {
        currentNote = null;
    }

    public void DetectPressedKeyOrButton()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                if(MatchingDirection(kcode) == currentNote)
                {
                    PlayKeySound(MatchingDirection(kcode));
                    score++;
                    scoreText.text = "Score: " + score.ToString();
                }
                else
                {
                    //errorSound.Play();
                }
            }
        }
    }

    public String MatchingDirection(KeyCode kcode)
    {
        //Keycode moet omgezet worden
        //voorbeeld: L note moet K key worden, dus wanneer L note is match K en return L
        if (left.activeInHierarchy)
        {
            switch (kcode)
            {
                case KeyCode.L: return "NoteSemicolon";
                case KeyCode.K: return "NoteL";
                case KeyCode.J: return "NoteK";
                case KeyCode.H: return "NoteJ";
                case KeyCode.G: return "NoteH";
                case KeyCode.F: return "NoteG";
                case KeyCode.D: return "NoteF";
                case KeyCode.LeftShift: return "NoteZ";
                case KeyCode.Z: return "NoteX";
                case KeyCode.X: return "NoteC";
            }
        }
        //voorbeeld: L note moet Semicolon key worden, dus wanneer L note is match Semicolon en return L
        else if (right.activeInHierarchy)
        {
            switch (kcode)
            {
                case KeyCode.Semicolon: return "NoteL";
                case KeyCode.L: return "NoteK";
                case KeyCode.K: return "NoteJ";
                case KeyCode.J: return "NoteH";
                case KeyCode.H: return "NoteG";
                case KeyCode.G: return "NoteF";
                case KeyCode.F: return "NoteD";
                case KeyCode.D: return "NoteS";
                case KeyCode.V: return "NoteC";
                case KeyCode.C: return "NoteX";
                case KeyCode.X: return "NoteZ";
            }
        }
        return "Note"+kcode;
    }

    //Method for sound testing
    public void PlayKeySound(string kcode)
    {
        if (kcode == "NoteL")
        {
            notes[0].Play();
        }
        if (kcode == "NoteK")
        {
            notes[1].Play();
        }
        if (kcode == "NoteG")
        {
            notes[2].Play();
        }
        if (kcode == "NoteJ")
        {
            notes[3].Play();
        }
        if (kcode == "NoteH")
        {
            notes[4].Play();
        }
        if (kcode == "NoteF")
        {
            notes[5].Play();
        }
        if (kcode == "NoteZ")
        {
            notes[6].Play();
        }
        if (kcode == "NoteX")
        {
            notes[7].Play();
        }
        if (kcode == "NoteC")
        {
            notes[8].Play();
        }
    }

    public void TestKeys()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                if (kcode == KeyCode.L)
                {
                    notes[0].Play();
                }
                if (kcode == KeyCode.K)
                {
                    notes[1].Play();
                }
                if (kcode == KeyCode.G)
                {
                    notes[2].Play();
                }
                if (kcode == KeyCode.J)
                {
                    notes[3].Play();
                }
                if (kcode == KeyCode.H)
                {
                    notes[4].Play();
                }
                if (kcode == KeyCode.F)
                {
                    notes[5].Play();
                }
                if (kcode == KeyCode.Z)
                {
                    notes[6].Play();
                }
                if (kcode == KeyCode.X)
                {
                    notes[7].Play();
                }
                if (kcode == KeyCode.C)
                {
                    notes[8].Play();
                }
            }
        }

    }
}
