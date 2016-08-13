using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class StarColor : MonoBehaviour
{
    public Transform[] star = new Transform[9];
    public Image[] image = new Image[9];
    public Image[] image2 = new Image[9];
    public Image[] image3 = new Image[9];
    public SelectStage select;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < 9; i++)
        {
            if (Timer.Instance.score[i] > 80 && PlayerCtrl.Instance.stage[i] == 1)
            {
                image[i - 1].color = Color.green;
                image2[i - 1].color = Color.green;
                image3[i - 1].color = Color.green;
            }
            else if (Timer.Instance.score[i] > 40 && Timer.Instance.score[i] < 80 && PlayerCtrl.Instance.stage[i] == 1)
            {
                image[i - 1].color = Color.green;
                image2[i - 1].color = Color.green;
            }
            else if (Timer.Instance.score[i] > 5 && Timer.Instance.score[i] < 40 && PlayerCtrl.Instance.stage[i] == 1)
            {
                image[i - 1].color = Color.green;
            }
        }
        //if (PlayerCtrl.Instance.iBlood > 190)
        //{
        //   image[0].color = Color.green;
        //    image2[0].color = Color.green;
        //    image3[0].color = Color.green;
        //}
    }
}
