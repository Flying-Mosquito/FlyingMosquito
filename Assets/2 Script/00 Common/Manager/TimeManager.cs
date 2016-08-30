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

    public void SetOption(bool _isMenuOpen)
    {
        print("셋옵션");
        isMenuOpen = _isMenuOpen;

        // 원래 이렇게 하면 안되지만, 일단 내가 원하는건 애니메이션이 멈추는거고, 따로 조절할 수도 있겠지만, 물리같은 건 유니티 내부에서 처리하니까
        // 내가 어떻게 해야 할지 모르겠군...
        if (_isMenuOpen)
        {
            fPreTimeScale = Time.timeScale;
            print("타임스케일영");
            Time.timeScale = 0f;

        }
        else
        {
            Time.timeScale = fPreTimeScale;  // = 1f;
            print("타임스케일: " + fPreTimeScale);
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
            Time.timeScale = 0f;

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
