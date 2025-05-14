using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollWithAudio : MonoBehaviour
{
    public static ScrollWithAudio instance;
    public bool paused;

    public ScrollRect scrollRect;
    private AudioSource audioSource;

    private float scrollDuration;
    bool nextStepEnable;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(bool play)
    {
        if (audioSource != null)
        {
            if (audioSource.clip != null)
            {
                if (play)
                {
                    audioSource.Play();
                    paused = false;
                }
                else
                {
                    audioSource.Pause();
                    paused = true;
                }
            }

        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            float normalizedTime = audioSource.time / scrollDuration;
            scrollRect.verticalNormalizedPosition = 1f - normalizedTime;
        }
        else
        {
            if (paused) return;
            if (nextStepEnable)
            {
                nextStepEnable = false;
                StepsController.instance.NextStep();
                audioSource.clip = null;
            }
        }
    }

    public void PlayAudioAndScroll(AudioClip clip)
    {
        paused = false;
        audioSource.clip = clip;
        scrollDuration = audioSource.clip.length;
        audioSource.Play();
        nextStepEnable = true;
    }
}
