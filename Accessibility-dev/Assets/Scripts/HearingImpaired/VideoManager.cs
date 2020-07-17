using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Video;
using System.ComponentModel;
using UnityEditor;
using UnityEngine.Audio;

public class VideoManager : MonoBehaviour
{
    [SerializeField]
    private string videoClip;
    [SerializeField]
    private VideoPlayer video;
    [SerializeField]
    private CurtainManager curtainManager;

    private bool Ended;

    void Start()
    {
        Ended = false;
        var videoPlayer = video;

        //GameObject camera = GameObject.Find("Main Camera");


        //var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.aspectRatio = VideoAspectRatio.FitHorizontally;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
        videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
        //videoPlayer.targetMaterialRenderer = renderer;

        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoClip + ".mp4");

        videoPlayer.Play();

        if (!videoPlayer.isPlaying && Ended == false)
        {
            Ended = true;
            StartCoroutine(CloseAfterAudioFinished());
        }
    }

    IEnumerator CloseAfterAudioFinished()
    {
        yield return new WaitForSeconds(4);
        Close();
    }

    void Close()
    {
        curtainManager.gameObject.SetActive(true);
        curtainManager.Close();
    }
}
