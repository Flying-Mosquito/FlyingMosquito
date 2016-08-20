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
    public Timer timer;
    // Use this for initialization
    void Start()
    {
        timer= GameObject.FindObjectOfType<Timer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (UI.Instance.stage[1] == 1)
        {
            for (int i = 1; i < 9; i++)
            {
                if (timer.score[i] > 80 && UI.Instance.stage[i] == 1)
                {
                    image[i - 1].color = Color.green;
                    image2[i - 1].color = Color.green;
                    image3[i - 1].color = Color.green;
                }
                else if (timer.score[i] > 40 && timer.score[i] < 80 && UI.Instance.stage[i] == 1)
                {
                    image[i - 1].color = Color.green;
                    image2[i - 1].color = Color.green;
                }
                else if (timer.score[i] > 5 && timer.score[i] < 40 && UI.Instance.stage[i] == 1)
                {
                    image[i - 1].color = Color.green;
                }
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
