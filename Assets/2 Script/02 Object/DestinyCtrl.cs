using UnityEngine;
using System.Collections;

public class DestinyCtrl : MonoBehaviour
{
    private bool isCollPlayer;
    public StageUICtrl stageUICtrl;
    void Start()
    {
        isCollPlayer = false;

        stageUICtrl = GameObject.Find("MyUI").GetComponent<StageUICtrl>();
    }
    void OnCollisionEnter(Collision _coll)
    {
        
        if(_coll.collider.CompareTag("PLAYER"))
        {
            if (isCollPlayer == false)
            {
                print("플레이어와 충돌!!!!");
                isCollPlayer = true;
                stageUICtrl.IncreaseDestinyCount();
            }
        }
    }

}

