using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    public Button exit;
    public Button back;
    // Use this for initialization
   
    public  int[] stage= new int[2] { 0, 1 }  ;
   
    public bool backBool;
    void Start()

    {
        
        stage = new int[9];
        for(int i=0;i<9;i++)
        {
            stage[i] =1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName == "Stage1")
        {
            stage[0] = 0;
        }

    }
   
    public void Stage1()
    {

        print(Application.loadedLevelName);
        SceneManager.LoadScene(3);
        
        
        
    }
    public void Stage2()
    {
        SceneManager.LoadScene(8);
        
    }
}
