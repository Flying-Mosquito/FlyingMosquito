using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeBtCtrl : MonoBehaviour {

    private Slider slider;
	// Use this for initialization
	void Start ()
    {
        slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        SoundManager.Instance.backgroundSound = slider.value;
	}
}
