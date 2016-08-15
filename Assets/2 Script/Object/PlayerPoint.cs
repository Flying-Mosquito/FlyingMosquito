using UnityEngine;
using System.Collections;

public class PlayerPoint : MonoBehaviour
{
    public enum ePointState {  eMenu, eStage}
    public ePointState state;
    public GameObject PlayerPrefeb;
    private PlayerCtrl player = null;


    public bool isCamStateCollider;

    void Awake()
    {
        CameraCtrl.Instance.GetComponent<CameraEffect>().SetParentCamp();
        //   PlayerCtrl.Instance.SetStateIdle(true);

        CreatePlayer();
        
        //PlayerCtrl.Instance.SetTransform(transform.position, transform.rotation);
    }
   
    void CreatePlayer()
    {
        // 플레이어 만들면서 카메라도 셋팅 
        GameObject _player = Instantiate(PlayerPrefeb, transform.position, transform.rotation) as GameObject;
        CameraCtrl.Instance.SetPlayer(_player);

        player = _player.GetComponent<PlayerCtrl>();
    }

    void Start ()
    {
        if (isCamStateCollider)
            CameraCtrl.Instance.SetStateToCollider(true);
        else
            CameraCtrl.Instance.SetStateToCollider(false);
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
                TouchEventManager.Instance.SetPlayer(player);
                gameObject.SetActive(false);
             
            }
            else
            {
                GameObject _obj = null;
                if(_obj = CollisionManager.Instance.Get_MouseCollisionObj(3000f, "RAINDROP"))
                {
                    //   PlayerCtrl.Instance.SetStateIdle(false);
                    player.SetIsInStage(true);
                    gameObject.SetActive(false);
                    //UI.Instance.SetPlayer(player);
                    //FlyBtCtrl flybt = GameObject.FindObjectOfType<FlyBtCtrl>();
                    //ClingBtnCtrl clingbt = GameObject.FindObjectOfType<ClingBtnCtrl>();

                    //Timer.Instance.enabled = false;
                    //clingbt.SetPlayer(null);
                    //flybt.SetPlayer(null);
                }
            }
            
        }



    }

}
