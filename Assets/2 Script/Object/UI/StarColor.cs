using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class StarColor : MonoBehaviour {
    public Transform star;
    public Image image;
    public Image image2;
    public Image image3;
    public SelectStage select;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerCtrl.Instance.iBlood > 190)
        {
           image.color = Color.green;
            image2.color = Color.green;
            image3.color = Color.green;
        }
    }
}
