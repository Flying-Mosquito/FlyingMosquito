using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mapStruct 
{
    public int worldNum;
    public List<StageStarInfo> stageList;

    public mapStruct(int _iWorldNum)
    {
        worldNum = _iWorldNum;
        stageList = new List<StageStarInfo>();
    }
    
}
