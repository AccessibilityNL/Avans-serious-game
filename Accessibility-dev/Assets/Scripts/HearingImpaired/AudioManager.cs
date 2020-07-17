using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource Voice;
    [SerializeField]
    private AudioSource Sound;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button doneButton;
    [SerializeField]
    private Text text;
    [SerializeField]
    private CurtainManager curtainManager;

    [SerializeField]
    private float VolumeVoice;

    private bool secondTime;
    void Start()
    {
        secondTime = false;
        Voice.volume = VolumeVoice;

        StartCoroutine(StartSound());

        doneButton.interactable = false;
        doneButton.onClick.AddListener(Close);

        playButton.interactable = false;
        playButton.onClick.AddListener(OneMoreTime);
    }

    private void OneMoreTime()
    {
        secondTime = true;
        playButton.gameObject.SetActive(false);
        text.text = "";

        StartCoroutine(StartSound());
    }
    IEnumerator StartSound()
    {
        float startVolume = 0.01f;

        Sound.volume = startVolume;
        Sound.Play();
        while (Sound.volume < 0.3)
        {
            Sound.volume += startVolume * Time.deltaTime / 0.05f;

            yield return null;
        }

        Voice.Play();
        if (secondTime == true)
        {
            StartCoroutine(CloseAfterAudioFinished());
        }
        StartCoroutine(StopSound());
    }

    IEnumerator StopSound()
    {
        yield return new WaitForSeconds(Voice.clip.length);
        Sound.Stop();
        playButton.interactable = true;
        doneButton.interactable = true;
    }

    IEnumerator CloseAfterAudioFinished()
    {
        yield return new WaitForSeconds(Voice.clip.length);
        Close();
    }

    void Close()
    {
        doneButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        text.text = "";
        curtainManager.gameObject.SetActive(true);
        curtainManager.Close();
    }
}
