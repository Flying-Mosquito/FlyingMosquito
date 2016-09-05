using UnityEngine;
using System.Collections;
using System;



public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public float moveSpeed;
    public float rotationSpeed;

    public float angrygauge = 0;

    Randommove movetransform;
    private Rigidbody rigidBody;



    public Transform myTransform;
    public PlayerCtrl _Player;

    public Enemy character;

    public enum State
    {
        PATROL, CHASE, ATTACK, FOOT, LAID
    }

    public State state;
    private bool alive;

    PlayerCtrl playerctrl;

    //partol
    public GameObject waypoint1;
    public GameObject waypoint2;
    private int moveNumbe;
    public float patrolSpeed = 0.5f;

    //chase
    // Use this for initialization
    void Awake()
    {
        myTransform = transform;
        rigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        playerctrl = GameObject.FindObjectOfType<PlayerCtrl>(); //GameObject.Find("Player").GetComponent<PlayerCtrl>();

      
            state = EnemyAI.State.PATROL;
       
        alive = true;
        StartCoroutine("FSM");
    }
    void Update()

    {
        if (Application.loadedLevelName == "Stage9" || Application.loadedLevelName == "Stage10")
        {
            state = EnemyAI.State.LAID;
            if ((playerctrl.state == Constants.ST_BLOOD) && Vector3.Distance(new Vector3(this.transform.position.x, 0, 0), new Vector3(playerctrl.transform.position.x, 0, 0)) < 3)
            {

                playerctrl.iBlood += 0.3f;
                angrygauge += 0.1f;

            }
            if (angrygauge < 10)
                return;
        }


        if ((playerctrl.state == Constants.ST_BLOOD) && Vector3.Distance(new Vector3(this.transform.position.x, 0, 0), new Vector3(playerctrl.transform.position.x, 0, 0)) < 3)
        {

            playerctrl.iBlood += 0.1f;
            angrygauge += 0.05f;

        }
        if (angrygauge > 10 && angrygauge < 90)
        {

           // angrygauge -= 4 * Time.deltaTime;

            if (Vector3.Distance(new Vector3(this.transform.position.x, 0, 0), new Vector3(playerctrl.transform.position.x, 0, 0)) < 5)
            {
                state = EnemyAI.State.ATTACK;
            }
            else if (Vector3.Distance(this.transform.position, playerctrl.transform.position) > 5)
            {
                state = EnemyAI.State.CHASE;
            }



        }
        //patrol

        else if (angrygauge < 10)
        {
            state = EnemyAI.State.PATROL;
        }
        if (angrygauge > 90)
        {
            state = EnemyAI.State.LAID;
        }
        //Foot
        if (angrygauge < 70 && angrygauge > 10 && (Vector3.Distance(new Vector3(this.transform.position.x, 0, 0), new Vector3(target.transform.position.x, 0, 0)) < 5) && playerctrl.transform.position.y < 5)
        {
            state = EnemyAI.State.FOOT;
        }

    }




    //   myTransform.rotation = Quaternion.Euler(0, target.position.z - myTransform.position.z * rotationSpeed * Time.deltaTime, 0);
    // myTransform.rotation = Quaternion.LookRotation(new Vector3(0, target.position.y,0) - myTransform.position);

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "WALL")
        {
            print("부딛힘");
            myTransform.gameObject.transform.Rotate(40,90, 40);

        }
    }


    IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {
                case State.PATROL:

                    Patrol();
                    break;

                case State.CHASE:
                    Chase();
                    break;
                case State.ATTACK:
                    Attact();
                    break;

                case State.FOOT:
                    Foot();
                    break;
                case State.LAID:
                    Laid();
                    break;
            }

            yield return null;
        }
    }

    void Laid()
    {

    }
    void Foot()
    {

    }

    void Patrol()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Move");

        target = go.transform;
        //if (Vector3.Distance(this.transform.position,Randommove[moveNumber].transform.position)>=2)
        //        {


        //        }

        if(Application.loadedLevelName =="Stage7")
        {
            if (Vector3.Distance(this.transform.position, waypoint1.transform.position) <= 2)
            {
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(waypoint2.transform.position - myTransform.position), rotationSpeed * Time.deltaTime);

            }
            else
            {
                myTransform.position += new Vector3(myTransform.forward.x * moveSpeed * Time.deltaTime, 0, myTransform.forward.z * moveSpeed * Time.deltaTime);
            }

        }
        else
        {

            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
            myTransform.position += new Vector3(myTransform.forward.x * moveSpeed * Time.deltaTime, 0, myTransform.forward.z * moveSpeed * Time.deltaTime);

        }
    }



    void Chase()
    {
        GameObject go = GameObject.FindGameObjectWithTag("PLAYER");

        target = go.transform;
        WaitForSeconds.Equals(1, 5);

        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
        myTransform.rotation = Quaternion.Euler(new Vector3(0f, myTransform.rotation.eulerAngles.y, 0f));
        myTransform.position += new Vector3(myTransform.forward.x * moveSpeed * Time.deltaTime, 0, myTransform.forward.z * moveSpeed * Time.deltaTime);

    }

    void Attact()
    {

    }


    // Update is called once per frame

}
