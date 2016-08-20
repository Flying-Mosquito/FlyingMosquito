using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour {
    public Button exit;
    public Button back;
    // Use this for initialization

    public PlayerCtrl playerctrl;
    public bool backBool;
	void Start () {
        playerctrl = GameObject.FindObjectOfType<PlayerCtrl>();

    }
	
	// Update is called once per frame
	void Update () {

    }
    public void Click()
    {
       
            playerctrl.iBlood = 0;
            playerctrl.SetParentNull();
            playerctrl.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Timer.Instance.gameover.gameObject.SetActive(false);
            Timer.Instance.gameClear.gameObject.SetActive(false);

            SceneManager.LoadScene(1);

            //PlayerCtrl.Instance.state = Constants.ST_IDLE;
            //PlayerCtrl.Instance.variable &= ~(Constants.BV_Stick);
            // //   PlayerCtrl.Instance.SetStateIdle(true);
            // PlayerCtrl.Instance.SetIsInStage(false);

            // Timer.Instance.totaltime = 60;
            // Timer.Instance.isEnable = true;
        
    }
    public void SetPlayer()
    {
        playerctrl = GameObject.FindObjectOfType<PlayerCtrl>(); //GameObject.Find("Player").GetComponent<PlayerCtrl>();

    }
    public void intoMulti()
    {
        SceneManager.LoadScene(14);
    }
    public void backLobby()
    {
        SceneManager.LoadScene(14);
    }
    public void IntoRoom()
    {
        SceneManager.LoadScene(15);
    }
}
