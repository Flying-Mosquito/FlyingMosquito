using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickB : BaseButton//MonoBehaviour
{
    //  public Text Text;
    // private Transform tr;
    // private bool isMouseDown = false;
    private PlayerCtrl player = null;
    Option option;
    // Use this for initialization
    void Start()
    {
        //tr = GetComponent<Transform>();
        //  player = GameObject.FindObjectOfType<PlayerCtrl>(); //GameObject.Find("Player").GetComponent<PlayerCtrl>();



    }





    public override void OnTouchBegin(Vector2 _pos)
    {
        isMouseDown = true;
        //isOnTouch = true;
        if (player != null)
        {
            player.iBlood = 0;
            player.SetParentNull();
            player.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
         

            SceneManager.LoadScene(13);
        }
            // left가 되었다
            // Text.text = "OnTouchBegin";
        }
    /*
    public override void OnTouchStay()
    {
        player.Rightbuttondown(); // left가 되었다
        Text.text = "OnTouchStay";
    }
    */
    /*
    public override void OnTouchMove(Vector2 _pos)
    {
        if(isMouseDown)
            player.ClingBtDown();
        //Text.text = "OnTouchMove";
    }
    */
    public override void OnTouchEnd(Vector2 _pos)
    {
        isMouseDown = false;

        if (player != null)
            if (player != null)
            {
                player.iBlood = 0;
                player.SetParentNull();
                player.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                

                SceneManager.LoadScene(13);
            }
        //Text.text = "OnTouchEnd";
    }

    public void SetPlayer(PlayerCtrl _player)
    {
        player = _player;
    }
}




