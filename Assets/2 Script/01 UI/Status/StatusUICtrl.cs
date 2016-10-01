using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// 플레이어에게 넣어준다
public class StatusUICtrl : MonoBehaviour {

    private float MAXHP;
    private float MAXSTAMINA;
    private float MAXBLOOD;
    public Image hpImage;
    public Image staminaImage;
    public Image bloodImage;
    public Text LimitTimeText;
    public float fLimitTime;


    public PlayerCtrl player;

    void Start()
    {
        fLimitTime = 100f;
        MAXHP = 100f;
        MAXSTAMINA = 100f;
        MAXBLOOD = 100f;
        hpImage = GameObject.Find("Heart").GetComponent<Image>();
        staminaImage = GameObject.Find("StaminaBar").GetComponent<Image>();
        bloodImage = GameObject.Find("BloodBar").GetComponent<Image>();
        LimitTimeText = GameObject.Find("LimitTime").GetComponent<Text>();
        LimitTimeText.text = fLimitTime.ToString();

        player = GameObject.FindObjectOfType<PlayerCtrl>();

    }

    void Update()
    {
        SetPlayerHPBar();
        SetPlayerStaminaBar();
        SetPlayerBloodBar();
        //System.Text.StringBuilder
        
     
            SetLimitTimer();
    }

    public void SetPlayerHPBar()
    {
        hpImage.fillAmount = player.fHP / MAXHP;
    }

    public void SetPlayerStaminaBar()
    {
        staminaImage.fillAmount = player.fStamina / MAXSTAMINA;
    }
    public void SetPlayerBloodBar()
    {
        bloodImage.fillAmount = player.iBlood / MAXBLOOD;
    }
    public void SetLimitTimer()
    {
        if (fLimitTime < 0f)
            return;

            fLimitTime -= Time.deltaTime;

        // 0~1사이는 따로 처리해줘야 할듯
        if (fLimitTime < 0f)   // 게임오버라던가 그런거 띄우는처리 해줘야함 
            return;

        int toIntTime = (int)fLimitTime;



        LimitTimeText.text = string.Format("{0:D3}", toIntTime);
     

    }

}
