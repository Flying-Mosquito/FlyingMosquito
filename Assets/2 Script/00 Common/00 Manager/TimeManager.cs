using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeManager : Singleton<TimeManager>
{
    public List<TimeAffectedObj> objList;
    public bool isMenuOpen = false;
    public float fPreTimeScale = 1f;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void AddObj(TimeAffectedObj _obj)
    {
        objList.Add(_obj);
    }

    public void DeleteAllObj()
    {
        objList.Clear();
        isMenuOpen = false;
    }

    public IEnumerator SetOption(bool _isMenuOpen)//void SetOption(bool _isMenuOpen)
    {
        yield return new WaitForEndOfFrame();

        isMenuOpen = _isMenuOpen;

        if (isMenuOpen)
        {
            fPreTimeScale = Time.timeScale;
            print("셋옵션- 타임스케일영 : " + isMenuOpen);
            Time.timeScale = 0f;

        }
        else
        {
            Time.timeScale = fPreTimeScale;  // = 1f;
        }
            
    }



    void FixedUpdate()
    {
        if (!isMenuOpen)
        {
            for (int i = 0; i < objList.Count; ++i)
                objList[i].MyFixedUpdate();
        }
        else
        {
            print("타임스케일영");
            Time.timeScale = 0f;
        }

    }

	void Update ()
    {
        if (!isMenuOpen)
        {
            for (int i = 0; i < objList.Count; ++i)
                objList[i].MyUpdate();
        }
	}

    void LateUpdate()
    {
        if (!isMenuOpen)
        {
            for (int i = 0; i < objList.Count; ++i)
                objList[i].MyLateUpdate();
        }
    }
}
