using UnityEngine;
using System.Collections;

public class PlayerPoint : MonoBehaviour
{
    public enum ePointState {  eMenu, eStage}
    public ePointState state;
    public GameObject PlayerPrefeb;
    private PlayerCtrl player = null;


    public bool isCamStateCollider;

    //public int iPlayerNum = -1; // 플레이어로 넘겨줄 iPlayerNum변수 

    void Awake()
    {
     //   CameraCtrl.Instance.GetComponent<CameraEffect>().SetParentCamp();
        //   PlayerCtrl.Instance.SetStateIdle(true);

        CreatePlayer();
        
        //PlayerCtrl.Instance.SetTransform(transform.position, transform.rotation);
    }

    public void SetPlayerNum(int _iPlayerNum)
    {
       // print("_PlayerPoint -  :" + _iPlayerNum);
       // iPlayerNum = _iPlayerNum;
        player.SetPlayerNum(_iPlayerNum);
    }
   
    void CreatePlayer()
    {
        // 플레이어 만들면서 카메라도 셋팅 
        GameObject _player = Instantiate(PlayerPrefeb, transform.position, transform.rotation) as GameObject;
        CameraCtrl.Instance.SetPlayer(_player);

        player = _player.GetComponent<PlayerCtrl>();
        player.SetHP(100);
        player.fBlood = 0;


        //TimeManager.Instance.StartCoroutine("SetTimeStop", false);    // 이건 나중에 퀘스트보드에서 해줘야함
        player.SetIsInStage(true);


        //버튼에 플레이어 넣어줌 
        FlyBtCtrl flybt = GameObject.FindObjectOfType<FlyBtCtrl>();
        ClingBtCtrl clingbt = GameObject.FindObjectOfType<ClingBtCtrl>();
        clingbt.SetPlayer(player);
        flybt.SetPlayer(player);

    }

    void Start ()
    {
        if (isCamStateCollider)
            CameraCtrl.Instance.SetStateToCollider(true);
        else
            CameraCtrl.Instance.SetStateToCollider(false);


//        TimeManager.Instance.StartCoroutine("SetOption",true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (state == ePointState.eStage)
            {
                player.SetIsInStage(true);
                player.SetHP(100);
                //player.CheckServerConnect(false, Constants.SERVCONNECT_NUM);
                TouchEventManager.Instance.SetPlayer(player);
                GameManager.Instance.SetBackgroundAlphaColor(0f);
                gameObject.SetActive(false);


            }
            //else // ???
            //{
            //    GameObject _obj = null;
            //    if(_obj = CollisionManager.Instance.Get_MouseCollisionObj(3000f, "RAINDROP"))
            //    {
            //        //   PlayerCtrl.Instance.SetStateIdle(false);
                  
            //        player.SetIsInStage(true);
            //        gameObject.SetActive(false);
            //        TouchEventManager.Instance.SetPlayer(player);
            //        //UI.Instance.SetPlayer(player);
            //        FlyBtCtrl flybt = GameObject.FindObjectOfType<FlyBtCtrl>();
            //        ClingBtCtrl clingbt = GameObject.FindObjectOfType<ClingBtCtrl>();
            //        clingbt.SetPlayer(null);
            //        flybt.SetPlayer(null);
            //        //Timer.Instance.enabled = false;

            //    }
            //}
            
        }

    }

}
