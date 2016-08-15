using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : Singleton<Timer>
{

    public Text timerText;
    public Text ScoreText;
    public float startTime;
    public Transform gameover;
    public Transform gameClear;
    public float totaltime = 60;
   public bool isEnable = false;

    public float[] score = new float[9];
    public int[] stage = new int[2] { 0, 1 };
    public int[] CheckTimer = new int[2] { 0, 1 };

    // Use this for initialization
    PlayerCtrl playerctrl;

    void onEnable()
    {
        startTimer();
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
        playerctrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        stage = new int[9];
        CheckTimer = new int[9];
        for (int i = 0; i < 9; i++)
        {
            stage[i] = 0;
        }
    }
    void Start()
    {
      
        startTime = Time.time;

        startTimer();

    }

    // Update is called once per frame
    void Update()
    {
        CheckS();


        if (playerctrl.iHP <5|| totaltime < 1)
        {
            gameover.gameObject.SetActive(true);

            StopTimer();

        }
       
        else
        {
            gameover.gameObject.SetActive(false);
        }

        for (int i = 1; i < 9; i++)
        {
            if (playerctrl.iBlood > 190 && CheckTimer[i] == 1)
            {
                gameClear.gameObject.SetActive(true);

                StopTimer();


                score[i] = (Timer.Instance.totaltime * 2);
                ScoreText.text = score[i].ToString();


            }

        }
       
      
        if (isEnable)
        {
             totaltime -=Time.deltaTime;
        }



        string minutes = ((int)totaltime / 60).ToString();
        string seconds = (totaltime % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;




    }
    public void CheckS()
    {
        for (int i = 1; i < 9; i++)
        {
            CheckTimer[i] = 0;

            if (Application.loadedLevelName == "Stage" + i.ToString())
            {
                stage[i] = 1;
                CheckTimer[i] = 1;
                if (Application.loadedLevelName == "Stage3")
                    playerctrl.blooding();
                  
                else if (Application.loadedLevelName == "Stage2")
                    playerctrl.blooding();

            }
        }

    }
    public void startTimer()
    {
       
        isEnable = true;
    }
    public void StopTimer()
    {
      
        isEnable = false;
    }
}
