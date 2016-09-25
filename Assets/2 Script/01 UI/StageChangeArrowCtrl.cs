using UnityEngine;
using System.Collections;

public class StageChangeArrowCtrl : MonoBehaviour {

	// Use this for initialization


    public PlayerCtrl player = null;
    public float fSpeed;
    private Vector3 vDestPos;
    public bool bMove { get; private set; }
    public bool bClickRight { get; private set; }
    void Start ()
    {
        vDestPos = Vector3.zero;
        player = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        fSpeed = 2f;

        bMove = false;
        bClickRight = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Space))     // 키입력을 확인 후 , 목표지점 설정 
        {
            if (!bMove)
            {
                bMove = true;
                vDestPos = player.transform.position + (player.transform.right * fSpeed * 10f);
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))    // 키입력을 확인 후 , 목표지점 설정 
        {
            if (!bMove)
            {
                bMove = true;
                vDestPos = player.transform.position + (-player.transform.right * fSpeed * 10f);
            }
        }

        if (bMove)  // 키입력을 받은 후, 플레이어를 이동시킴 
        {   
            if (player.state == Constants.ST_IDLE)
            {
                //player.MoveHorizontal(vDestPos);

                if (player.transform.position == vDestPos)
                    bMove = false;
                
            }

        }

    }
}
