using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScrollingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject barPrefab;
    [SerializeField]
    private float timePerWord = 0.2f;
    [SerializeField]
    private int wordsPerJump = 4;
    [SerializeField]
    private float waitAfterStutter = 1f;
    [SerializeField]
    private float chanceForLineBack = 0.3f;
    [SerializeField]
    private Text myText;
    [SerializeField]
    private Text textSizeCalculator;
    [SerializeField]
    private CurtainManager curtainManager;


    private RectTransform[] bars;
    private string[] lines;
    private int currentWordInLine;
    private int currentLine;
    void Start()
    {
        currentWordInLine = wordsPerJump - 1;
        currentLine = 0;
        Canvas.ForceUpdateCanvases();
        lines = new string[myText.cachedTextGenerator.lines.Count];
        for (int i = 0; i < myText.cachedTextGenerator.lines.Count; i++)
        {
            int startIndex = myText.cachedTextGenerator.lines[i].startCharIdx;
            int endIndex = (i == myText.cachedTextGenerator.lines.Count - 1) ? myText.text.Length
                : myText.cachedTextGenerator.lines[i + 1].startCharIdx;
            int length = endIndex - startIndex;
            lines[i] = (myText.text.Substring(startIndex, length));
            if(lines[i].EndsWith(" "))
            {
                lines[i] = lines[i].Remove(lines[i].Length - 1, 1);
            }
        }

        bars = new RectTransform[lines.Length * 2];
        var currentPosition = new Vector2(0, 0);
        currentPosition.y -= myText.lineSpacing;
        textSizeCalculator.text = "Geweldig"; // test text
        var height = textSizeCalculator.preferredHeight;
        textSizeCalculator.text = "Geweldig \n test"; // test text
        var lineSpacing = textSizeCalculator.preferredHeight - 2* height;

        for (var i = 0; i < lines.Length * 2; i++)
        {
            var bar = Instantiate(barPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            bar.transform.SetParent(myText.transform, false);
            var rectTransform = bar.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = currentPosition;
            rectTransform.sizeDelta = new Vector2(myText.rectTransform.sizeDelta.x, height);
            bars[i] = rectTransform;
            if (i % 2 == 1)
            {
                currentPosition.y -= height +  lineSpacing;
            }
        }
        StartCoroutine(NextWord());

    }

    private void SetInitialBarLengths()
    {
        var startingX = 0;
        for (var i = 0; i < bars.Length; i++)
        {
            if (i % 2 == 0)
            {
                float fullBarWidth = CalculateLengthOfMessage(lines[i / 2]);
                bars[i].sizeDelta = new Vector2(fullBarWidth, bars[i].sizeDelta.y);
                bars[i].anchoredPosition = new Vector2(startingX, bars[i].anchoredPosition.y);
            }
            else
            {
                bars[i].sizeDelta = new Vector2(0, bars[i].sizeDelta.y);
            }
        }
        var wordsInLine = lines[currentLine].Split(" ".ToCharArray());

        var text = "";
        for (var i = 0; i < currentWordInLine; i++)
        {
            text += wordsInLine[i] + " ";
        }
        float textWidth = CalculateLengthOfMessage(text);
        bars[currentLine * 2].sizeDelta = new Vector2(bars[currentLine * 2].sizeDelta.x - textWidth, bars[currentLine * 2].sizeDelta.y);
        bars[currentLine * 2].anchoredPosition = new Vector2(startingX + textWidth, bars[currentLine * 2].anchoredPosition.y);
    }

    private void moveLeftBar(int untillWord, int lineId)
    {
        var wordsInLine = lines[lineId].Split(" ".ToCharArray());
        var text = "";
        if (untillWord <= 0)
        {
            untillWord = wordsInLine.Length + untillWord;
        }
        for (var i = 0; i < untillWord; i++)
        {
            text += wordsInLine[i] + " ";
        }
        float width = CalculateLengthOfMessage(text);
        bars[lineId * 2 + 1].sizeDelta = new Vector2(width, bars[lineId * 2 + 1].sizeDelta.y);
    }

    private void moveRightBar()
    {
        var wordsInLine = lines[currentLine].Split(" ".ToCharArray());
        var text = wordsInLine[currentWordInLine] + " ";

        float width = CalculateLengthOfMessage(text);
        bars[currentLine * 2].sizeDelta = new Vector2(bars[currentLine * 2].sizeDelta.x - width, bars[currentLine * 2].sizeDelta.y);
        bars[currentLine * 2].anchoredPosition = new Vector2(bars[currentLine * 2].anchoredPosition.x + width, bars[currentLine * 2].anchoredPosition.y);
    }

    IEnumerator NextWord()
    {
        var goBackToLine = 0;
        SetInitialBarLengths();
        while (currentLine < lines.Length)
        {
            var firstWord = currentWordInLine - wordsPerJump + 1;
            if (firstWord > 0)
                moveLeftBar(firstWord, currentLine);
            else if (currentLine > 0)
                moveLeftBar(firstWord, currentLine - 1);

            moveRightBar();
            yield return new WaitForSeconds(timePerWord);
            var wordsInLine = lines[currentLine].Split(" ".ToCharArray());
            if (currentWordInLine < wordsInLine.Length - 1)
            {
                currentWordInLine++;
                if (goBackToLine > 0 && currentWordInLine > 3)
                {
                    yield return new WaitForSeconds(waitAfterStutter);
                    currentLine = goBackToLine;
                    goBackToLine = 0;
                    currentWordInLine = 0;
                    SetInitialBarLengths();
                }
            }
            else
            {
                currentLine++;
                currentWordInLine = 0;
                if (currentLine > 1 && currentLine < lines.Length)
                {
                    if (UnityEngine.Random.value < chanceForLineBack)
                    {
                        goBackToLine = currentLine;
                        currentLine -= 2;
                        if (lines[currentLine].Length >= 4)
                        {
                            SetInitialBarLengths();
                            chanceForLineBack -= 0.05f;
                        }
                        else
                        {
                            currentLine += 2;
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(timePerWord);
        curtainManager.gameObject.SetActive(true);
        curtainManager.Close();
    }


    //Function to get the display size of the text, because Unity does not want to fix their own function for caluclation it..
    float CalculateLengthOfMessage(string message)
    {
        textSizeCalculator.text = message;
        return textSizeCalculator.preferredWidth;
    }
}
