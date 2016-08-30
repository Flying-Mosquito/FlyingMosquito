using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : Singleton<SoundManager>
{
    public List<AudioSource> backgroundAudioList = new List<AudioSource>();
    public List<AudioSource> effectAudioList     = new List<AudioSource>();
    private int MAXSOUND = 5;
    private int backgroundSound = 5;
    private int effectSound = 5;

    public void AddBackgroundSound(AudioSource _source)
    {
        backgroundAudioList.Add(_source);
    }
    public void AddEffectSound(AudioSource _source)
    {
        effectAudioList.Add(_source);
    }

    public void SetBackgroundSound(int _sound)
    {
        float sound = _sound / MAXSOUND;
        if (sound > 1f)
            sound = 1f;

        for(int i = 0; i <backgroundAudioList.Count; ++i)
        {
            backgroundAudioList[i].volume = sound;
        }
    }

    public void SetEffectSound(int _sound)
    {
        float sound = _sound / MAXSOUND;
        if (sound > 1f)
            sound = 1f;

        for (int i = 0; i < effectAudioList.Count; ++i)
        {
            effectAudioList[i].volume = sound;
        }
    }
}
