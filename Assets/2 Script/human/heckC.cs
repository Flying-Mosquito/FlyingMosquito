using UnityEngine;
using System.Collections;

public class heckC : MonoBehaviour
{
    public EnemyAI enemyai;
    void Start()
    {
        enemyai = gameObject.GetComponent("EnemyAI") as EnemyAI;
    }
    void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag == "PLAYER" && (EnemyAI.State.ATTACK == enemyai.state))
        {
            PlayerCtrl.Instance.Damaged(30);
        }


        else if (coll.gameObject.tag == "PLAYER" && (EnemyAI.State.FOOT == enemyai.state))
        {
            PlayerCtrl.Instance.Damaged(30);
        }
    }
}