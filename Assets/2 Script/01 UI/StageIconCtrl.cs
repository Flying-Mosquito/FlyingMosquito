using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class StageIconCtrl : MonoBehaviour {

    public Image image;
    public Text stageText;

    public int iWorldNum;
    public int iStageNum;

    public int iStarNum;        // 스테이지에서 받은 별의 개수

    public Sprite CloseImage;
    public Sprite OpenImage;
    public Sprite CompleteImage;

    public Sprite EmptyStarImage;
    public Sprite StarImage;

    public List<Image> StarList; 
    // Use this for initialization
    void Start ()
    {
        iStarNum = 0;
        image = GetComponent<Image>();
        stageText = GetComponentInChildren<Text>();

        for (int i = 0; i < 3; ++i)
            StarList.Add(GetComponentsInChildren<Image>()[i+1]);

        stageText.text = (iStageNum+1).ToString();

        iStarNum = WorldStageManager.Instance.worldmapList[iWorldNum].stageList[iStageNum].iStarNum;
        print("iWorldNum :  " + iWorldNum + ", iStageNum : " + iStageNum + ", imageChoice  = " + iStarNum);
 
        if (iStarNum == -1)
        {
            image.sprite = CloseImage;
            stageText.enabled = false;
        }
        else if (iStarNum == 0)
        {
            image.sprite = OpenImage;
            stageText.enabled = true;
            SetStageStar();
        }
        else
        {
            image.sprite = CompleteImage;
            stageText.enabled = true;
            SetStageStar();
        }
        
    }

    public void EnterStage()
    {
        // 임시적으로 하긴 했는데, world가 늘어나면 바뀌어야함 
        GameManager.Instance.StartCoroutine("StartSceneLoadWithLoading", "Stage" + (iStageNum+1).ToString());
    }

    public void SetStageStar()
    {
        for( int i = 0; i < 3; ++i )
        {
            if (i < iStarNum)
            {
                StarList[i].sprite = StarImage;
            }
            else
                StarList[i].sprite = EmptyStarImage;
        }
    }

}
