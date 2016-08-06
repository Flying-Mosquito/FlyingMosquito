using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    public Button exit;
    public Button back;
    // Use this for initialization
   
   
   
    public bool backBool;
    void Start()

    {
        
     
     

    }

    // Update is called once per frame
   
   
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
