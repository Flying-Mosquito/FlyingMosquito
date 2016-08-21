using UnityEngine;
using System.Collections;

public class ChameleonCtrl : MonoBehaviour {

    public enum eState { IDLE, EAT }
    public eState state;
    public Animator Anim;

    public int iFoodNum; // 만족시켜야 하는 먹이 개수 
    public int iFoodCount; // 먹은 먹이 개수 
    

	// Use this for initialization
	void Start ()
    {
        Anim = GetComponent<Animator>();
        state = eState.IDLE;
        iFoodCount = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
       // Anim.GetCurrentAnimatorStateInfo
	    if(Anim.GetAnimatorTransitionInfo(0).IsName("Base Layer.eat -> Base Layer.idle"))
        {
            state = eState.IDLE;
            Anim.SetInteger("iState", 0);
            print("EAT");
        }
	}

    void OnCollisionEnter(Collision _coll)
    {
        if(_coll.gameObject.CompareTag("CHAMELFOOD"))
        {
            ++iFoodCount;
            Anim.SetInteger("iState", 1);
            state = eState.EAT;

            Destroy(_coll.gameObject);
         //   Anim.Play("eat");
        }
    }

}
