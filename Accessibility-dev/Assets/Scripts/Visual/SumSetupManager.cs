using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SumSetupManager : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> imagesFirstNumber;
    [SerializeField]
    private List<Sprite> imagesSecondNumber;
    [SerializeField]
    private Image fistImage;
    [SerializeField]
    private List<string> oparations;
    [SerializeField]
    private Text oparation;
    [SerializeField]
    private Image secondImage;
    void Start()
    {
        int randomFirstImage = Random.Range(0, imagesFirstNumber.Count);
        int randomSecondImage = Random.Range(0, imagesSecondNumber.Count);
        int randomOparation = Random.Range(0, 2);

        fistImage.sprite = imagesFirstNumber[randomFirstImage];
        secondImage.sprite = imagesSecondNumber[randomSecondImage];
        oparation.text = oparations[randomOparation];

    }


}
