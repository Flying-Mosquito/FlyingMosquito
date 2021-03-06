﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System;
using UnityEngine.SceneManagement;

public class Login : Singleton<Login>
{
    #region Variables
    
    //Static Variables
    public static string Id = "";
    public static string Password = "";
    public static string NickName = "";
    //Public Variables
    public string CurrentMenu = "Login";
    //Private Variables
    private string CheckDupUrl = "http://192.168.83.221:8080/check_duplicate";
    private string CreateAccountUrl = "http://192.168.83.221:8080/make_account";
    private string CheckLoginUrl = "http://192.168.83.221:8080/check_login";
    private string TakeDataUrl = "http://192.168.83.221:8080/get_data";
    private string GetStageUrl = "http://192.168.83.221:8080/get_stage_list";
    private string EnterStageUrl = "http://192.168.83.221:8080/enter_stage";
    private string EndStageUrl = "http://192.168.83.221:8080/end_stage";

    // private string LoginUrl = "";
    private string ConfrimPass = "";
    private string ConfirmEmail = "";
    private string CEmail = "";
    private string Cpassword = "";
    private string Cnickname = "";
    public string AccessNumber = "";
    private string usrData = "";
    public string[] userData;
    private int ActiveNum = 4;
    //GUI Test section

    public float X;
    public float Y;
    public float Width;
    public float Height;
    public float control = 60;

    private WWW www;
    #endregion 

    void Start()
    {
        www = new WWW("http://");
    }
    void OnGUI()
    {
       // WWW www = new WWW("http://");
        

        if (CurrentMenu == "Login")
        {
            LoginGUI();
            
        }
        else if (CurrentMenu == "CreateAccount")
        {
            CreateAccountGUI();
        }

    }

    #region Custom methods

    void LoginGUI()
    {

        //GUI.Box(new Rect(280 - control * 2, 120 - control, (Screen.width / 2) + 200, (Screen.height / 2) + 250), "Login");
        float fLeft = Screen.width / 10f;
        float ftop = Screen.height / 10f;
        float fRight = Screen.width - (Screen.width / 10f * 2f);
        float fBottom = Screen.height - (Screen.height / 10f * 2f);

        GUI.Box(new Rect(fLeft
                        , ftop
                        , fRight
                        , fBottom )
                        , "Login");

        //GUI.Label(new Rect(390 - control * 2, 200 - control, 220, 25), "ID");
        GUI.Label(new Rect(Screen.width/2 - Screen.width/5
                                , Screen.height/2 - Screen.height/5
                                ,( Screen.width / 2 - Screen.width / 5) + 50
                                , (Screen.height / 2 - Screen.height / 5) +5 )
                                , "ID");
        Id = GUI.TextField(new Rect(Screen.width / 2 - Screen.width / 5
                                , (Screen.height / 2 - Screen.height / 5) + 20
                                , (Screen.width / 2 - Screen.width / 5) + 50
                                , 20)
                                , Id);

        GUI.Label(new Rect(Screen.width / 2 - Screen.width / 5
                                , Screen.height / 2 
                                , (Screen.width / 2 - Screen.width / 5) + 50
                                , (Screen.height / 2) + 5)
                                , "Password");

        Password = GUI.TextField(new Rect(Screen.width / 2 - Screen.width / 5
                                , (Screen.height / 2 ) + 20
                                , (Screen.width / 2 - Screen.width / 5) + 50
                                , 20)
                                , Password);






        //if (GUI.Button(new Rect(360 - control * 2, 360 - control, 120, 25), "Create Account"))
        if (GUI.Button(new Rect(Screen.width/2f - 150f//fLeft + (fRight / 5f)
                                ,fBottom - (fBottom/ 8f)
                                , 120
                                , 25)
                                , "Create Account"))
        {
            CurrentMenu = "CreateAccount";
        }


        // if (GUI.Button(new Rect(520 - control * 2, 360 - control, 120, 25), "Log in"))
        //  if (GUI.Button(new Rect(520 - control * 2, 360 - control, 120, 25), "Log in"))
        if (GUI.Button(new Rect(Screen.width / 2f + 30f//fRight - (fRight / 5f)
                               , fBottom - (fBottom / 8f)
                               , 120
                               , 25)
                               , "Login"))
        {
            WWWForm form = new WWWForm();
            form.AddField("user_id", Id);
            form.AddField("user_passwd", Password);

            StartCoroutine(CreateAccount(CheckLoginUrl, form));

            form = new WWWForm();
            form.AddField("code", AccessNumber);
            StartCoroutine(CreateAccount(TakeDataUrl, form));

           // if (ActiveNum == 10)   
                SceneManager.LoadScene(1);
          

        }

    }
    public void Click()
    {

        if (GUI.Button(new Rect(520 - control * 2, 360 - control, 120, 25), "Log in"))
        {
            SceneManager.LoadScene(1);

        }

    }
    void CreateAccountGUI()
    {
        GUI.Box(new Rect(280 - control * 2, 120 - control, (Screen.width / 4) + 200, (Screen.height / 4) + 250), "Create Account");


        GUI.Label(new Rect(390 - control * 2, 200 - control, 220, 25), "Id");
        CEmail = GUI.TextField(new Rect(390 - control * 2, 225 - control, 220, 25), CEmail);

        GUI.Label(new Rect(390 - control * 2, 250 - control, 220, 25), "Password");
        Cpassword = GUI.TextField(new Rect(390 - control * 2, 275 - control, 220, 25), Cpassword);


        GUI.Label(new Rect(390 - control * 2, 320 - control, 220, 25), "NickName");
        Cnickname = GUI.TextField(new Rect(390 - control * 2, 340 - control, 220, 25), Cnickname);


        if (GUI.Button(new Rect(360 - control * 2, 460 - control, 120, 25), "Create Account"))
        {
            WWWForm form = new WWWForm();
            form.AddField("user_id", CEmail);
            form.AddField("user_nickname", Cnickname);

            StartCoroutine(CreateAccount(CheckDupUrl, form));

            if (ActiveNum == 1)
            {
                form = new WWWForm();
                form.AddField("user_id", CEmail);
                form.AddField("user_nickname", Cnickname);
                form.AddField("user_passwd", Cpassword);
                StartCoroutine(CreateAccount(CreateAccountUrl, form));
                if (ActiveNum == 5)
                {
                    CurrentMenu = "Login";
                }
            }
            else if (ActiveNum == 0)
            {
                Debug.Log("There is same ID or Nickname already");
            }
        }
        if (GUI.Button(new Rect(520 - control * 2, 460 - control, 120, 25), "Back"))
        {
            CurrentMenu = "Login";
        }
    }

    #endregion

    #region CoRoutines
    private IEnumerator CreateAccount(string url, WWWForm form)
    {

        www = new WWW(url, form);
        //Debug.Log(Convert.ToInt64(www.text));
        //print("www.bytesDownloaded : " + www.bytesDownloaded);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError("Cannot Connect to Account Creation");
            Debug.Log(www.error);
        }
        else
        {
            long textReturn =Convert.ToInt64( www.text);
            if (url == CheckDupUrl)
            {
                if (textReturn.ToString() == "0")
                {
                    print("ActiveNum = 1");
                    ActiveNum = 1;  // 중복이 아닐때 (계정 생성 가능할때)
                }
                else if (textReturn.ToString() == "1")
                {
                    print("ActiveNum = 0");
                    ActiveNum = 0;  // 중복일때
                }
            }
            else if (url == CheckLoginUrl)
            {
                AccessNumber = textReturn.ToString();
                AccessNumber = AccessNumber.Insert(AccessNumber.Length, "L");
                print("CheckLoginUrl");
            }
            else if (url == CreateAccountUrl)
            {
                print("active = 5");
                if (textReturn.ToString() == "make account OK")
                    ActiveNum = 5;  // 계정 생성 완료
            }
            else if (url == TakeDataUrl)
            {
                print("active = 10");
                usrData = textReturn.ToString();
                userData = usrData.Split('/');
                ActiveNum = 10;
            }
        }
    }
    #endregion
}