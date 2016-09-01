using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuSoundCtrl : MonoBehaviour {
    public Slider slider;

    void Awake()
    {
        // slider가 하나 더 들어오면 수정할거야 
        slider = GetComponentInChildren<Slider>();
    }
    public void SetSound()
    {
        SoundManager.Instance.backgroundSound = slider.value;
        SoundManager.Instance.SetSound();
    }
}
