using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class StarColor : MonoBehaviour
{
    
    public Text myGold;
    public Text Stamina;
    public Transform[] star = new Transform[12];
    public Image[] image = new Image[12];
    public Image[] image2 = new Image[12];
    public Image[] image3 = new Image[12];
    public SelectStage select;
    public Timer timer;
    // Use this for initialization
    void Start()
    {
        timer= GameObject.FindObjectOfType<Timer>();
        UI.Instance.Tscore = 0;
        for (int i = 1; i < 12; i++)
        {
            UI.Instance.Tscore += UI.Instance.score[i];
        }
        UI.Instance.Tscore -= UI.Instance.Gold;
    }

        // Update is called once per frame
        void Update()
    {
        myGold.text = UI.Instance.Tscore.ToString();

        if (UI.Instance.stage[1] == 1)
        {
            for (int i = 1; i < 12; i++)
            {
                if (UI.Instance.score[i] > 80 && UI.Instance.stage[i] == 1)
                {
                    image[i - 1].color = Color.cyan;
                    image2[i - 1].color = Color.cyan;
                    image3[i - 1].color = Color.cyan;
                }
                else if (UI.Instance.score[i] > 40 && UI.Instance.score[i] < 80 && UI.Instance.stage[i] == 1)
                {
                    image[i - 1].color = Color.cyan;
                    image2[i - 1].color = Color.cyan;
                }
                else if (UI.Instance.score[i] > 5 && UI.Instance.score[i] < 40 && UI.Instance.stage[i] == 1)
                {
                    image[i - 1].color = Color.cyan;
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
