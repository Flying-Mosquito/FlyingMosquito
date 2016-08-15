using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterState : MonoBehaviour

{

    public Scrollbar HealthBar;
    public Scrollbar StaminaBar;
    public Scrollbar bloodBar;

    PlayerCtrl playerctrl;


    void Start () {


        playerctrl = GameObject.FindObjectOfType<PlayerCtrl>();// GameObject.Find("Player").GetComponent<PlayerCtrl>();




    }
	
	// Update is called once per frame
	void Update () {

        HealthBar.value =playerctrl.iHP / 100f;

        StaminaBar.size = playerctrl.fStamina/ 100f;
        bloodBar.value = playerctrl.iBlood /200f;
    }
    public void gamestart()
    {
        playerctrl.SetHP(75);
        playerctrl.iBlood = 0;
        playerctrl.fStamina = 200;
        
        Timer.Instance.totaltime = 60;
        Timer.Instance.isEnable = true;
        //PlayerCtrl.Instance.SetStateIdle(false);
        playerctrl.SetIsInStage(true);
       
    }
}
