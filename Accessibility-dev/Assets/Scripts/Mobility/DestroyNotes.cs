using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNotes : MonoBehaviour
{
    [SerializeField]
    private string nextScene = "overworld";
    public GameObject finalText;
    GameObject noteLine;
    NoteLineCollision scoreFetch;
    [SerializeField] private CurtainManager curtainManager;
    SceneLoader sceneLoader;
    private string category = "mobility";

    void Start()
    {
        noteLine = GameObject.FindGameObjectWithTag("NoteLine");
        scoreFetch = FindObjectOfType<NoteLineCollision>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name != "NoteWall")
        {
            if (other.gameObject.name == "NoteJ (1)")
            {
                StartCoroutine("NextScene", 3);
                finalText.SetActive(true);
                noteLine.GetComponent<MonoBehaviour>().CancelInvoke();
                GameObject.Find("RightArrow").SetActive(false);
                GameObject.Find("LeftArrow").SetActive(false);
                
            }
            Destroy(other.gameObject);
        }
        
    }

    IEnumerator CloseGame(float time)
    {
        yield return new WaitForSeconds(time);
        curtainManager.Close();
        yield return null;
    }

    public IEnumerator NextScene()
    {
        //17 notes 20 total points = 1.17 per point
        double newScore = scoreFetch.score * 1.17;
        curtainManager.Close();
        yield return new WaitForSeconds(3);
        if (sceneLoader)
        {
            print("Sceneloader found");
            if (nextScene.Equals("overworld"))
                sceneLoader.BackToOverworld("mobility", (int)newScore);
            else
                sceneLoader.ChangeToScene(nextScene, category, (int)newScore);
        }
    }
}
