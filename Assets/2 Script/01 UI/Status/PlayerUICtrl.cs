using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// MyUI에넣어준다 
public class PlayerUICtrl : TimeAffectedObj {

    private float MAXHP;
    private float MAXSTAMINA;
    private float MAXBLOOD;
    public Image hpImage;
    public Image staminaImage;
    public Image bloodImage;
 
    //public Text LimitTimeText;
    //public float fLimitTime;

    //public GameObject GameOverPannel;
    //public GameObject OptionPannel;
  //  public bool bStartTimer;


    public PlayerCtrl player;
    public override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerCtrl>();
        hpImage = GameObject.Find("Heart").GetComponent<Image>();
        staminaImage = GameObject.Find("StaminaBar").GetComponent<Image>();
        bloodImage = GameObject.Find("BloodBar").GetComponent<Image>();

        //fLimitTime = 20f;
        //LimitTimeText = GameObject.Find("LimitTime").GetComponent<Text>();
        //LimitTimeText.text = fLimitTime.ToString();

        MAXHP = 100f;
        MAXSTAMINA = 100f;
        MAXBLOOD = 100f;
        //bStartTimer = false;
    }

    public override void MyUpdate()
    {
        ShowPlayerHPBar();
        ShowPlayerStaminaBar();
        ShowPlayerBloodBar();
        //System.Text.StringBuilder
        
            //ShowLimitTimer();
    }
    public void ShowPlayerHPBar()
    {
        hpImage.fillAmount = player.fHP / MAXHP;
    }
    public void ShowPlayerStaminaBar()
    {
        staminaImage.fillAmount = player.fStamina / MAXSTAMINA;
    }
    public void ShowPlayerBloodBar()
    {
        bloodImage.fillAmount = player.fBlood / MAXBLOOD;
    }
    //public void SetTimer(int _time)
    //{
    //    fLimitTime = _time;
    //}
    //public void ShowLimitTimer()
    //{
    //    if (fLimitTime < 0f)
    //        return;

    //        fLimitTime -= Time.deltaTime;

      
    //    if (fLimitTime < 0f)   // 게임오버라던가 그런거 띄우는처리 해줘야함 
    //        GameOverPannel.SetActive(true);

    //    int toIntTime = (int)fLimitTime;

    //    LimitTimeText.text = string.Format("{0:D3}", toIntTime);
    //}

    //public void PressMainMap()
    //{
    //    //GameManager.Instance.StartCoroutine("StartSceneLoadWithLoading", "02 map");

    //    TimeManager.Instance.StartCoroutine("SetTimeStop", false);
    //    TimeManager.Instance.DeleteAllObj();
    //    SoundManager.Instance.ClearSoundList();
    //    GameManager.Instance.StartCoroutine("StartLoad", "02 map");
    //}

    //public void PressOption()
    //{
    //    print("PressOption");
    //    OptionPannel.SetActive(true);
    //}
    //public void PressCancel(Transform _tr)
    //{
    //    print("PressCancel");
    //    _tr.parent.gameObject.SetActive(false);
    //}
    //public void PressRetry()
    //{
    //    // GameManager.Instance.StartCoroutine("StartReload");
    //    //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

    //    TimeManager.Instance.DeleteAllObj();
    //    TimeManager.Instance.StartCoroutine("SetTimeStop", false);
        
    //    SoundManager.Instance.ClearSoundList();
    //    GameManager.Instance.StartCoroutine("StartReload");

    //}

}
