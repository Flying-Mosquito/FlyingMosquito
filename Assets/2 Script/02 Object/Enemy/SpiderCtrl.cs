using UnityEngine;
using System.Collections;

public class SpiderCtrl : MonoBehaviour
{
    public Transform target;
    public Animator Anim;
    public Transform tr;
    private PlayerCtrl player = null;
    public float moveSpeed;
    public float rotationSpeed;
    public enum eState { IDLE, MOVE, ATTACK }
    public eState state;
    public Transform[] Waypoints;
    public int curWaypoint;
    public bool doPatrol = true;
    public Vector3 Target;
    public Vector3 Movedirection;
    public Vector3 Velocity;
    private Rigidbody rigidbody;
    private float fDist;
    public Transform sline1;
    public Transform sline2;
    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
        Anim = GetComponent<Animator>();
        player = GameObject.FindObjectOfType<PlayerCtrl>(); //GameObject.Find("Player").GetComponent<PlayerCtrl>();
        rigidbody = GetComponent<Rigidbody>();
        state = eState.IDLE;
        fDist = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if(sline1.gameObject.activeInHierarchy==false && sline2.gameObject.activeInHierarchy == false)
        {
            player.iBlood = 200;
        }
            //if (Vector3.Distance(new Vector3(this.transform.position.x, 0, 0), new Vector3(player.transform.position.x, 0, 0)) < 5)
            //{
            //    state = eState.ATTACK;

            //}
            //else
            MOVE();
        
        Animate();
    }
    void MOVE()
    {
       
        state = eState.MOVE;

        if (curWaypoint < Waypoints.Length)
        {
            Target = Waypoints[curWaypoint].position;
            Movedirection = Target - transform.position;
          
          if (Movedirection.magnitude < 1)
            {
               
                
                    curWaypoint++;

            }
            else
            {
                if (Application.loadedLevelName == "Stage11")
                {
                    if ((player.state == Constants.ST_BLOOD)|| (player.state == Constants.ST_CLING))
                    {
                        curWaypoint = 1;

                    }
                    else
                        curWaypoint = 0;
                }
                tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.LookRotation(Waypoints[curWaypoint].position -tr.position), rotationSpeed * Time.deltaTime);
                tr.position += new Vector3(tr.forward.x * moveSpeed * Time.deltaTime, tr.forward.y * moveSpeed * Time.deltaTime, tr.forward.z * moveSpeed * Time.deltaTime);

            }

        }
        else
        {
            curWaypoint = 0;
        }
       
    }

   
    void Animate()
    {
        switch (state)
        {
            case eState.IDLE:
                {
                    Anim.SetInteger("iState", 0);
                    break;
                }
            case eState.MOVE:
                {
                    Anim.SetInteger("iState", 1);
                    break;
                }
            case eState.ATTACK:
                {
                    Anim.SetInteger("iState", 2);
                    break;
                }

        }
    }
}