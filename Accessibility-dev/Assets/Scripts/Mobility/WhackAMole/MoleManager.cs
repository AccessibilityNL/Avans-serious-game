using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour
{
    public GameObject circlePrefab;
    public Transform[] spawnPoints;
    public float gameTime;
    public Text gameText;
    public Text scoreText;
    public int score;
    public Text finalScore;
    private bool done = false;
    SceneLoader sceneLoader;
    private string category = "mobility";
    private string nextScene = "PianoScene";

    [SerializeField] private CurtainManager curtainManager;

    void Start()
    {
        score = 0;
        sceneLoader = FindObjectOfType<SceneLoader>();
        SpawnCircles();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameTime == 0 && !done)
        {
            finalScore.text = "Goed gedaan! Jouw score is : " + score.ToString() + ". Lastig als de muis niet meewerkt? Zo kan het voelen als je niet zelf" +
                " de volledige controle hebt over je lichaam";
            scoreText.enabled = false;
            gameText.enabled = false;
            GameObject circle = GameObject.FindGameObjectWithTag("Circle");
            Destroy(circle);
            StartCoroutine("NextScene", 4);
            done = true;//bad fix, but else you are calling the CloseGame function a million times
            Cursor.visible = true;
        }
        gameTime -= Time.deltaTime;
        if(gameTime < 1)
        {
            gameTime = 0;
        }
        gameText.text = gameTime.ToString("F1");

        if(Input.GetButtonDown("Fire1") && gameTime > 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit)
            {
                if (hit.collider.tag == "Circle")
                {
                    Destroy(hit.transform.gameObject);
                    score += 1;
                    scoreText.text = score.ToString();
                    SpawnCircles();
                }
            }

        }
    }

    public void SpawnCircles()
    {
        GameObject circle = Instantiate(circlePrefab) as GameObject;
        circle.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
    }

    IEnumerator CloseGame(float time)
    {
        yield return new WaitForSeconds(time);
        curtainManager.Close();
        Cursor.visible = true;
        yield return null;
    }

    public IEnumerator NextScene()
    {
        if(score > 20)
        {
            score = 20;
        }
        Cursor.visible = true;
        curtainManager.Close();
        yield return new WaitForSeconds(3);
        if (sceneLoader)
        {
            if (nextScene.Equals("overworld"))
                sceneLoader.BackToOverworld("mobility", (int)score);
            else
                sceneLoader.ChangeToScene(nextScene, category, (int)score);
        }
    }
}
