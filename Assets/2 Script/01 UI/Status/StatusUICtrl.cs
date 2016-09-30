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

    public PlayerCtrl player;

    void Start()
    {
        MAXHP = 100f;
        MAXSTAMINA = 100f;
        MAXBLOOD = 100f;
        hpImage = GameObject.Find("Heart").GetComponent<Image>();
        staminaImage = GameObject.Find("StaminaBar").GetComponent<Image>();
        bloodImage = GameObject.Find("BloodBar").GetComponent<Image>();

        player = GameObject.FindObjectOfType<PlayerCtrl>();

    }

    void Update()
    {
        SetPlayerHPBar();
        SetPlayerStaminaBar();
        SetPlayerBloodBar();
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

}
