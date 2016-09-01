using UnityEngine;
using System.Collections;

public class Randommove : MonoBehaviour
{
    public Transform move2;
   
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
       
            float sizeX = Random.Range(-15f, 38f);
            float sizeY = 0;
            float sizeZ = Random.Range(-7f, 30.5f);
            move2.transform.position = new Vector3(sizeX, sizeY, sizeZ);
       
       


    }
}
