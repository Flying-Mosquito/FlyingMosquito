using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterState : MonoBehaviour

{

    public Scrollbar HealthBar;
    public Scrollbar StaminaBar;
    public Scrollbar bloodBar;

   public PlayerCtrl playerctrl;
    public Timer timer;

    void Start () {


        playerctrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
       timer=  GameObject.Find("Timer").GetComponent<Timer>();



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
       timer.totaltime = 60;
        timer.isEnable = true;
        //PlayerCtrl.Instance.SetStateIdle(false);
        playerctrl.SetIsInStage(true);
       
    }
}
