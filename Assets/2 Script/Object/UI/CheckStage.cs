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
    SelectStage SelectStage;
   

    private int MAXINDEX = 3;
    private int[] Arr = new int[3] {0,1,2};


    // Use this for initialization
    void Start () {

        Arr = new int[iStage];
        print("dddd");
        for(int i=0;i<9;i++)
        {
            Arr[i] =0;//0닫힘 1주황 2 초록
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 9; i++)
        {
            if (PlayerCtrl.Instance.iBlood > 190 && SelectStage.stage[i])
            {
                Arr[i] = 1;
            }
        }
            //1성공 2닫힘
            if (Arr[0]==1&& Arr[1]==0)
        {
            Stage2.Find("2").gameObject.SetActive(true);
        }
            if(Arr[1] == 2)
        {
            Stage2.Find("2-1").gameObject.SetActive(true);
        }
     
     
	}
}
