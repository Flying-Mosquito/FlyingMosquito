using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    public Button exit;
    public Button back;
    // Use this for initialization
    public int[] stagenum=new int[12];
  

    public bool backBool;
    void Start()

    {
        
     
     

    }

    // Update is called once per frame
   
   
    public void Stage1()
    {

        SoundManager.Instance.ClearSoundList();
       
       // GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[0].ToString());
       GameManager.Instance.StartCoroutine("StartSceneLoadWithLoading", "Stage" + stagenum[0].ToString());

    }
    public void Stage2()
    {
        SoundManager.Instance.ClearSoundList();
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[1].ToString());

    }
    public void Stage3()
    {
        SoundManager.Instance.ClearSoundList();
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[2].ToString());

    }
    public void Stage4()
    {
        SoundManager.Instance.ClearSoundList();
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[3].ToString());

    }
    public void Stage5()
    {
        SoundManager.Instance.ClearSoundList();
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[4].ToString());

    }
    public void Stage6()
    {
        SoundManager.Instance.ClearSoundList();
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[5].ToString());

    }
    public void Stage7()
    {
        SoundManager.Instance.ClearSoundList();
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[6].ToString());

    }
    public void Stage8()
    {
        SoundManager.Instance.ClearSoundList();
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[7].ToString());

    }
    public void Stage9()
    {
        SoundManager.Instance.ClearSoundList();
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[8].ToString());

    }
    public void Stage10()
    {
        SoundManager.Instance.ClearSoundList();
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[9].ToString());

    }
    public void Stage11()
    {
        SoundManager.Instance.ClearSoundList();
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[10].ToString());

    }
    public void Stage12()
    {
        SoundManager.Instance.ClearSoundList();
        GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stagenum[11].ToString());

    }
  

}
