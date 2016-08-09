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

        print(Application.loadedLevelName);
        SceneManager.LoadScene(stagenum[0]);
        
        
        
    }
    public void Stage2()
    {
        SceneManager.LoadScene(stagenum[1]);
        
    }
    public void Stage3()
    {
        SceneManager.LoadScene(stagenum[2]);

    }
    public void Stage4()
    {
        SceneManager.LoadScene(stagenum[3]);

    }
    public void Stage5()
    {
        SceneManager.LoadScene(stagenum[4]);

    }
    public void Stage6()
    {
        SceneManager.LoadScene(stagenum[5]);

    }
}
