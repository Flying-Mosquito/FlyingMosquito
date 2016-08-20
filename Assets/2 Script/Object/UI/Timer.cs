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
   
    // Use this for initialization
    PlayerCtrl playerctrl=null;

    void onEnable()
    {
        startTimer();
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
        //playerctrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
       
    }
    public void SetPlayer()
    {
        playerctrl = GameObject.FindObjectOfType<PlayerCtrl>(); //GameObject.Find("Player").GetComponent<PlayerCtrl>();
        
    }
    void Start()
    {
      
        startTime = Time.time;

        startTimer();

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName == "Stage1")
        {
            SetPlayer();
        }
        if (!(playerctrl== null))
        {
            UI.Instance.CheckS();


            if (playerctrl.iHP < 5 || totaltime < 1)
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
                if (playerctrl.iBlood > 190 && UI.Instance.CheckTimer[i] == 1)
                {
                    gameClear.gameObject.SetActive(true);

                    StopTimer();


                    score[i] = (Timer.Instance.totaltime * 2);
                    ScoreText.text = score[i].ToString();


                }

            }


            if (isEnable)
            {
                totaltime -= Time.deltaTime;
            }



            string minutes = ((int)totaltime / 60).ToString();
            string seconds = (totaltime % 60).ToString("f2");

            timerText.text = minutes + ":" + seconds;

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
