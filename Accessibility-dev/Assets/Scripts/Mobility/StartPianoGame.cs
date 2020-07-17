using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPianoGame : MonoBehaviour
{
    public GameObject glow;
    public GameObject piano;
    public GameObject noteWall;
    public GameObject noteHolder;
    private bool isFrozen = false;

    void CheckInteraction()
    {
        float distance = 4f;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        glow.SetActive(false);
        if (Physics.Raycast(ray, out hit, distance))
        {
            if(hit.transform.tag == "Sign")
            {
                glow.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ActivatePianoGame();
                    print("HIT");
                }
            }
        }

        if (isFrozen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnFreezePlayer();
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        print("Started");
    }

    // Update is called once per frame
    void Update()
    {
        CheckInteraction();
    }

    void ActivatePianoGame()
    {
        Vector3 endPosition = new Vector3(1.41f, 0.02f, 15.09f);
        Vector3 startPosition = new Vector3(1.41f, -4.22f, 15.09f);
        //piano.transform.position = Vector3.Lerp(startPosition,endPosition,500.0f*Time.deltaTime);
        piano.transform.position += new Vector3(0.0f, 4.3f, 0.00f);
        noteHolder.SetActive(true);
        noteWall.SetActive(true);
        FreezePlayer();
    }

    void FreezePlayer()
    {
        GameObject varGameObject = GameObject.FindWithTag("Player");
        Behaviour obj = (Behaviour)varGameObject.GetComponent("PlayerMovement");
        obj.enabled = false;
        isFrozen = true;
    }

    void UnFreezePlayer()
    {
        GameObject varGameObject = GameObject.FindWithTag("Player");
        Behaviour obj = (Behaviour)varGameObject.GetComponent("PlayerMovement");
        obj.enabled = true;
        isFrozen = false;
    }
}
