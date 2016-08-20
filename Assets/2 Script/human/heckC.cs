using UnityEngine;
using System.Collections;

public class heckC : MonoBehaviour
{
   
    PlayerCtrl playerctrl;
    void Start()
    {
       // playerctrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        playerctrl = GameObject.FindObjectOfType<PlayerCtrl>();
        
    }
    void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag == "PLAYER" )
        {
            playerctrl.Damaged(30);
        }


        else if (coll.gameObject.tag == "PLAYER")
        {
            playerctrl.Damaged(30);
        }
    }
}