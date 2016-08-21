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
       
    }

    // Update is called once per frame
    void Update()
    {
        if (UI.Instance != null)
        {
            for (int i = 0; i < 5; i++)
            {
                if (UI.Instance.stage[i] == 1)
                {
                    Arr[i] = 1;
                }
            }
            //1성공 2닫힘
            for (int i = 0; i < 5; i++)
            {
                if ((Arr[i] == 0 && Arr[i + 1] == 1) || (Arr[i] == 1 && Arr[i + 1] == 1))
                {
                    Stage[i + 1].Find("" + (i + 2).ToString()).gameObject.SetActive(true);
                }
            }
            if (Arr[1] == 2)
            {
                Stage2.Find("2-1").gameObject.SetActive(true);
            }


        }
    }
}