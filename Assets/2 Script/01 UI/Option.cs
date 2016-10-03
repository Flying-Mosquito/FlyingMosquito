using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour {
    public GameObject option_back;
    public Button exit;
    public Button back;
    // Use this for initialization
    public Timer timer;
    public PlayerCtrl playerctrl;
    public bool backBool;
	void Start () {
        playerctrl = GameObject.FindObjectOfType<PlayerCtrl>();
        timer = GameObject.FindObjectOfType<Timer>();
    }
	
    public void OpenMenu()
    {
        if (!TimeManager.Instance.isMenuOpen)
        {
            print("오픈메뉴");
            option_back.SetActive(true);
            //TimeManager.Instance.SetOption(true);
            TimeManager.Instance.StartCoroutine("SetOption", true);
        }
    }

    public void Continue()
    {
        //TimeManager.Instance.SetOption(false);
        TimeManager.Instance.StartCoroutine("SetOption", false);
        option_back.SetActive(false);
        
    }
    public void gomap()
    {
        SceneManager.LoadScene(1);
    }
    public void Click()
    {
        playerctrl.fBlood = 0;
        playerctrl.SetParentNull();
        playerctrl.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //TimeManager.Instance.SetOption(false);
        TimeManager.Instance.StartCoroutine("SetOption", false);
        timer.gameover.gameObject.SetActive(false);
        timer.gameClear.gameObject.SetActive(false);
        TimeManager.Instance.DeleteAllObj();
        SoundManager.Instance.ClearSoundList();
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
