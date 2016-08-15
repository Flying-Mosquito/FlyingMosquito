using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClingBtnCtrl : BaseButton//MonoBehaviour
{
  //  public Text Text;
   // private Transform tr;
   // private bool isMouseDown = false;
    private PlayerCtrl player = null;
    // Use this for initialization
    void Start ()
    {
        //tr = GetComponent<Transform>();
      //  player = GameObject.FindObjectOfType<PlayerCtrl>(); //GameObject.Find("Player").GetComponent<PlayerCtrl>();
    }



	

    public override void OnTouchBegin(Vector2 _pos)
    {
        isMouseDown = true;
        //isOnTouch = true;
        if(player!=null)
            player.ClingBtDown(); // left가 되었다
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

        if(player!=null)
            player.ClingBtUp();
        //Text.text = "OnTouchEnd";
    }

    public void SetPlayer(PlayerCtrl _player)
    {
        player = _player;
    }
}




