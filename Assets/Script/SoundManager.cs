using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    // audio source
    public AudioSource audioSource;

    // audio clip
    [SerializeField]
    private AudioClip jumpAudioClip, huryAudioClip, goodsCollect;

    private void Awake()
    {
        instance = this;
    }

    public void JumpAudio()
    {
        Play(jumpAudioClip);
    }

    public void HurtAudio()
    {
        Play(huryAudioClip);
    }

    public void CollectGoods()
    {
        Play(goodsCollect);
    }

    private void Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
