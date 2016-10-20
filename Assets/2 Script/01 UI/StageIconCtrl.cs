using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StageIconCtrl : MonoBehaviour {

    public Image image;
    public Text stageText;

    public int iWorldNum;
    public int iStageNum;
    public Sprite CloseImage;
    public Sprite OpenImage;
    public Sprite CompleteImage;

	// Use this for initialization
	void Start ()
    {
        image = GetComponent<Image>();
        stageText = GetComponentInChildren<Text>();
        stageText.text = (iStageNum+1).ToString();

        int imageChoice = WorldStageManager.Instance.worldmapList[iWorldNum].stageList[iStageNum].iStarNum;
        print("iWorldNum :  " + iWorldNum + ", iStageNum : " + iStageNum + ", imageChoice  = " + imageChoice);
 
        if (imageChoice == -1)
        {
            image.sprite = CloseImage;
        }
        else if (imageChoice == 0)
        {
            image.sprite = OpenImage;
        }
        else
        {
            image.sprite = CompleteImage;
        }
    }

    public void EnterStage()
    {
        // 임시적으로 하긴 했는데, world가 늘어나면 바뀌어야함 
        GameManager.Instance.StartCoroutine("StartSceneLoadWithLoading", "Stage" + (iStageNum+1).ToString());
    }

}
