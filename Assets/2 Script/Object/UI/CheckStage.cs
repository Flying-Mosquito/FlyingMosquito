using UnityEngine;
using System.Collections;

public class CheckStage : MonoBehaviour {
    public Transform Stage1;
    public Transform Stage2;
    public Transform Stage3;
    public Transform Stage4;
    public Transform Stage5;
    public Transform Stage6;
    public Transform Stage7;
    public Transform Stage8;
    public Transform Stage9;
    public bool clear;

 
    // Use this for initialization
    void Start () {

        clear = true;
   
    }
	
	// Update is called once per frame
	void Update () {
        if(clear==false)
        {
            Stage1.Find("1-1").gameObject.SetActive(true);
          
        }
        if(clear==true)
        {
         
        }
       
     
	}
}
