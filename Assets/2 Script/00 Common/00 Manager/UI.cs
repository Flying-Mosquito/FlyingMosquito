using UnityEngine;
using System.Collections;

 public class UI : Singleton<UI>
{

    public SleepHuman Sleep;
    public int[] stage = new int[2] { 0, 1 };
    public int[] CheckTimer = new int[2] { 0, 1 };
    public float[] score = new float[12];
    PlayerCtrl playerctrl = null;
    public float Tscore;
    public float Gold;
    public int plusSt;
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
       
        stage = new int[12];
        CheckTimer = new int[12];
        score = new float[12];
        for (int i = 0; i < 11; i++)
        {
            stage[i] = 0;
        }
        //playerctrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        //  playerctrl = GameObject.FindObjectOfType<PlayerCtrl>();//GameObject.Find("Player").GetComponent<PlayerCtrl>();
    }
    public void CheckS()
    {
        for (int i = 1; i < 12; i++)
        {
            CheckTimer[i] = 0;

            if (Application.loadedLevelName == "Stage" + i.ToString())
            {

                if (Application.loadedLevelName == "Stage6")
                {

                    Sleep = GameObject.FindObjectOfType<SleepHuman>();
                    if (Sleep.Sleep1 == true && Sleep.Sleep2 == true)
                    {
                        stage[6] = 1;
                        CheckTimer[6] = 1;
                    }
                }
             

                else {
                    stage[i] = 1;
                    CheckTimer[i] = 1;
                }

            }
        }

    }
    // Update is called once per frame
    void Update()
    {
    
     
    }

    public void SetPlayer()
    {
        playerctrl = GameObject.FindObjectOfType<PlayerCtrl>();
    }
}