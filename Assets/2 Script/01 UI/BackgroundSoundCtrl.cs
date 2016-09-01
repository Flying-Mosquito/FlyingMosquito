using UnityEngine;
using System.Collections;

public class BackgroundSoundCtrl : MonoBehaviour {
   public AudioClip backgroundAudio;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = backgroundAudio;
    }

    void Start()
    {


        SoundManager.Instance.AddBackgroundSound(source);
        SoundManager.Instance.SetSound();
        source.Play();
    }

}
