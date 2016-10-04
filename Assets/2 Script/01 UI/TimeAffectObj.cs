using UnityEngine;
using System.Collections;

// 활성화 시 시간을 멈추고, 비활성화 시 시간을 움직이게 함 
public class TimeAffectObj : MonoBehaviour {

    void OnEnable()
    {
        //print("OnEnable");
        TimeManager.Instance.StartCoroutine("SetTimeStop",true);
    }

    void OnDisable()
    {
        //print("OnDisable");
        TimeManager.Instance.StartCoroutine("SetTimeStop",false);
    }
	
}
