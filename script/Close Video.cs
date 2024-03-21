using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class CloseVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private bool videoFinished = false;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void Update()
    {
        if (videoFinished ) 
        {
            SwitchToNextScene();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        videoFinished = true;
    }

    void SwitchToNextScene()
    {
        SceneManager.LoadScene("Menu"); 
    }

}
