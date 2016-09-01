﻿using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class CameraCtrl : Singleton<CameraCtrl>
{
    [System.Serializable]
    public enum eMoveState { TRIGGER, COLLIDER }
    // LateUpdate쪽으로 바꿔줘야 하는데 .. 무엇을..?
    public PlayerCtrl player = null;
    public Transform targetTr = null;
    private Transform rayTarget;
    private float preYAngle;
    public bool isLookFar;
    Vector3 vLerp;

   // private CamPivot ParentCamp = null;
    DepthOfField dof;
    Component fx_speedLight;
    private List<ObjStruct> preRayHitObjList = new List<ObjStruct>();      //이전에 충돌한 gameObject를 가지고 있는 List
                                                                           //private GameObject Camera;
                                                                           //private List<string> preRayHitObj

    public eMoveState moveState = eMoveState.COLLIDER;

    public float fTargetDist;
    public float fNormalDist;
    public float fFarDist;
    public float fTargetHeight;


    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        //  Camera = 
        
        dof = transform.GetComponent<DepthOfField>();
        fx_speedLight = GetComponentInChildren<ParticleSystem>();
        fx_speedLight.gameObject.SetActive(false);

     //   transform.LookAt(targetTr);
        fTargetDist = 0.8f;//1.5f;
        fNormalDist = 0.8f;//1.5f;
        fFarDist = 18f;
        fTargetHeight = 0.2f;
        isLookFar = false;
        preYAngle = transform.eulerAngles.y;
    }
    
    public void SetPlayer(GameObject _player)
    {
        player = _player.GetComponent<PlayerCtrl>();
        targetTr = GameObject.Find("CamPivot").transform;
       // ParentCamp = player.GetComponentInChildren<CamPivot>();
    }
    public void SetTarget(GameObject _obj)
    {
        if ((targetTr != null) || _obj == null)
            targetTr = null;

        targetTr = _obj.transform;
        transform.LookAt(targetTr);
    }

    public void SetPosition(Vector3 _pos)
    {
        transform.position = _pos;
    }

    public void SetLocalPosition(Vector3 _pos)
    {
        transform.localPosition = _pos;
    }

    void Update()
    {

        if (TimeManager.Instance.isMenuOpen)
            return;
        CameraEffct();
    }

    void LateUpdate()
    {

        if (TimeManager.Instance.isMenuOpen)
            return;

        if (player == null)
        {
            if (!(targetTr == null))
            {
                print("target: " + targetTr);
                transform.position = targetTr.position + (-targetTr.forward * fTargetDist) + Vector3.up * fTargetHeight;
                transform.LookAt(targetTr.position);
            }
        }
        else
        {
            if (player.state != Constants.ST_CLING)        // 붙어있지 않을 때의 카메라 효과(위치)
            {
                isLookFar = false;
                if (fTargetDist != fNormalDist)
                    fTargetDist = Mathf.Lerp(fTargetDist, fNormalDist, 0.3f);

            }

            transform.position = targetTr.position + (-targetTr.forward * fTargetDist) + Vector3.up * fTargetHeight;

            transform.LookAt(targetTr.position);
            //  print("rotate.y: " + transform.eulerAngles.y);

            if (preYAngle - transform.eulerAngles.y > 179f)
            {

                float _y = transform.eulerAngles.y + 180f;

                transform.eulerAngles.Set(transform.eulerAngles.x, _y, transform.eulerAngles.z);
                //  print("preYAngle: " + preYAngle + "eulrAngle.y : " + transform.eulerAngles.y + " _y  : " + _y);

                // Time.timeScale = 0f;
                preYAngle = _y;

            }
            else
                preYAngle = transform.eulerAngles.y;

            // transform.Rotate(-20, 0, 0);
            // transform.rotation = targetTr.rotation;


            rayTarget = player.transform;    // 수정필요, rayTarget을 잡아주는 함수 
            if (moveState == eMoveState.TRIGGER)
            {
                if (player.state == Constants.ST_CLING)
                {
                    if (isLookFar != true)
                    {
                        Transform parentTr = player.GetParent();
                        if ((parentTr != null) && parentTr.CompareTag("RAINDROP"))
                            StartCoroutine("DelayLerpPosition", 0f);
                        else
                            StartCoroutine("DelayLerpPosition", 1f);
                    }
                    else
                    {

                        if (fTargetDist < fFarDist)
                            fTargetDist = Mathf.Lerp(fTargetDist, fFarDist, 0.3f);
                        transform.position = targetTr.position + (-targetTr.forward * fTargetDist) + (targetTr.up * fTargetHeight);
                    }

                }
                //MakeObjTransparent();
            }
            else  // movecState == eMoveState.COLLIDER;
            {
                if (player.state == Constants.ST_CLING)
                {
                    // 카메라가 멀리 떨어지게함...
                    if (isLookFar != true)
                    {
                        StartCoroutine("DelayLerpPosition", 1.5f);

                    }
                    else
                    {
                        // 카메라 충돌 체크, 플레이어가 가려지는 일이 발생하지 않게 함

                        Vector3 vDir = transform.position - targetTr.position;
                        vDir.Normalize();


                        if (fTargetDist < fFarDist)
                            fTargetDist = Mathf.Lerp(fTargetDist, fFarDist, 0.3f);

                        //이게 정상동작 코드
                        // transform.position = CollisionManager.Instance.Get_RayCollisionPositionFromObj(targetTr.position, vDir, fFarDist, "CAMERA");
                        transform.position = CollisionManager.Instance.Get_RayCollisionPositionFromObj(targetTr.position, vDir, fTargetDist, "WALL");

                    }
                }
               // MakeObjTransparent();
            }
        }
        if(!(rayTarget==null))
            MakeObjTransparent();

    }

    private void CameraEffct()
    {
        if (player && targetTr)
        {
            //print("maxBlurSize = " + dof.maxBlurSize);
            if (player.fSpeed > 8f)
            {
                fx_speedLight.gameObject.SetActive(true);
                //transform.GetComponent<DepthTextureMode>();
                //dof.enabled = true;
                //dof.maxBlurSize = 0.52f;
                //  dof.maxBlurSize += 5 * Time.deltaTime;
                // print("maxBlurSize = " + dof.maxBlurSize);
            }
            else if (player.state == Constants.ST_STUN)
            {
                fx_speedLight.gameObject.SetActive(false);
                dof.enabled = true;
                dof.maxBlurSize = 10f;
            }

            else
            {
                fx_speedLight.gameObject.SetActive(false);
                dof.enabled = false;


                dof.maxBlurSize = Mathf.Lerp(dof.maxBlurSize, 0f, 0.008f);
            }
        }
    }

    private void MakeObjTransparent()
    {
        RaycastHit[] RayHit = CollisionManager.Instance.Get_RayCollisionsAFromB(this.transform, rayTarget);     // 여기에 충돌한 정보가 들어감 
        List<ObjStruct> newRayHitObjList = new List<ObjStruct>();   // RayHit에서 충돌한 충돌체들을 넣어줄 것임 

        // RayHit에서 충돌한 충돌체들을 newRayHitObjList에 넣어줌
        for (int i = 0; i < RayHit.Length; ++i)
        {
            if (RayHit[i].collider.gameObject.CompareTag("PLAYER"))
                continue;
            else
            {
                ObjStruct objStruct = new ObjStruct();
                Renderer renderer;
                //  MeshRenderer tempRenderer;  // null 체크할 변수 
                // SkinnedMeshRenderer tempSkinnedRenderer;   // null 체크할 변수 
                objStruct._obj = RayHit[i].collider.gameObject;

                if (preRayHitObjList.Count != 0) // 이미 투명해진 물체가 있는 상황이라면 
                {

                    for (int j = 0; j < preRayHitObjList.Count; ++j)
                    {
                        Shader tempShader;

                        if (!(tempShader = preRayHitObjList.Find(delegate (ObjStruct _objStruct) { return (_objStruct._obj.name == RayHit[i].collider.gameObject.name); })._objShader)) // preRayHitObjList에 있는 이름과 RayHit에 있는 이름과 비교해서 있는 이름인지 확인
                        {   // 이전에 투명해졌던 객체가 아니라면 shader정보를 받아오고, 이미 투명해졌던 객체라면 shader정보를 받아오지 않는다.
                            if (renderer = RayHit[i].collider.gameObject.GetComponent<MeshRenderer>())//GetComponent<MeshRenderer>())  // MeshRenerer가 없는 경우가 있다. 그 경우에는 투명하게 할 필요가 없으므로 break.
                                objStruct._objShader = renderer.material.shader;
                            else if (renderer = RayHit[i].collider.gameObject.GetComponentInChildren<SkinnedMeshRenderer>())//GetComponent<SkinnedMeshRenderer>())
                                objStruct._objShader = renderer.material.shader;
                            else
                                break;

                        }
                        else
                        {
                            objStruct._objShader = tempShader;
                        }

                    }
                }
                else
                {
                    if (renderer = RayHit[i].collider.gameObject.GetComponent<MeshRenderer>())//GetComponent<MeshRenderer>())  // MeshRenerer가 없는 경우가 있다. 그 경우에는 투명하게 할 필요가 없으므로 break.
                    {

                        objStruct._objShader = renderer.material.shader;
                        print("메쉬렌더러 : " + objStruct._objShader.name);
                    }
                    else if (renderer = RayHit[i].collider.gameObject.GetComponentInChildren<SkinnedMeshRenderer>())
                    {
                        objStruct._objShader = renderer.material.shader;
                        print("스킨드메쉬렌더러 : " + objStruct._obj.name);
                    }

                    else
                    {
                        print("브레이크");
                        break;
                    }
                }
                newRayHitObjList.Add(objStruct);

            }
        }

        /*
         // preRayHitObjList와 newRayHitObjList를 비교하여, Ray가 닿지 않은 Obj라면 셰이더 상태를 원래대로 돌려 놓는다.
                for (int j = 0; j < preRayHitObjList.Count; ++j)
                {
                    if (!newRayHitObjList.Find(delegate (ObjStruct _objStruct) { return (_objStruct._obj.name == preRayHitObjList[j]._obj.name); })._obj) //preRayHitObjList[i]);
                    {
                    }
                }
        */

        // newRayHitObjList에 들어가 있는 Obj들의 셰이더를 투명하게 바꿈 
        for (int i = 0; i < newRayHitObjList.Count; ++i)
        {
            Renderer renderer = newRayHitObjList[i]._obj.GetComponent<MeshRenderer>();//newRayHitObjList[i]._obj.GetComponentInChildren<SkinnedMeshRenderer>();//GetComponent<SkinnedMeshRenderer>();

            if (renderer == null)
            {
                renderer = newRayHitObjList[i]._obj.GetComponentInChildren<SkinnedMeshRenderer>();
            }

            if (renderer != null)
            {
                renderer.material.shader = Shader.Find("Transparent/VertexLit");
                // renderer.material.shader = Shader.Find("Mobile/Particles/Multiply");// Transparent/VertexLit");

                // print("투명투명");

                if (renderer.material.HasProperty("_Color"))
                {
                    // print("rayHitObjList[i].collider.gameObject.name : " + newRayHitObjList[i].name);

                    Color prevColor = renderer.material.GetColor("_Color");
                    //print("color : " + prevColor);
                    //renderer.material.SetFloat("_Mode", 3f);
                    renderer.material.SetColor("_Color", new Color(prevColor.r, prevColor.g, prevColor.b, 0.5f));
                    //   print("컬러에 들어옴");
                }
            }
        }



        // preRayHitObjList와 newRayHitObjList를 비교하여, Ray가 닿지 않은 Obj라면 셰이더 상태를 원래대로 돌려 놓는다.
        for (int i = 0; i < preRayHitObjList.Count; ++i)
        {
            if (!newRayHitObjList.Find(delegate (ObjStruct _objStruct) { return (_objStruct._obj.name == preRayHitObjList[i]._obj.name); })._obj) //preRayHitObjList[i]);
            {
                // string nameShader = "Mobile/Unlit (Supports Lightmap)";
                //string nameShader = "Standard";
                //MeshRenderer renderer = preRayHitObjList[i]._obj.GetComponent<MeshRenderer>();
                Renderer renderer = preRayHitObjList[i]._obj.GetComponent<MeshRenderer>();//GetComponentInChildren<SkinnedMeshRenderer>();//GetComponent<SkinnedMeshRenderer>();

                if (renderer == null)
                    renderer = preRayHitObjList[i]._obj.GetComponentInChildren<SkinnedMeshRenderer>();//GetComponent<MeshRenderer>();

                if (renderer != null)
                {
                    renderer.material.shader = preRayHitObjList[i]._objShader;
                    if (renderer.material.HasProperty("_Color"))
                    {
                        // print("rayHitObjList[i].collider.gameObject.name : " + newRayHitObjList[i].name);

                        Color prevColor = renderer.material.GetColor("_Color");
                        //print("color : " + prevColor);
                        //renderer.material.SetFloat("_Mode", 3f);
                        renderer.material.SetColor("_Color", new Color(prevColor.r, prevColor.g, prevColor.b, 1f));
                        //   print("컬러에 들어옴");
                    }
                }
                // renderer.sharedMaterial 을 사용하래  // 수정 
            }
        }

        // swap
        preRayHitObjList = newRayHitObjList;

    }


    private IEnumerator DelayLerpPosition(float fTime)
    {

        yield return new WaitForSeconds(fTime);

        isLookFar = true;

    }

    public void SetStateToCollider(bool _bool)
    {
        if (_bool)
            moveState = eMoveState.COLLIDER;
        else
            moveState = eMoveState.TRIGGER;
    }

}