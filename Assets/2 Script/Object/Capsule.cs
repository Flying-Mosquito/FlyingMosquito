using UnityEngine;
using System.Collections;

public class Capsule : MonoBehaviour {

    public Transform open;

    void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag == "PLAYER")
        {
          

            open.gameObject.SetActive(true);

        }
    }
}
