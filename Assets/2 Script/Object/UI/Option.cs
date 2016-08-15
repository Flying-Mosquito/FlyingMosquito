﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour {
    public Button exit;
    public Button back;
    // Use this for initialization
    Timer timer;

    PlayerCtrl playerctrl;
    public bool backBool;
	void Start () {
        playerctrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Click()
    {
        playerctrl.iBlood = 0;
        playerctrl.SetParentNull();
        playerctrl.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        timer.gameover.gameObject.SetActive(false);
       timer.gameClear.gameObject.SetActive(false);

        SceneManager.LoadScene(13);
        
        //PlayerCtrl.Instance.state = Constants.ST_IDLE;
        //PlayerCtrl.Instance.variable &= ~(Constants.BV_Stick);
        // //   PlayerCtrl.Instance.SetStateIdle(true);
        // PlayerCtrl.Instance.SetIsInStage(false);

        // Timer.Instance.totaltime = 60;
        // Timer.Instance.isEnable = true;
    }
    public void intoMulti()
    {
        SceneManager.LoadScene(14);
    }
    public void backLobby()
    {
        SceneManager.LoadScene(14);
    }
    public void IntoRoom()
    {
        SceneManager.LoadScene(15);
    }
}
