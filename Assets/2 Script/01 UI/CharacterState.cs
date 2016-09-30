using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterState : MonoBehaviour

{

   
    public Scrollbar StaminaBar;
    public Scrollbar bloodBar;

    PlayerCtrl playerctrl;
    Timer timer;

    void Start () {


        playerctrl = GameObject.FindObjectOfType<PlayerCtrl>();// GameObject.Find("Player").GetComponent<PlayerCtrl>();
      timer = GameObject.FindObjectOfType<Timer>();



    }
	
	// Update is called once per frame
	void Update () {

       

        StaminaBar.size = playerctrl.fStamina/ 100f;
        bloodBar.value = playerctrl.iBlood / 200;
    }
    public void gamestart()
    {
        // playerctrl.SetHP(100);
        // playerctrl.iBlood = 0;


       // print("이름은 : " + transform.gameObject.tag);
    //   timer.isEnable = true;
        //PlayerCtrl.Instance.SetStateIdle(false);
        //TimeManager.Instance.StartCoroutine("SetOption", false);
        //playerctrl.SetIsInStage(true);


        // FlyBtCtrl flybt = GameObject.FindObjectOfType<FlyBtCtrl>();
        //ClingBtCtrl clingbt = GameObject.FindObjectOfType<ClingBtCtrl>();
        //clingbt.SetPlayer(playerctrl);
        //flybt.SetPlayer(playerctrl);

    }
}
