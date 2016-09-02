using UnityEngine;
using System.Collections;


public class FrogCtrl : TimeAffectedObj
{
    Animator anim;

    private Transform tr;
    //private Transform _TongueTr;
    public PlayerCtrl player;
    //private GameObject _Tongue;
    private FrogTongue _Tongue;

    private RaycastHit hit;
    private float fLength;      // 혀길이
    private bool isInSight;     // 원뿔형 시야에 들어왔는지 확인
    private Vector3 vTongueDir;


    // Use this for initialization
    public override void Awake()
    {
        base.Awake();

        player = GameObject.FindObjectOfType<PlayerCtrl>();//GameObject.Find("Player").GetComponent<PlayerCtrl>(); //PlayerCtrl.Instance;// 
        tr = GetComponent<Transform>();

        //  _TongueTr = tr.transform.FindChild("Tongue");   // 자식으로 가진 Tongue의 Transform을 가져오기 위해 사용 
        _Tongue = tr.transform.FindChild("Tongue").GetComponent<FrogTongue>();

        //_Tongue = _TongueTr.gameObject;                 //
                                                        //_Tongue = GameObject.Find("Tongue");
                                                        // x = 0f;
        fLength = 6f;
        isInSight = false;
        vTongueDir = Vector3.zero;

        anim = GetComponent<Animator>();
        anim.Play("idle");

    }


    // Update is called once per frame
    public override void MyUpdate()
    {    //excution Order를 변경했기 때문에 Player 이후에 호출됨
         // isInSight = Check_Sight();
        if ((player.variable & Constants.BV_Stick) > 0)
            return;
        //yield return new WaitForFixedUpdate();
        isInSight = CollisionManager.Instance.Check_Sight(tr, player.transform.position, fLength, 80f);
        if (isInSight)
        {
            // Time.timeScale = 0f;
            int iPlayerMove;

            print("fspeed : " + player.fSpeed);
            if (player.fSpeed < 4.5f )//player.state != Constants.ST_FLYING)
            {
                iPlayerMove = 1;
                vTongueDir = (player.transform.position /*+ _Player.transform.forward*/) - _Tongue.transform.position;    // 방향벡터 구하기
             //   print("가야할방향 : " + vTongueDir);
            }
            else
            {
                iPlayerMove = 2;
             
                vTongueDir = (player.transform.position +player.transform.forward/*(_Player.transform.position - _Player.prePosition)*/) - (_Tongue.transform.position);    // 방향벡터 구하기
               // print("_Player.transform.position -_Player.prePosition  ..  : " + (_Player.transform.position - _Player.prePosition));

            }
             
            vTongueDir.Normalize();               // 정규화

            //Vector3 vTemp = (player.transform.position - tr.position);
            //vTemp.Normalize();
            _Tongue.SetMoveState(iPlayerMove);
            _Tongue.SetDir(vTongueDir);
            print("Player Position : " + player.transform.position);
        }

    }

}
