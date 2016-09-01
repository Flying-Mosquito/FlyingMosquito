using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    public Button exit;
    public Button back;
    // Use this for initialization
    public int[] stagenum=new int[9];
  

    public bool backBool;
    void Start()

    {
        
     
     

    }

    // Update is called once per frame
   
   
    public void Stage1()
    {

       
       
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[0].ToString());


    }
    public void Stage2()
    {
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[1].ToString());

    }
    public void Stage3()
    {
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[2].ToString());

    }
    public void Stage4()
    {
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[3].ToString());

    }
    public void Stage5()
    {
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[4].ToString());

    }
    public void Stage6()
    {
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[5].ToString());

    }
    public void Stage7()
    {
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[6].ToString());

    }
}
