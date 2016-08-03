using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    public Button exit;
    public Button back;
    // Use this for initialization
   
    public bool[] stage= new bool[9]  ;
    public bool stage2;
    public bool stage3;
    public bool stage4;
    public bool stage5;
    public bool stage6;
    public bool backBool;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
   
    public void Stage1()
    {
        SceneManager.LoadScene(3);
        stage[0] = true;
        
    }
    public void Stage2()
    {
        SceneManager.LoadScene(8);
        stage[1] = true;
    }
}
