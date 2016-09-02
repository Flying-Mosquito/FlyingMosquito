using UnityEngine;
using System.Collections;

public class Capsule : MonoBehaviour {

    public Transform open;
    public bool one=false;


    void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag == "PLAYER")
        {

            if (one == false)
            {
                open.gameObject.SetActive(true);
                one = true;
            }
        }
    }
}
