using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MapSoundCtrl : MonoBehaviour {
    public Slider slider;
    public Toggle GO;
    public static bool Check;
    void Awake()
    {
        // slider가 하나 더 들어오면 수정할거야 
        slider = GetComponentInChildren<Slider>();
        GO = GetComponentInChildren<Toggle>();
        if (Check == GO.isOn)
            return;
        else
            GO.isOn=Check;
    }
    public void SetSound()
    {
      
       
        SoundManager.Instance.backgroundSound = slider.value;
        SoundManager.Instance.SetSound();
    }
    public void mark()
    {
        Check = GO.isOn;
    }
}
