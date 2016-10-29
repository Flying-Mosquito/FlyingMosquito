using UnityEngine;
using System.Collections;

public class OtherPlayerPoint : MonoBehaviour {

    public enum ePointState { eMenu, eStage }
    public ePointState state;
    public GameObject PlayerPrefeb;

    public bool isCamStateCollider;

    void Awake()
    {
        //   CameraCtrl.Instance.GetComponent<CameraEffect>().SetParentCamp();
        //   PlayerCtrl.Instance.SetStateIdle(true);

        CreatePlayer();

        //PlayerCtrl.Instance.SetTransform(transform.position, transform.rotation);
    }

    void CreatePlayer()
    {
        // 플레이어 만들면서 카메라도 셋팅 
        GameObject _player = Instantiate(PlayerPrefeb, transform.position, transform.rotation) as GameObject;
     //   CameraCtrl.Instance.SetPlayer(_player);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (state == ePointState.eStage)
            {
                GameManager.Instance.SetBackgroundAlphaColor(0f);
                gameObject.SetActive(false);


            }
        

        }



    }
}
