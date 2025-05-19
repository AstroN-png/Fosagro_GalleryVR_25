using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer [] videoPlayer;

    public void PlayVideo(int index)
    {
        for (int i = 0; i < videoPlayer.Length; i++)
        {
            videoPlayer[i].SetDirectAudioMute(0, true);
        }

        videoPlayer[index].Stop();
        // ¬ключаем звук только у нужного
        videoPlayer[index].SetDirectAudioMute(0, false);
        videoPlayer[index].Play();
    }
}
