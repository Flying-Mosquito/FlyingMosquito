using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeBtCtrl : BaseButton {

    private Slider slider;
	// Use this for initialization
	void Start ()
    {
        slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	/*void Update ()
    {
        SoundManager.Instance.backgroundSound = slider.value;
	}*/
    public override void OnTouchBegin(Vector2 _pos)
    {
        SoundManager.Instance.backgroundSound = slider.value;
    }

    public override void OnTouchMove(Vector2 _pos)
    {
        SoundManager.Instance.backgroundSound = slider.value;
    }

    public override void OnTouchEnd(Vector2 _pos)
    {
        SoundManager.Instance.backgroundSound = slider.value;
    }

}
