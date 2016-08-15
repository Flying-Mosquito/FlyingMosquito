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
    public void MainS()
    {
        SceneManager.LoadScene(1);
    }
    public void Click()
    {

        SceneManager.LoadScene(13);

        //PlayerCtrl.Instance.state = Constants.ST_IDLE;
     

        if (Timer.Instance != null)
        {
            Timer.Instance.totaltime = 60;
            Timer.Instance.isEnable = true;
        }
    }
    public void Multi()

    {
        SceneManager.LoadScene(16);
    }
}
