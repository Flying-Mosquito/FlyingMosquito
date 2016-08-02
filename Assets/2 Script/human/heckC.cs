using UnityEngine;
using System.Collections;

public class heckC : MonoBehaviour
{
    public EnemyAI EnemyAI;

    void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag == "PLAYER" && (EnemyAI.State.ATTACK == EnemyAI.state))
        {
            PlayerCtrl.Instance.Damaged(30);
        }


        else if (coll.gameObject.tag == "PLAYER" && (EnemyAI.State.FOOT == EnemyAI.state))
        {
            PlayerCtrl.Instance.Damaged(30);
        }
    }
}