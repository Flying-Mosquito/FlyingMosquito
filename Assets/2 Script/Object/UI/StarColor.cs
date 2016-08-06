using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class StarColor : MonoBehaviour {
    public Transform[] star= new Transform[9];
    public Image[] image  = new Image[9];
    public Image[] image2 = new Image[9];
    public Image[] image3 = new Image[9];
    public SelectStage select;
   
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
     
        if (PlayerCtrl.Instance.iBlood > 190)
        {
           image[0].color = Color.green;
            image2[0].color = Color.green;
            image3[0].color = Color.green;
        }
    }
}
