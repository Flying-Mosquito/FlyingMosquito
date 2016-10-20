using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class WorldStageManager : Singleton<WorldStageManager> {

    public List<mapStruct> worldmapList;//= new List<mapStruct>();
    int iTotalWorldNum = 2;
    int iMaxStageNumInWorld = 8;
  
   
	// Use this for initialization
	void Awake ()
    {
        DontDestroyOnLoad(this);

        worldmapList = new List<mapStruct>();
        
        for (int i = 0; i < iTotalWorldNum; ++i)                 // World생성 후, 스테이지들을 닫힌 상태로 초기화
        {
            worldmapList.Add(new mapStruct(i));

            print("i는 : " + i);
           // mapStruct worldStruct = new mapStruct();
            //worldStruct.worldNum = i;
            //worldStruct.stageList = new List<StageStarInfo>();
           

            for ( int j = 0; j < iMaxStageNumInWorld; ++j)      //  하나의 World를 구성하고 있는 stage를 iMaxStageNumInWorld의 숫자만큼 만들어줌
            {
                
                StageStarInfo stageStarInfo = new StageStarInfo(j,-1);      // iStarNum이 -1이면 아직 활성화 되지 않은 스테이트  
                worldmapList[i].stageList.Add(stageStarInfo);
                //stageStarInfo.iStageNum = j;
                // stageStarInfo.iStarNum = -1;   
            }
        }

        worldmapList[0].stageList[0].SetStarNum(0);

        /*
        print("****에러가 안남 : WorldStageManager.Instance.worldmapList[iWorldNum].stageList[iStageNum] : " + WorldStageManager.Instance.worldmapList[0].worldNum);
        if (WorldStageManager.Instance.worldmapList[0] == null)
            print("***null2");

        print("worldmapList[0].stageList[2].iSiStarNumtageNum  : " + worldmapList[0].stageList[2].iStarNum );

        ((worldmapList[0]).stageList[2]).iStarNum = 0;
        */
        // worldmapList[0].stageList.Find()

    }
	
	// Update is called once per frame
	void Update ()
    {

	}
}
