using UnityEngine;
using System.Collections;


public class FrogCtrl : MonoBehaviour
{
    Animator anim;

    private Transform tr;
    //private Transform _TongueTr;
    private PlayerCtrl player;
    //private GameObject _Tongue;
    private FrogTongue _Tongue;

    private RaycastHit hit;
    private float fLength;      // 혀길이
    private bool isInSight;     // 원뿔형 시야에 들어왔는지 확인
    private Vector3 vTongueDir;


    // Use this for initialization
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCtrl>(); //PlayerCtrl.Instance;// 
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
    void Update()
    {    //excution Order를 변경했기 때문에 Player 이후에 호출됨
         // isInSight = Check_Sight();
        if ((player.variable & Constants.BV_Stick) > 0)
            return;

        isInSight = CollisionManager.Instance.Check_Sight(tr, player.transform.position, fLength, 80f);
        if (isInSight)
        {
            // Time.timeScale = 0f;
            int iPlayerMove;

            if (player.state != Constants.ST_FLYING)
            {
                iPlayerMove = 1;
                vTongueDir = (player.transform.position /*+ _Player.transform.forward*/) - _Tongue.transform.position;    // 방향벡터 구하기
             //   print("가야할방향 : " + vTongueDir);
            }
            else
            {
                iPlayerMove = 2;
                vTongueDir = (player.transform.position + player.transform.forward * 1.5f/*(_Player.transform.position - _Player.prePosition)*/) - (_Tongue.transform.position);    // 방향벡터 구하기
               // print("_Player.transform.position -_Player.prePosition  ..  : " + (_Player.transform.position - _Player.prePosition));

            }

            vTongueDir.Normalize();               // 정규화

            Vector3 vTemp = (player.transform.position - tr.position);
            vTemp.Normalize();



            _Tongue.SetMoveState(iPlayerMove);
            _Tongue.SetDir(vTongueDir);
        }

    }

    void LateUpdate()
    {

    }


    /*
    private bool Check_Sight()
    {
        // Debug.Log("_Player.position : " + _Player.transform.position.x.ToString() + ", " + _Player.transform.position.y.ToString() + ", " + _Player.transform.position.z.ToString());
        // Debug.Log("tr.position : " + tr.position.x.ToString() + ", " + tr.position.y.ToString() + ", " + tr.position.z.ToString());

        Vector3 vDir = _Player.transform.position - tr.position;   // 플레이어 
                                                                   //  print("vDir Length = " + Vector3.Distance(_Player.transform.position, tr.position).ToString());
        vDir.Normalize();


        //     if (Physics.Raycast(_Obj.transform.position, _Obj.transform.forward, out hit, _fDist))
        //     if( hit.collider.tag == _Tag)

        //Debug.Log("Angle : " + Vector3.Angle(tr.forward , vDir).ToString());

        Debug.DrawRay(tr.position, vDir * fLength, Color.blue);
        if ((Vector3.Angle(tr.forward, vDir) < 40) && Physics.Raycast(tr.position, vDir, out hit, fLength))   // 범위안에 들어와 있으면서, 각도가 40보다 작다
        {

            //Debug.Log("들어옴");
            //  if (false == bCheck)
            //  {
            //   bCheck = true;
            if (hit.collider.tag == "PLAYER")
            {

                vTongueDir = (_Player.transform.position + _Player.transform.forward) //  * _Player.fOwnSpeed * Time.deltaTime
                            - _Tongue.transform.position;    // 방향벡터 구하기 - 플레이어의 다음 위치의 방향에 
                //
                vTongueDir.Normalize();               // 정규화
                                                      //  _Tongue.transform.rotation = Quaternion.Euler(vDir);

                //}
                return true;
            }

        }

        //  bCheck = false;
        // Debug.Log("안들어옴");
        return false;

    }
*/
}
