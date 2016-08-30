using UnityEngine;
using System.Collections;

public class SleepHuman : MonoBehaviour {
    
    // Use this for initialization
    public EnemyAI enemy1;
    public EnemyAI enemy2;

    public bool Sleep1=false;
    public bool Sleep2=false;

    void Start()
    {
        
        enemy1 = GameObject.Find("Human").GetComponent<EnemyAI>();
        enemy2 = GameObject.Find("human-2").GetComponent<EnemyAI>();
    }
	// Update is called once per frame
	void Update () {
	
        if(enemy1.angrygauge>90)
        {
            Sleep1 = true;
        }
        if(enemy2.angrygauge > 90)
        {
            Sleep2 = true;
        }
	}
}
