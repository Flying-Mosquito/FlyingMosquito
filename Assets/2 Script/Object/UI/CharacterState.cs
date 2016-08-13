using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterState : MonoBehaviour

{

    public Scrollbar HealthBar;
    public Scrollbar StaminaBar;
    public Scrollbar bloodBar;




    void Start () {


       
      



    }
	
	// Update is called once per frame
	void Update () {

        HealthBar.value = PlayerCtrl.Instance.iHP / 100f;

        StaminaBar.size = PlayerCtrl.Instance.fStamina/ 100f;
        bloodBar.value = PlayerCtrl.Instance.iBlood /200f;
    }
    public void gamestart()
    {
        PlayerCtrl.Instance.SetHP(75);
        PlayerCtrl.Instance.iBlood = 0;
        PlayerCtrl.Instance.fStamina = 200;
        Timer.Instance.totaltime = 60;
        Timer.Instance.isEnable = true;
        //PlayerCtrl.Instance.SetStateIdle(false);
        PlayerCtrl.Instance.SetIsInStage(true);
       
    }
}
