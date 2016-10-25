using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// MyUI 에 넣어줌 
public class StageUICtrl : TimeAffectedObj
{
    public Text LimitTimeText;
    public float fLimitTime;

    public GameObject GameOverPannel;
    public GameObject OptionPannel;

    public GameObject DestinyObj;

    void Start ()
    {
        fLimitTime = 20f;
        LimitTimeText = GameObject.Find("LimitTime").GetComponent<Text>();
        LimitTimeText.text = fLimitTime.ToString();
    }

    public override void MyUpdate()
    {
        ShowLimitTimer();
    }

 
    void OnCollisionEnter(Collision _coll)
    {
        if(_coll.gameObject == DestinyObj)
        {
            
        }
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
            GameOverPannel.SetActive(true);

        int toIntTime = (int)fLimitTime;

        LimitTimeText.text = string.Format("{0:D3}", toIntTime);
    }
    public void PressMainMap()
    {
        //GameManager.Instance.StartCoroutine("StartSceneLoadWithLoading", "02 map");

        TimeManager.Instance.StartCoroutine("SetTimeStop", false);
        TimeManager.Instance.DeleteAllObj();
        SoundManager.Instance.ClearSoundList();
        GameManager.Instance.StartCoroutine("StartLoad", "02 map");
    }

    public void PressOption()
    {
        print("PressOption");
        OptionPannel.SetActive(true);
    }
    public void PressCancel(Transform _tr) // 부모의 Active상태 false로 만듦
    {
        print("PressCancel");
        _tr.parent.gameObject.SetActive(false);
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

}

