using UnityEngine;
using System.Collections;

public class UI : Singleton<UI>
{

    public Transform timer;
    PlayerCtrl playerctrl;
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
        timer.gameObject.SetActive(false);
        playerctrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((playerctrl.variable & Constants.BV_IsInStage) > 0)
            timer.gameObject.SetActive(true);
        else// if (PlayerCtrl.Instance.state == Constants.ST_IDLE)
            timer.gameObject.SetActive(false);
    }
}
