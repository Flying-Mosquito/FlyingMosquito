﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FlyBtCtrl : BaseButton//, IPointerDownHandler, IDragHandler, IPointerUpHandler 
{
    private Transform tr;
   // public Transform StartTr;  // 처음 시작하는 위치 ( FlyButton위치 )           
    public Transform endTr;       // 부스트의 위치
    public Vector3 startPosition;
    private Vector3 endPosition;
    private Image image;

    public Sprite idleSp;
    public Sprite downSp;
    public Sprite boostSp;
    private Color idleAlpha;
    //   private bool isMouseDown = false;
    //   private bool isOnTouch = false;
    private float fStraightAngle;

    private PlayerCtrl player = null;
   // private bool isBoost = false;
    // public static 
    void Start()
    {
        tr = GetComponent<Transform>();
        idleAlpha = new Color(1f, 1f, 1f, 0.3f);
        image = GetComponent<Image>();
        // player = GameObject.FindObjectOfType<PlayerCtrl>();//GameObject.Find("Player").GetComponent<PlayerCtrl>();


        //  startPosition = tr.position;
        //  print("스타트 :  " + startPosition);
        // StartPosition = tr.InverseTransformPoint(tr.position); //StartTr.InverseTransformPoint(StartTr.position);

        //endPosition = endTr.InverseTransformPoint(endTr.position);

        //fStraightAngle = (StartPosition.y - endPosition.y) / (StartPosition.x - endPosition.x); // 직선의방정식 기울기 
    }

    public void SetPlayer(PlayerCtrl _player)
    {
        player = _player;
    }
    /*
      void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
       {
           isMouseDown = true;

           startPosition = tr.position;
           endPosition = endTr.position;

           fStraightAngle = (endPosition.y - startPosition.y) / (endPosition.x - startPosition.x); // 직선의방정식 기울기 

           player.FlyBtDown();
       }

       void IDragHandler.OnDrag(PointerEventData eventData)
       { 
           if (EventSystem.current.IsPointerOverGameObject())
           {
               tr.position = CalculatePositionBetweenStartPositionAndBoostPosition(Input.mousePosition.x);//Input.mousePosition;////
               if (tr.position == endPosition)
               {
                   player.FlyBtUp();
                   player.boostdown();
               }
               else
               {
                   player.boostup();
                   player.FlyBtDown();
               }
           }
       }

       public void OnPointerUp(PointerEventData eventData)
       {
           isMouseDown = false;
           tr.position = startPosition;

           player.FlyBtUp();
           player.boostup();
       }
       */
    /*
    private Vector3 CalculatePositionBetweenStartPositionAndBoostPosition(float _fX) // 마우스의 x값을 넣어서 y값을 정해주는 함수 
    {
       
        
        if (_fX < startPosition.x) _fX = startPosition.x;
        if (_fX > endPosition.x) _fX = endPosition.x;

       
        float _fY = fStraightAngle * (_fX - startPosition.x) + startPosition.y;
        return new Vector3(_fX,_fY, 0f);
    }
    */
    //}



    public override void OnTouchBegin(Vector2 _pos)
    {
        isMouseDown = true;
        print("플라이버튼 눌림---------------------");
        if (player != null)
        {
            print("플레이어 널이 아님");
            if (startPosition == Vector3.zero)
            {
                startPosition = tr.position;
                endPosition = endTr.position;
            }

            fStraightAngle = (endPosition.y - startPosition.y) / (endPosition.x - startPosition.x); // 직선의방정식 기울기 


            player.FlyBtDown();

            image.sprite = downSp;
            image.color = Vector4.one;
        }
    }

    public override void OnTouchMove(Vector2 _pos)
    {
        if (player != null)
        {
            tr.position = CalculatePositionBetweenStartPositionAndBoostPosition(_pos.x);//Input.mousePosition;////
            if (tr.position == endPosition)
            {
                endTr.gameObject.SetActive(true);
                player.FlyBtUp();
                player.boostdown();

                image.sprite = boostSp;
                
            }
            else
            {
                player.boostup();
                player.FlyBtDown();

                image.sprite = downSp;
            }
        }
    }

    public override void OnTouchEnd(Vector2 _pos)
    {
        isMouseDown = false;
        if (player != null)
        {
            tr.position = startPosition;
            endTr.gameObject.SetActive(false);
            player.FlyBtUp();
            player.boostup();

            image.sprite = idleSp;
            image.color = idleAlpha;
        }
    }

    private Vector3 CalculatePositionBetweenStartPositionAndBoostPosition(float _fX) // 마우스의 x값을 넣어서 y값을 정해주는 함수 
    {
        if (_fX < startPosition.x) _fX = startPosition.x;
        if (_fX > endPosition.x) _fX = endPosition.x;

        float _fY = fStraightAngle * (_fX - startPosition.x) + startPosition.y;
        return new Vector3(_fX, _fY, 0f);
    }

}

