using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using UnityEngine.SceneManagement;

public class GetStage : Singleton<GetStage>
{

    private string GetStageUrl = "http://192.168.1.105:8080/get_stage_list";
    private string AccessNumber = "";
    public string myStage = "";
    public Login login;

    void Start()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(GetStagelist());
        login = GameObject.FindObjectOfType<Login>(); ;
    }

    private IEnumerator GetStagelist()
    {
        WWWForm form = new WWWForm();
        form.AddField("code",login.AccessNumber );

        WWW www = new WWW(GetStageUrl, form);

        yield return www;
        if (www.error != null)
        {
            Debug.Log("get stage error");
        }
        else
        {
            myStage = www.text;

        }
    }
}