using UnityEngine;
using System.Collections;

public class heckC : MonoBehaviour
{
    EnemyAI enemyai;
    PlayerCtrl playerctrl;
    void Start()
    {
        playerctrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        enemyai = gameObject.GetComponent("EnemyAI") as EnemyAI;
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