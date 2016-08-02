using UnityEngine;
using System.Collections;

public class Spray : MonoBehaviour {

    public Transform OnSpray;
    public EnemyAI EnemyAI;
  
    // Use this for initialization
  

    // Update is called once per frame
    void Update () {
	if(EnemyAI.angrygauge>70 && EnemyAI.angrygauge<90)
        {
            OnSpray.gameObject.SetActive(true);
            EnemyAI.state = EnemyAI.State.CHASE;
        }
    else
        {
            OnSpray.gameObject.SetActive(false);
        }

    }
}
