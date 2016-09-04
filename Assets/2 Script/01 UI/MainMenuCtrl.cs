using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuCtrl : MonoBehaviour
{
    private Login loginScript;
    private WebServ WebServScript;

    void Start()
    {
        loginScript = gameObject.AddComponent<Login>();
        WebServScript = gameObject.AddComponent<WebServ>();
        loginScript.enabled = false;
        WebServScript.enabled = false;
    }

    public void ClickGameStart()
    {
        WebServScript.enabled = true;
        loginScript.enabled = true;
    }
}
