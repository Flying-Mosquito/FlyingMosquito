using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{

    public Transform timer;
    PlayerCtrl playerctrl;
    public GameObject player;
    // Use this for initialization
    void Awake()
    {
       
        timer.gameObject.SetActive(false);
        playerctrl = GameObject.Find("Player").GetComponent("PlayerCtrl") as PlayerCtrl;
    }

    // Update is called once per frame
    void Update()
    {
   
       
    }
}
