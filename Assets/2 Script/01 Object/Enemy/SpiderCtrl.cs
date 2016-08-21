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
        MOVE();
        
      
        Animate();
    }
    void MOVE()
    {
        if (curWaypoint < Waypoints.Length)
        {
            Target = Waypoints[curWaypoint].position;
            Movedirection = Target - transform.position;
            Velocity = rigidbody.velocity;
            if (Movedirection.magnitude < 1)
            {
                curWaypoint++;

            }
            else
            {
                Velocity = Movedirection.normalized * moveSpeed;
            }

        }
        else
        {
            if (doPatrol)
            {
                curWaypoint = 0;
            }
            else
            {
                Velocity = Vector3.zero;
            }
        }
        rigidbody.velocity = Velocity;
        transform.LookAt(Target);
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