using UnityEngine;
using System.Collections;

public class Spray : MonoBehaviour {

    public Transform OnSpray;
     public EnemyAI enemyai;
    public GameObject human;
    // Use this for initialization
    void Start()
    {
        enemyai = GameObject.FindObjectOfType<EnemyAI>();
    }

    // Update is called once per frame
    void Update () {
        if(enemyai.angrygauge>70 && enemyai.angrygauge<90)
        { 
            OnSpray.gameObject.SetActive(true);
            enemyai.state = EnemyAI.State.CHASE;
        }
  

    }
}
