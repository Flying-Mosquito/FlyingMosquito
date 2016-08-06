using UnityEngine;
using System.Collections;

public class SpiderCtrl : MonoBehaviour {
    public Animator Anim;
    public Transform tr;

    public enum eState { IDLE, MOVE, ATTACK }
    public eState state;

    private float fDist;
	// Use this for initialization
	void Start ()
    {
        tr = GetComponent<Transform>();
        Anim = GetComponent<Animator>();

        state = eState.IDLE;
        fDist = 8f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(tr.position, PlayerCtrl.Instance.transform.position) < fDist)
            state = eState.ATTACK;
        else
            state = eState.IDLE;

        Animate();
	}

    void Animate()
    {
        switch(state)
        {
            case eState.IDLE:
                {
                    Anim.SetInteger("iState",0);
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
