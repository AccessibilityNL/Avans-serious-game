using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Line")
        {
            ScoreManager.IncreaceScore();
            this.gameObject.SetActive(false);
            
        };
    }
}
