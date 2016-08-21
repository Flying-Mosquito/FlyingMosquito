using UnityEngine;
using System.Collections;

public class WebServ : MonoBehaviour
{

    public string[] UserData = new string[3];
    public Login login;
    // Use this for initialization
    void Start()
    {
        login = GameObject.FindObjectOfType<Login>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName == "login")
        {



        }


    }
}
