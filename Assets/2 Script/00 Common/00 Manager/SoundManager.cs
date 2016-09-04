using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//게임내에서사운드바꿀거 생각해서
public class SoundManager : Singleton<SoundManager>
{
    public  List<AudioSource> backgroundAudioList = new List<AudioSource>();
    public  List<AudioSource> effectAudioList     = new List<AudioSource>();
    //private int MAXSOUND = 1f;
    public float backgroundSound = 1f;
    public float effectSound = 1f;

    void Awake()
    {
        DontDestroyOnLoad(this);

    }

    public void AddBackgroundSound(AudioSource _source)
    {

        backgroundAudioList.Add(_source);
    }
    public void AddEffectSound(AudioSource _source)
    {
        effectAudioList.Add(_source);
    }

    public void ClearSoundList()
    {
        backgroundAudioList.Clear();
        effectAudioList.Clear();
    }

    public void SetSound()
    {
      //  float sound = _sound / MAXSOUND;
       // if (sound > 1f)
         //   sound = 1f;

        for(int i = 0; i <backgroundAudioList.Count; ++i)
        {
            backgroundAudioList[i].volume = backgroundSound;
        }

        for (int i = 0; i < effectAudioList.Count; ++i)
        {
            effectAudioList[i].volume = effectSound;
        }
    }
    /*
    public void SetEffectSound()
    {
       // float sound = _sound / MAXSOUND;
        //if (sound > 1f)
         //   sound = 1f;

        for (int i = 0; i < effectAudioList.Count; ++i)
        {
            effectAudioList[i].volume = effectSound;
        }
    }
    */
}
