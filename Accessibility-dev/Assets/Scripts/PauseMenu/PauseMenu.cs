using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private PauseGame pauseGame;
    void Start()
    {
        pauseGame = FindObjectOfType<PauseGame>();
    }

    public void Resume()
    {
        pauseGame.Resume();
    }
    public void Quit()
    {
        pauseGame.BackToOverworld();
    }

}
