using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiControl : MonoBehaviour
{
    public Transform[] PointList;
    public GameObject PlayerPointPrefeb;
    public GameObject OtherPlayerPointPrefeb;

    // Use this for initialization
    void Awake()
    {
        PointList = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();

        print("Awake - : " + Constants.SERVCONNECT_NUM);
        for( int i = 1; i < PointList.Length; ++i)
        {
            if (Constants.SERVCONNECT_NUM + 1 == i)   // SERVCONNECT_NUM : 0~3
            {
                GameObject _playerPoint = Instantiate(PlayerPointPrefeb, PointList[i].position, PointList[i].rotation) as GameObject;//PointList[i].AddComponent<PlayerPoint>();
                PlayerPoint playerPoint = _playerPoint.GetComponent<PlayerPoint>();
                playerPoint.SetPlayerNum(Constants.SERVCONNECT_NUM);
            }
            else
            {
                GameObject _otherPlayerPoint = Instantiate(OtherPlayerPointPrefeb, PointList[i].position, PointList[i].rotation) as GameObject;//PointList[i].AddComponent<OtherPlayerPoint>();
                OtherPlayerPoint otherPlayerPoint = _otherPlayerPoint.GetComponent<OtherPlayerPoint>();
                otherPlayerPoint.SetPlayerNum(i-1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
