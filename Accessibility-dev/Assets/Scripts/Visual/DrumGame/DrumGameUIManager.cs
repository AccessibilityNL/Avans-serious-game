using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrumGameUIManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI text;

    public void EngageWinOrLossUI(string state)
    {
        StartCoroutine(ShowWinOrLossUI(state));
    }

    private IEnumerator ShowWinOrLossUI(string state)
    {
        //SetActive
        text.gameObject.SetActive(true);
        text.SetText(state);

        //SetInactive
        yield return new WaitForSeconds(1f);
        text.gameObject.SetActive(false);
        
    }
}
