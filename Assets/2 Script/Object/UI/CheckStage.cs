﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class CheckStage : MonoBehaviour {
    public Transform Stage1;
    public Transform Stage2;
    
    private int iStage = 9;
  
    SelectStage selectstage;
    public int stageNum;

    public Transform[] Stage = new Transform[9];
    private int MAXINDEX = 3;
   public int[] Arr = new int[3] {0,1,2};


    // Use this for initialization
    void Awake () {
        selectstage = gameObject.GetComponent("SelectStage") as SelectStage;
        Arr = new int[iStage];
        print("dddd");
        for(int i=0;i<9;i++)
        {
            Arr[i] =0;//0닫힘 1주황 2 초록
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 9; i++)
        {
            if (PlayerCtrl.Instance.iBlood > 190&& PlayerCtrl.Instance.stage[i]==1)
            {
                Arr[0] = 1;
            }
        }
        //1성공 2닫힘
        for (int i = 0; i < 9; i++)
        {
            if (Arr[i] == 1 && Arr[i+1] == 0)
            {
               Stage[i+1].Find(""+(i+2).ToString()).gameObject.SetActive(true);
            }
        }
            if(Arr[1] == 2)
        {
            Stage2.Find("2-1").gameObject.SetActive(true);
        }
     
     
	}
}
