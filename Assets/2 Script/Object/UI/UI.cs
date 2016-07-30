using UnityEngine;
using System.Collections;

public class UI : Singleton<UI>
{

    public Transform timer;
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
        timer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((PlayerCtrl.Instance.variable & Constants.BV_IsInStage) > 0)
            timer.gameObject.SetActive(true);
        else// if (PlayerCtrl.Instance.state == Constants.ST_IDLE)
            timer.gameObject.SetActive(false);
    }
}
