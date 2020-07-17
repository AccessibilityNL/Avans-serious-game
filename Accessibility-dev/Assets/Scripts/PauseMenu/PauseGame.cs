using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public static bool isGamePaused = false;
    private float timeScale = 1;
    private SceneLoader sceneLoader;
    private GameObject currentPauseScreen = null;
    private CursorLockMode lockmodeBeforePause;
    private bool wasCursorVisable = true;
    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        wasCursorVisable = Cursor.visible;
        Cursor.visible = true;
        lockmodeBeforePause = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;
        var foundCanvas = FindObjectsOfType<Canvas>();
        Canvas canvas;
        if(foundCanvas.Length > 1)
        {
            canvas = foundCanvas[1];
        }
        else
        {
            canvas = foundCanvas[0];
        }
        currentPauseScreen = Instantiate(pauseMenu, new Vector3(0, 0, 0), Quaternion.identity);
        currentPauseScreen.transform.SetParent(canvas.transform, false);
        //pauseMenu.SetActive(true);
        timeScale = Time.timeScale;
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void Resume()
    {
        Cursor.lockState = lockmodeBeforePause;
        Cursor.visible = wasCursorVisable;
        Time.timeScale = timeScale;
        isGamePaused = false;
        DestroyPauseMenu();
    }
    public void BackToOverworld()
    {
        DestroyPauseMenu();
        sceneLoader.BackToOverworld("pause");
        Time.timeScale = timeScale;
        isGamePaused = false;
    }

    private void DestroyPauseMenu()
    {
        if (currentPauseScreen)
        {
            Destroy(currentPauseScreen);
            currentPauseScreen = null;
        }
    }
}
