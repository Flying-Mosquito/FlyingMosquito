using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClingBtCtrl : BaseButton//MonoBehaviour
{
  //  public Text Text;
   // private Transform tr;
   // private bool isMouseDown = false;
    private PlayerCtrl player = null;
    private Color idleAlpha;
    private Image image;
    public Sprite idleSp;
    public Sprite downSp;
    // Use this for initialization
    void Start ()
    {
        //tr = GetComponent<Transform>();
        //  player = GameObject.FindObjectOfType<PlayerCtrl>(); //GameObject.Find("Player").GetComponent<PlayerCtrl>();
        idleAlpha = new Color(1f, 1f, 1f, 0.3f);
        image = GetComponent<Image>();
    }



	

    public override void OnTouchBegin(Vector2 _pos)
    {
        isMouseDown = true;
        //isOnTouch = true;
        if(player!=null)
            player.ClingBtDown(); // left가 되었다

        image.sprite = downSp;
        image.color = Vector4.one;
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

        image.sprite = idleSp;
        image.color = idleAlpha;

        //Text.text = "OnTouchEnd";
    }

    public void SetPlayer(PlayerCtrl _player)
    {
        player = _player;
    }
}




