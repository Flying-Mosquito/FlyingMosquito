using UnityEngine;
using System.Collections;



public class Enemy : MonoBehaviour
{

    public int moveSpeed;
    public int rotationSpeed;


    // Use this for initialization
    void Update()
        {

        if ((PlayerCtrl.Instance.state == Constants.ST_BLOOD))
        {

            PlayerCtrl.Instance.iBlood += 1;
           

        }
    }
    
      
    }
