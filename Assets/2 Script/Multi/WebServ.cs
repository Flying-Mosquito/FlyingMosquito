using UnityEngine;
using System.Collections;

public class WebServ : MonoBehaviour {

    public string UserData;
    public Login login;
	// Use this for initialization
	void Start () {
       login= GameObject.FindObjectOfType<Login>();

    }
	
	// Update is called once per frame
	void Update () {
	if(Application.loadedLevelName == "login")
        {

            UserData = login.AccessNumber;
            print(UserData);

        }


    }
}
