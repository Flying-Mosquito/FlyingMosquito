using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeManager : Singleton<TimeManager>
{
    public List<TimeAffectedObj> objList;
   // public bool isMenuOpen = false;
    public float fPreTimeScale = 1f;
    public int iOpenPannelNum;

    void Awake()
    {
        DontDestroyOnLoad(this);
        iOpenPannelNum = 0;
    }

    public void AddObj(TimeAffectedObj _obj)
    {
        objList.Add(_obj);
    }

    public void DeleteAllObj()
    {
        objList.Clear();
        //isMenuOpen = false;
        print("DeleteAllObj");
        //iOpenPannelNum = 0;
        //Time.timeScale = fPreTimeScale;
        Time.timeScale = 1f; // 정상시간으로 되돌려줌
    }

    public IEnumerator SetTimeStop(bool _isMenuOpen)//void SetOption(bool _isMenuOpen)
    {
        yield return new WaitForEndOfFrame();

        //isMenuOpen = _isMenuOpen;

        if (_isMenuOpen) //시간이 멈춰야 함
        {
            if (iOpenPannelNum == 0)
            {
                fPreTimeScale = Time.timeScale;
                Time.timeScale = 0f;
            }
           // else
             //   Time.timeScale = 0f;
            ++(iOpenPannelNum);

            //  ++iOpenPannelNum;
        }
        else   //예전시간 그대로 가야함
        {
            --iOpenPannelNum;
            if (iOpenPannelNum <= 0)
            {
                if (iOpenPannelNum < 0) // 이렇게 하면 안되는데...
                {
                    print("0보다 작으시단다");
                    iOpenPannelNum = 0;
                }
               // print("타임스케일 원래대로, preTimescale : " + fPreTimeScale);
                Time.timeScale = fPreTimeScale;  // = 1f;
            }
        }
            
       // print("")
    }



    void FixedUpdate()
    {
        if (iOpenPannelNum == 0)
        {
            for (int i = 0; i < objList.Count; ++i)
                objList[i].MyFixedUpdate();
        }
        else
        {
            //print("타임스케일영, num : " + iOpenPannelNum);
         //   fPreTimeScale = Time.timeScale;
          //  Time.timeScale = 0f;
        }

    }

	void Update ()
    {
        //print("TimeScale: " + Time.timeScale);
        if (iOpenPannelNum == 0)
        {
            for (int i = 0; i < objList.Count; ++i)
                objList[i].MyUpdate();
        }
	}

    void LateUpdate()
    {
        if (iOpenPannelNum == 0)
        {
            for (int i = 0; i < objList.Count; ++i)
                objList[i].MyLateUpdate();
        }
    }
}
