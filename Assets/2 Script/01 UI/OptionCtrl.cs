using UnityEngine;
using System.Collections;

public class OptionCtrl : MonoBehaviour {

    void OnEnable()
    {
        
        TimeManager.Instance.StartCoroutine("SetTimeStop",true);
    }

    void OnDisable()
    {
        TimeManager.Instance.SetTimeStop(false);
    }
	
}
