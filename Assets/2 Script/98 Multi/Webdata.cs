using UnityEngine;
using System.Collections;

public class Webdata : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        print(GetStage.Instance.myStage);
        print(Login.Instance.userData);
	}
}
