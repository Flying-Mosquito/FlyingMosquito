using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeManager : Singleton<TimeManager>
{
    public List<TimeAffectedObj> objList;
    public bool isMenuOpen = false;

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
        isMenuOpen = _isMenuOpen;
    }

    void FixedUpdate()
    {
        if (!isMenuOpen)
        {
            for (int i = 0; i < objList.Count; ++i)
                objList[i].MyFixedUpdate();
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
