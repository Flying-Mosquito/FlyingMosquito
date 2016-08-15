using UnityEngine;
using System.Collections;

public class UI : Singleton<UI>
{

    public Transform timer;
    PlayerCtrl playerctrl = null;
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
        timer.gameObject.SetActive(false);
      //  playerctrl = GameObject.FindObjectOfType<PlayerCtrl>();//GameObject.Find("Player").GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerctrl != null)
        {
            if ((playerctrl.variable & Constants.BV_IsInStage) > 0)
            {
                timer.gameObject.SetActive(true);
                FlyBtCtrl flybt = GameObject.FindObjectOfType<FlyBtCtrl>();
                ClingBtnCtrl clingbt = GameObject.FindObjectOfType<ClingBtnCtrl>();


                clingbt.SetPlayer(playerctrl);
                flybt.SetPlayer(playerctrl);
            }
            else// if (PlayerCtrl.Instance.state == Constants.ST_IDLE)
            {
                timer.gameObject.SetActive(false);
                FlyBtCtrl flybt = GameObject.FindObjectOfType<FlyBtCtrl>();
                ClingBtnCtrl clingbt = GameObject.FindObjectOfType<ClingBtnCtrl>();


                clingbt.SetPlayer(playerctrl);
                flybt.SetPlayer(playerctrl);
            }
        }  
    }

    public void SetPlayer()
    {
        playerctrl = GameObject.FindObjectOfType<PlayerCtrl>();
    }
}
