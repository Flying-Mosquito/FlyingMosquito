using UnityEngine;
using System.Collections.Generic;
using System.Collections;


// 이게 서버붙여도 될라나..
// 클라에서 붙은지 안붙은지 확인하고, 그걸 서버로 전송?
public class MovableObjCtrl : MonoBehaviour {

    public Transform tr;
    public Transform equipPoint;
    private bool isPlayerCollision;
    private PlayerCtrl player;

   void Start()
    {

        equipPoint = GameObject.Find("EquipPoint").transform;
        tr = GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<PlayerCtrl>();
    }

   void Update()
    {
        // 이순간 Trigger로 바뀜
        if (isPlayerCollision && ((player.variable & Constants.BV_bCling) > 0 )) // 수정
        {
            tr.SetParent(equipPoint);
            player.Equip(gameObject);
            isPlayerCollision = false;  
        }
    }

    void OnCollisionEnter(Collision _coll)
    {
        if (_coll.gameObject.CompareTag("PLAYER"))
            isPlayerCollision = true;
        
    }

    void OnCollisionExit(Collision _coll)
    {
        if (_coll.gameObject.CompareTag("PLAYER"))
            isPlayerCollision = false;
    }

    
}
