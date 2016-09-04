using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Single : MonoBehaviour
{
    public Button exit;
    public Button back;
    // Use this for initialization

    public Login loginScript;
    public WebServ WebServScript;

    public bool backBool;
    void Start()
    {
        loginScript = gameObject.AddComponent<Login>();
        WebServScript = gameObject.AddComponent<WebServ>();
        loginScript.enabled = false;
        WebServScript.enabled = false;

    }
    public void MainS()
    {
        SceneManager.LoadScene(1);
    }
    public void Click()
    {


        // SceneManager.LoadScene(13);

        WebServScript.enabled = true;
        loginScript.enabled = true;

        //PlayerCtrl.Instance.state = Constants.ST_IDLE;
     

       
    }
    public void Multi()

    {
        SceneManager.LoadScene(13);
    }
}
