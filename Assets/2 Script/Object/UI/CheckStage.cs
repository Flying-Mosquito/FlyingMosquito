using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


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
    private int iStage = 9;
    public List<Transform> Stage = new List<Transform>();
    public bool clear;
    private int MAXINDEX = 3;  
    private int[] Arr = { };
   
    
    // Use this for initialization
    void Start () {

        Arr = new int[iStage];
        clear = true;
        for(int i=0;i<9;i++)
        {
            Arr[i] = 0;//0닫힘 1주황 2 초록
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 9; i++)
        {

            if (Arr[i-1]==1&&Arr[i]==1)
            {
             
            }
            
        }
       
     
	}
}
