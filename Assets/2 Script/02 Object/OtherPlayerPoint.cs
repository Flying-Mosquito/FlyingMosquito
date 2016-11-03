﻿using UnityEngine;
using System.Collections;

public class OtherPlayerPoint : MonoBehaviour {

    public enum ePointState { eMenu, eStage }
    public ePointState state;
    public GameObject PlayerPrefeb;
	public int iPlayerN;
    private PlayerCtrl player = null;
	public GameObject clntObj;

    public bool isCamStateCollider;

    void Awake()
    {
        //   CameraCtrl.Instance.GetComponent<CameraEffect>().SetParentCamp();
        //   PlayerCtrl.Instance.SetStateIdle(true);

        CreatePlayer();

        //PlayerCtrl.Instance.SetTransform(transform.position, transform.rotation);
    }
    public void SetPlayerNum(int _iPlayerNum)
    {
        // print("_PlayerPoint -  :" + _iPlayerNum);
        // iPlayerNum = _iPlayerNum;
        player.SetPlayerNum(_iPlayerNum);
		GameObject.Find ("ConnectManager").GetComponent<ConnectMultiServ> ().SetOtherPlayer (clntObj, _iPlayerNum);
    }

    void CreatePlayer()
    {
        // 플레이어 만들면서 카메라도 셋팅 
        GameObject _player = Instantiate(PlayerPrefeb, transform.position, transform.rotation) as GameObject;
        player = _player.GetComponent<PlayerCtrl>();
		clntObj = _player;

		// 멀티서버 스크립트 내부변수에 연결 (여기가 안된듯?)
		//GameObject.Find ("ConnectManager").GetComponent<ConnectMultiServ> ().SetOtherPlayer (_player, iPlayerN);
     //   CameraCtrl.Instance.SetPlayer(_player);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (state == ePointState.eStage)
            {
                GameManager.Instance.SetBackgroundAlphaColor(0f);
                gameObject.SetActive(false);


            }
        

        }



    }
}
