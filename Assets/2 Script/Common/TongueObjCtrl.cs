using UnityEngine;
using System.Collections;

public class TongueObjCtrl : MonoBehaviour {

    void OnCollisionEnter(Collision _coll)
    {
        if (_coll.gameObject)
        {
            print("frog- " + _coll.gameObject.tag);
        }

        if (_coll.gameObject.CompareTag("PLAYER"))
        {
            print("PLAYER충ㅗㄹ");

            PlayerCtrl _player = _coll.gameObject.GetComponent<PlayerCtrl>();
            if ((_player.variable & Constants.BV_Stick) > 0)
                return;

            Rigidbody _rBody = _coll.gameObject.GetComponent<Rigidbody>();
            _player.variable |= Constants.BV_Stick;
            _player.transform.SetParent(transform);
            _player.SetHP(0);
            _rBody.isKinematic = true;


            //variable |= Constants.BV_Stick;//isStick = true;
            //SetParent(coll.transform);
            //rigidBody.isKinematic = true;   // 물리적인 영향을 끔 
            //   iHP = 0;
        }
    }
}
