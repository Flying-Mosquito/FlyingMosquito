using UnityEngine;
using System.Collections;

public class StageStarInfo 
{
    public int iStageNum;
    public int iStarNum;

    public StageStarInfo(int _iStageNum, int _iStarNum)
    {
        iStageNum = _iStageNum;
        iStarNum = _iStarNum; 
    }

    public void SetStarNum(int _iStarNum)
    {
        iStarNum = _iStarNum;
    }
}
