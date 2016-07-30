using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Single : MonoBehaviour
{
    public Button exit;
    public Button back;
    // Use this for initialization


    public bool backBool;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Click()
    {

        SceneManager.LoadScene(1);

        //PlayerCtrl.Instance.state = Constants.ST_IDLE;
        PlayerCtrl.Instance.variable &= ~(Constants.BV_Stick);
        PlayerCtrl.Instance.SetStateIdle(true);

        if (Timer.Instance != null)
        {
            Timer.Instance.totaltime = 60;
            Timer.Instance.isEnable = true;
        }
    }
    public void Multi()

    {
        SceneManager.LoadScene(14);
    }
}
