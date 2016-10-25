using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// MyUI 에 넣어줌 
public class StageUICtrl : TimeAffectedObj
{
    public Text LimitTimeText;
    public float fLimitTime;

    public GameObject GameOverPanel;
    public GameObject OptionPanel;
    public GameObject GameClearPanel;

    //////// 스테이지클리어를 위한 조건 변수
    public int iDestinyNum;             // 도착지 수
    private int iDestinyCount;          // 도착한 도착지 수


    void Start()
    {
        iDestinyCount = 0;
        fLimitTime = 20f;
        LimitTimeText = GameObject.Find("LimitTime").GetComponent<Text>();
        LimitTimeText.text = fLimitTime.ToString();
    }

    public void IncreaseDestinyCount()
    {
        ++iDestinyCount;

        if(iDestinyCount >= iDestinyNum) // 클리어 - 함수로 만드는것이 나을 듯 함
        {
            ShowGameClear();
        }
    }

    public override void MyUpdate()
    {
        ShowLimitTimer();
        
    }

    public void SetTimer(int _time)
    {
        fLimitTime = _time;
    }
    public void ShowLimitTimer()
    {
        if (fLimitTime < 0f)
            return;

        fLimitTime -= Time.deltaTime;


        if (fLimitTime < 0f)   // 게임오버라던가 그런거 띄우는처리 해줘야함 
            GameOverPanel.SetActive(true);

        int toIntTime = (int)fLimitTime;

        LimitTimeText.text = string.Format("{0:D3}", toIntTime);

    }

    private void ShowGameClear()
    {
        print("ShowGameClear!!!!");
        GameClearPanel.SetActive(true);
    }

    public void PressMainMap()
    {
        //GameManager.Instance.StartCoroutine("StartSceneLoadWithLoading", "02 map");

        TimeManager.Instance.StartCoroutine("SetTimeStop", false);
        TimeManager.Instance.DeleteAllObj();
        SoundManager.Instance.ClearSoundList();
        GameManager.Instance.StartCoroutine("StartLoad", "02 map");
    }

 
    public void PressRetry()
    {
        // GameManager.Instance.StartCoroutine("StartReload");
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        TimeManager.Instance.DeleteAllObj();
        TimeManager.Instance.StartCoroutine("SetTimeStop", false);

        SoundManager.Instance.ClearSoundList();
        GameManager.Instance.StartCoroutine("StartReload");
    }
    // 나중가서는 World정보와, StageNum정보를 같이 보내줘야 함
    public void PressNextStage(int _iCurrStageNum)
    {
        int iNextStageNum = _iCurrStageNum + 1;

        TimeManager.Instance.DeleteAllObj();
        TimeManager.Instance.StartCoroutine("SetTimeStop", false);

        SoundManager.Instance.ClearSoundList();
        //점수설정 추가해야함 


        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + iNextStageNum.ToString());

    }
    public void PressOption()
    {
        print("PressOption");
        OptionPanel.SetActive(true);
    }
    public void PressCancel(Transform _tr) // 부모의 Active상태 false로 만듦
    {
        print("PressCancel");
        _tr.parent.gameObject.SetActive(false);
    }


}

