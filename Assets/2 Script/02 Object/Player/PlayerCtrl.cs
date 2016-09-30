using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using DG.Tweening;

// 해야할 일 : 기울기에 따라 플레이어가 너무 움직인다 - 조정필요 
// 2. 기본 가속도? 
public class PlayerCtrl : TimeAffectedObj//MonoBehaviour
{
    private Transform tr;
    public  Transform targetPlus;
    public  Transform equipPoint;
    private Transform[] tr_Mesh;
    private GameObject fx_boost;
    public  GameObject ClingObj;
    public  GameObject Player_Target;
    private RainDrop dest_script;
    private Rigidbody rigidBody;
    public  Animator anim;
  //  private StatusUICtrl statusUI;

    private Vector3 movement; // 수정- 없어도 될듯 하다 , 물론 코드 바꿔야 함 
    private Vector3 vDir;
    public  Vector3 prePosition;  // 개구리에서쓰고있네..  ??

    // 플레이어 상태와 변수상태가 들어가 있는 변수 
    public ulong state;
    public ulong variable;

    public  float fHP { get; private set; }
    public  float fStamina;                             // 스테미나 총량
    public  float fXAngle { get; private set; }         // 좌우   회전값
    public  float fYAngle { get; private set; }         // 위아래 회전값
    public  float startTime;
    public  float fSpeed;//{ get; private set; }          // 플레이어 최종 속도값
    public  float OWNMAXSPEED = 6f;                     // 일반속도 최대값       
    private float fStaminaMinus;                        // 스테미나 감소량 
    private float fSpeedVal = 2f;                       // 플레이어 속도 증가값 
    private float fBoostMinus = 20f;                    // 부스터 사용 후에 속도감소 변화값 
    private float fBoostPlus = 10f;                     // 부스터 사용 시 속도증가 값
    private float fBoostSpeed;                          // 기본속도에 더해지는 가속도값 
    private float MAXBOOST;                             // Boost 사용시의 최대 가속도값   
    private float fOwnSpeed;                            // 일반속도 값                  
    private float fRotSpeed;
    public  float iBlood = 0; // 흡혈량 ( 미구현 )  ??? 

    public override void Awake()
    {
        //  DontDestroyOnLoad(this);

        base.Awake();

        tr = GetComponent<Transform>();
        tr_Mesh = GetComponentsInChildren<Transform>();
        equipPoint = GameObject.Find("EquipPoint").transform;
        rigidBody = GetComponent<Rigidbody>();

        ClingObj = GameObject.Find("ClingObject");
        fx_boost = GetComponentInChildren<ParticleSystem>().gameObject;
        fx_boost.SetActive(false);
        anim = GetComponent<Animator>();
        //statusUI = GetComponent<StatusUICtrl>();

        vDir = Vector3.zero;
        state = Constants.ST_IDLE;//ST_CLING;
        variable = Constants.BV_bBoost | Constants.BV_IsCanSlow | Constants.BV_bStaminaRecovery;


        fStamina = 200f;
        fStaminaMinus = 40f;

        fXAngle = 0f;
        fYAngle = 0f;

        fSpeed = 0f;
        fRotSpeed = 80f;//55f;
        // fOwnRotSpeed = 55f;
        fBoostSpeed = 0f;
        MAXBOOST = 10f;

    }
    public override void MyFixedUpdate()//void FixedUpdate()
    {
        prePosition = tr.position;
        Move();
        variable &= ~(Constants.BV_bCollisionOthers);


    }
    public override void MyUpdate() //void Update()
    {
        KeyInput();
        Action();
        RotateAnimation();
        Animate();
        blooding();
        //  print("Update state:" + state);

        /*
                 print("┌──────────────────────────────────────────┐");
        
                if ((variable & Constants.BV_IsBoost) > 0)
                    print("│ isBoost");
                if ((variable & Constants.BV_bBoost) > 0)
                    print("│ bBoost");
                if ((variable & Constants.BV_bCling) > 0)
                    print("│ bCling");
                if ((variable & Constants.BV_bStun) > 0)
                    print("│ bStun");
                if ((variable & Constants.BV_IsCling) > 0)
                    print("│ isCling");
                if ((variable & Constants.BV_ClickRaindrop) > 0)
                    print("│ BV_ClickRaindrop");
                if ((variable & Constants.BV_bCollisionOthers) > 0)
                    print("│ BV_bCollisionOthers");
        if (state == Constants.ST_HOLD)
            print("state = hold");
        if (state == Constants.ST_CLING)
            print("state = cling");

        if ((variable & Constants.BV_IsMove) > 0)
            print("| BV_IsMove");
        else
            print("| BV_IDLE");

            if ((variable & Constants.BV_IsInStage) > 0)
                     print("│ isInStage");
                else
                     print("Not Stage");
                    */
        //print("state : " + state);
        //  print("isMovable = " + isMovable);
        //print("Update velocity : " + rigidBody.velocity);
        
                /*
                if (dest_script != null)
                    print("Player_dest : " + dest_script.tag);
                else
                    print("NULL");
                    */
    }



    private void blooding()
    {
        if ((variable & Constants.BV_bBlood) > 0)
        {
            startTime += Time.deltaTime;
        }
        else
        {

            variable &= ~(Constants.BV_bBlood);
        }

        if (startTime > 5)
        {
            ClingBtUp();
            startTime = 0;
        }

    }
    private void KeyInput()     // StateCheck 로 이름을 바꾸자..
    {
        if (state != Constants.ST_STUN)// && state != Constants.ST_CLING)
        {
#if UNITY_ANDROID
                fXAngle = Input.acceleration.x * 1.5f;      // fYRotation : 좌우 각도 변경  
                fYAngle = -(Input.acceleration.y * 1.5f) - 0.7f;    // fXRotatino : 상하 각도 변경 , 0.4 는 각도 좀더 세울수 있게 마이너스 한것      
#else
            fXAngle = Input.GetAxis("Horizontal");
            fYAngle = Input.GetAxis("Vertical");
#endif

            if ((-0.15f < fXAngle) && (fXAngle < 0.15f))
                fXAngle = 0f;
            if ((-0.1f < fYAngle) && (fYAngle < 0.15f))
                fYAngle = 0f;

            if (state == Constants.ST_CLING)
            {
                fXAngle *= 10f;
                fYAngle *= 10f;
            }

        }

        ///////////////////////////////////////////////// 마우스왼쪽 클릭
        if (TouchEventManager.Instance.isTouchBegin3DObj == true)
        {
            if (TouchEventManager.Instance.raindrop != null)    // touch한 물체가 raindrop이라면 
            {
                if (Constants.ST_CLING == state && (variable & Constants.BV_ClickRaindrop) > 0) //true == isCling)    // 붙어있는 상태라면 떨어질 수 있게 한다 . 붙은 상태여야 떨어질 수 있음 
                {
                    print("MakePArentNull - 1");
                    SetParentNull();
                    variable &= ~(Constants.BV_bCling);         //  bCling = false;
                    variable &= ~(Constants.BV_IsBoost);

                    Player_Target = null;                         // 목표 물방울이 없어진다 
                    if (dest_script != null)
                        dest_script.Change_CheckState(false);   // 타겟빗방울에게 플레이어가 타겟으로 삼지 않음을 알림 
                    dest_script = null;

                    variable &= ~(Constants.BV_ClickRaindrop);  // isClickRaindrop = false;
                    variable &= ~(Constants.BV_IsCling);        // isCling = false;// state = Constants.ST_FLYING; // 날아가는 상태로 바꿔주자
                                                                //state = Constants.//isCling = false;            // 붙지 않은 상태가 된다 - 다음번에 알아서 
                }
                else if ((variable & Constants.BV_bCling) == 0)//false == bCling)//bClickRaindrop)   // 물방울이 클릭 되지 않은 상태면서 물방울에 붙은 상태가 아니라면 물방울을 클릭한 상태로 바꿔준다, // RAINDROP 레이어어를 가진 물체와 raycast // 상태변경 
                {

                    if (Player_Target == null)   // hold수정??  hold 상태에서는 어쩔것인가
                    {

                        Player_Target = TouchEventManager.Instance.raindrop; //CollisionManager.Instance.Get_MouseCollisionObj(100f, "RAINDROP");
                        if (Player_Target != null)
                        {
                            dest_script = Player_Target.GetComponent<RainDrop>();
                            if (dest_script != null)
                                dest_script.Change_CheckState(true);    // 빗방울에게 플레이어가 타겟으로 삼았음을 알림

                            variable |= Constants.BV_bCling;//bCling = true;
                            variable |= Constants.BV_ClickRaindrop;//isClickRaindrop = true;

                            variable |= Constants.BV_IsBoost;
                        }
                    }
                }
            }
            else  // touch 물체가 Raindrop이 아니면 
            {
                if (Constants.ST_CLING == state && (variable & Constants.BV_ClickRaindrop) > 0)//true == isCling)    // 붙어있는 상태라면 떨어질 수 있게 한다 . 붙은 상태여야 떨어질 수 있음 
                {
                    print("MakePArentNull - 2");
                    SetParentNull();
                    variable &= ~(Constants.BV_bCling); //  bCling = false;
                    variable &= ~(Constants.BV_IsBoost);

                    Player_Target = null;       // 목표 물방울이 없어진다 
                    if (dest_script != null)
                        dest_script.Change_CheckState(false); // 타겟빗방울에게 플레이어가 타겟으로 삼지 않음을 알림 
                    dest_script = null;

                    variable &= ~(Constants.BV_ClickRaindrop);// isClickRaindrop = false;
                    variable &= ~(Constants.BV_IsCling);// isCling = false;// state = Constants.ST_FLYING; // 날아가는 상태로 바꿔주자
                                                        //state = Constants.//isCling = false;            // 붙지 않은 상태가 된다 - 다음번에 알아서 
                }
            }
        }
    }
    private void Action()   // 플레이어 모델이 직접 움직이지는 않으나, 속도변경 같은 코드가 들어감. State의 상태는 여기서만 바뀌게 된다.
    {
        if ((variable & Constants.BV_IsInStage) == 0)//state == Constants.ST_IDLE) // Idle 이면 시작전 
            return;

        if ((variable & Constants.BV_bStun) > 0)
            state = Constants.ST_STUN;
        else if ((variable & Constants.BV_IsMove) > 0)
            state = Constants.ST_FLYING;
        else
        {
            state = Constants.ST_IDLE;
            fOwnSpeed = 0f;
            fSpeed = 0f;
        }



        if (Boost())
        {
            state = Constants.ST_BOOST;
            fx_boost.SetActive(true);
        }
        else { fx_boost.SetActive(false); }


        if (((variable & Constants.BV_IsCling) > 0) && (variable & Constants.BV_bBlood) > 0) //isCling)  
        {
            state = Constants.ST_BLOOD;
        }
        else if ((variable & Constants.BV_IsCling) > 0)
        {
            state = Constants.ST_CLING;
            variable &= ~(Constants.BV_IsMove);     // 붙은상태면 움직이지 않게 함 
        }
        else if ((variable & Constants.BV_IsHold) > 0)
        {
            state = Constants.ST_HOLD;
        }

        if (((variable & Constants.BV_bCling) > 0) && (variable & Constants.BV_ClickRaindrop) > 0)//true == isInRainzone && // true == bCling )//&& false == isCling ) // rainzone 안에 있고, 빗방울에  붙으려고 할 때 
        {
            fSpeed = (OWNMAXSPEED + MAXBOOST) * 2.5f;      // 수정필요 , lerp필요(카메라 ) 
            rigidBody.velocity = Vector3.zero; // 수정수정 수정 수정 수정!!!!!!!!!!!!!!!!

            if ((null != Player_Target) && (variable & Constants.BV_ClickRaindrop) > 0)// (null != dest_script))                          // 목표 빗방울이 있다면 
            {
                vDir = (Player_Target.transform.position) - tr.position;
                vDir.y -= 1f;

                vDir.Normalize();
                if (dest_script != null)
                {
                    if (dest_script.isPlop) // 목표 빗방울이 비활성화 상태라면 빗방울이 이미 다른 오브젝트에 충돌해서 사라졌다는 소리.빗방울이 없어지게 되면 vDir은 이전에 받은 값을 유지한다.(빗방울이 사라져서 없어진 방향 )
                    {
                        print("셋 낫클링");
                        print("Dest_Script.isPlop :" + dest_script.isPlop);
                        if (dest_script.transform == Player_Target.transform)
                            print("Player_Target == dest_script");
                        else
                            print("Player_Target과 dest_script가 다르다니 이게 무슨 소리오!");

                        Player_Target = null;
                        dest_script = null;

                       
                        SetState_NotCling();
                    }
                }

            }
        }
    }
    private bool Boost()    // boost사용이라면 true리턴, 비행상태 체크 
    {
        if (((variable & Constants.BV_bCollisionOthers) == 0) && ((variable & Constants.BV_IsBoost) > 0) && ((variable & Constants.BV_bBoost) > 0) && (fStamina > 0))//(bCheckBoost)) 
        { // 충돌하지 않았고, 현재 스테가 스테미나 감소량보다 크고, Boost가 사용가능하고 Boost를 사용하는중이면

            if ((variable & Constants.BV_ClickRaindrop) == 0)
                fStamina -= (fStaminaMinus * Time.deltaTime);

            // 가속도 조절
            if (fOwnSpeed < OWNMAXSPEED)
                fOwnSpeed += (fBoostPlus * Time.deltaTime);
            else
            {
                fOwnSpeed = OWNMAXSPEED;

                fBoostSpeed += (fBoostPlus * Time.deltaTime);
                if (fBoostSpeed > MAXBOOST)
                    fBoostSpeed = MAXBOOST;
            }


            fSpeed = fOwnSpeed + fBoostSpeed;
            if (fStamina < 0)               // 스테미나가 0 이하로 떨어지면 부스터를 사용할 수 없다 
            {
                fStamina = 0;
                variable &= ~(Constants.BV_bBoost); //bCheckBoost = false;
                                                    //   state = Constants.ST_FLYING;
                StartCoroutine("DelayStaminaRecovery", 2.5f); // 2.5초간 스테미나 회복을 할 수 없다 
            }
            return true;
        }
        else  // Boost가 아닌 일반이동
        {
            if ((variable & Constants.BV_bStaminaRecovery) > 0)  // 스테미나가 회복되기 위한 조건 
                fStamina += 0.1f;     //수정

            if (((variable & Constants.BV_IsCling) > 0)/* && ((variable & Constants.BV_IsHold) ==0)*/)  ///////////// Cling 상태라면, 속도를 낮춘다 
            {
                fSpeed = 0f;
                fOwnSpeed = 0f;
            }
            else                        ////////////// 붙어있지 않은 상황 
            {
                // 최고속도까지 도달하지 못했으면 속도를 차츰 증가시켜준다 
                if (fOwnSpeed >= OWNMAXSPEED)
                    fOwnSpeed = OWNMAXSPEED;
                else
                    fOwnSpeed += (fSpeedVal * Time.deltaTime);

                // 부스터를 사용한 후에 속도를 조금씩 줄여준다 
                if (fBoostSpeed > 0)
                    fBoostSpeed -= (Time.deltaTime * fBoostMinus);
                else
                    fBoostSpeed = 0f;

                fSpeed = fOwnSpeed + fBoostSpeed;
        
                if (fStamina > 200 + UI.Instance.plusSt * 10)       // 수정필요
                    fStamina = 200 + UI.Instance.plusSt * 10;

                if (fStamina > 10f)      // 스테미나가 10이상이면 사용가능
                    variable |= Constants.BV_bBoost;//bCheckBoost = true;
            }
            return false;
        }

    }
    private void Move()     // 실제 플레이어 객체가 움직이는 코드가 들어있다   ㅅㅂ!!!!!!!!!!!!! 여기수정 여깃 ㅜㅈㅇㄹ ㅓㄴ이ㅏ런ㅇ;런아ㅣ;런아ㅣ;런ㅇ 
    {
        if (/*(state == Constants.ST_IDLE) ||*/ (variable & Constants.BV_Stick) > 0) // 상태가 IDLE이거나 , 어딘가에 달라붙은 경우라면 움직이지 못함 
        {
            //  print("1번");
        }
        else if ((state == Constants.ST_STUN)) // 플레이어가 스턴상태이면 중력을 받는 것 처럼 떨어뜨림
        {
            //    print("2번");
            // rigidBody.MovePosition(tr.position + (-Vector3.up * Time.deltaTime));
            rigidBody.velocity = (-Vector3.up * 5f * Time.deltaTime);// tr.position + (-Vector3.up * Time.deltaTime);

        }

        else if ((!(Constants.ST_CLING == state) && !(Constants.ST_BLOOD == state)) || (Constants.ST_CLING == state && (variable & Constants.BV_IsHold) > 0)) // 어딘가에 붙어있지 않다면. 일반적인 움직임, Boost || 물건을 들고있는경우 
        {
            Debug.DrawRay(tr.position, tr.forward * 1f, Color.blue);
            if (((variable & Constants.BV_ClickRaindrop) > 0) && ((variable & Constants.BV_bCling) > 0))//true == isInRainzone && 
            {
                //   print("3번");
                // rigidBody.MovePosition(tr.position + (vDir * fSpeed * Time.deltaTime)); //이녀석
                rigidBody.velocity = vDir * fSpeed;
                //   print("vDir : " + vDir);
                //    print("vDir :" + vDir);
            }
            else
            {

                //  state = Constants.ST_MOVE;
                // tr.localRotation.eulerAngles.Set(-fYAngle * Time.deltaTime * fRotSpeed, fXAngle * Time.deltaTime * fRotSpeed, 0f);
                //tr.localRotation.eulerAngles.Set(0f, fXAngle * Time.deltaTime * fRotSpeed, 0f);
                //  tr.Rotate(Vector3.up * fXAngle * Time.deltaTime * fRotSpeed, Space.World);   // 좌우
                // tr.Rotate(Vector3.right * -fYAngle * Time.deltaTime * fRotSpeed, Space.Self);  // 위아래
                // => _fY, _fX 구해서 localRotation으로 코드 변경 

                Vector3 angle = tr.eulerAngles;
                angle.y = tr.eulerAngles.y + (fXAngle * Time.deltaTime * fRotSpeed);
                angle.x = tr.eulerAngles.x + (-fYAngle * Time.deltaTime * fRotSpeed);

                if (angle.x >= 360f)
                    angle.x %= 360f;  // 360이하의 수로 만듦
                if (angle.x > 180f)
                    angle.x -= 360f;  // 180이 넘는 수는음수가 됨

                angle.x = Mathf.Clamp(angle.x, -90f, 90f);
              
                angle.z = 0f;

                tr.localRotation = Quaternion.Euler(angle);

                //print("angle : " + tr.rotation.eulerAngles);
                // state로 해도 될걸-수정
                if ((variable & Constants.BV_IsMove) > 0)
                {
                    // print("4번 - 전진! ");
                    //    print("speed : " +  fSpeed);
                    // 회전 

                    // 움직임
                    movement.Set(tr.forward.x, tr.forward.y, tr.forward.z);
                    rigidBody.velocity = (movement * fSpeed);// * Time.deltaTime);

                    Debug.DrawRay(tr.position, movement * 200f, Color.red);
                    // print("tr: " + tr.forward + "movement : " + movement + "Speed: " + fSpeed);

                }
                else
                {
                    //  print("5번");
                    rigidBody.velocity = Vector3.zero;
                }
            }
        }
        else // 붙어 있을 시 아무 동작도 하지 않도록 함 
        {
            // print("6번");
            // tr.Rotate(Vector3.up * fXAngle * Time.deltaTime * fRotSpeed, Space.Self);
            // tr.Rotate(Vector3.right * -fYAngle * Time.deltaTime * fRotSpeed, Space.Self);
        }
    }
    private void RotateAnimation()  // 플레이어모델이 회전할때 대각선으로 기울어진 느낌을 주도록 한다 
    {
        /*  for (int i = 1; i < tr_Mesh.Length; ++i)
              {
               tr_Mesh[i].localRotation = Quaternion.Euler((Vector3.up * fXAngle * 20.0f)
                                                    + (Vector3.right * -fYAngle * 20.0f));
             // tr_Mesh[i].localRotation = Quaternion.Euler(-fYAngle * 20.0f, 0f
              //                                      , (fXAngle * 20.0f));
              }
     */
        // tr_Mesh[1].localRotation = Quaternion.Euler((Vector3.up * fXAngle * 20.0f)
        //                                 + (Vector3.right * -fYAngle * 20.0f));
        /*tr_Mesh[1].localRotation = Quaternion.Euler(-fYAngle * 20.0f, fXAngle * 5f//0f
                                        , (-fXAngle * 20.0f));*/
        if (state == Constants.ST_BOOST)
        {
            //  tr_Mesh[1].localRotation = Quaternion.Slerp(tr_Mesh[1].localRotation, Quaternion.Euler(35f, 0f, 0f), 0.05f);
            tr_Mesh[1].localRotation = Quaternion.Slerp(tr_Mesh[1].localRotation, Quaternion.Euler(35f, fXAngle * 20.0f, -fXAngle * 20.0f), 0.05f);

        }

        else if (state != Constants.ST_CLING)
        {
            /*    tr_Mesh[1].localRotation = Quaternion.Slerp(tr_Mesh[1].localRotation
                                                            , Quaternion.Euler(-fYAngle * 20.0f, fXAngle * 5f
                                                            , -fXAngle * 20.0f), 0.05f);*/
            tr_Mesh[1].localRotation = Quaternion.Slerp(tr_Mesh[1].localRotation
                                                        , Quaternion.Euler(-fYAngle * 20.0f, fXAngle * 20.0f
                                                        , -fXAngle * 10f), 0.05f);
        }
    }
    public void SetState_NotCling()    // 플레이어가 어딘가에 붙어있다면 붙지 않은 상태로 만들어주는 함수
    {
        if (Constants.ST_CLING == state)//isCling)
        {
            print("setState_notcling");
            variable &= ~(Constants.BV_ClickRaindrop);//isClickRaindrop = false;

            print("MakePArentNull - 3");
            SetParentNull();
            //tr.transform.parent = null;
            //ClingObj.transform.parent = null;

            variable &= ~(Constants.BV_bCling); //bCling = false;
                                                //isCling = false;
                                                //  state = Constants.ST_FLYING;
        }

    }
    public void SetTransform(Vector3 _pos, Quaternion _rot)
    {
        transform.position = _pos;
        transform.rotation = _rot;
    }
    public void SetIsInStage(bool _bStage)
    {
        if (_bStage)
            variable |= Constants.BV_IsInStage;
        else
            variable &= ~(Constants.BV_IsInStage);
    }
    public void SetStateStick(bool _bStick)
    {
        if (_bStick)
            variable |= Constants.BV_Stick;//isStick = true;
        else
            variable &= ~(Constants.BV_Stick);
    }

    public void ChangeTimeScale()       // 수정 - bcling?
    {                                                           // 이부분을 플레이어 상태와 비교해야 하나??
        if (((variable & Constants.BV_IsCanSlow) > 0) && ((variable & Constants.BV_bCling) == 0))//true == isCanSlow && false == bCling)
        {
            Time.timeScale = 0.3f;
            variable &= ~(Constants.BV_IsCanSlow); //isCanSlow = false;
            StartCoroutine("ChangeSlowVal");
        }
    }

    public void Drop()
    {
        if ((variable & Constants.BV_IsHold) > 0)
        {
            print("Drop");
            //isHold = false;
            variable &= ~(Constants.BV_IsHold);
            Collider[] colls = equipPoint.GetComponentsInChildren<Collider>();
            Rigidbody rBody = equipPoint.GetComponentInChildren<Rigidbody>();

            // Player에게는 RigidBody하나, Collision하나 있으니 그것을 제외하고
            for (int i = 0; i < colls.Length; ++i)
                colls[i].enabled = true;

            if (!rBody)
                return;//                Time.timeScale = 0f;

            rBody.isKinematic = false;
            //tr.GetComponent<Collider>().enabled = true;
            //tr.GetComponent<Rigidbody>().isKinematic = false;

            equipPoint.DetachChildren();// 흠...
        }

    }

    public Transform GetParent()
    {
        if (tr.transform.parent)
            return tr.transform.parent;

        return null;

    }
    private void SetParent(Transform collTr)
    {
                print("setParent");
        ClingObj.transform.parent = collTr;
        tr.transform.parent = ClingObj.transform;
    }
    public void SetParentNull()
    {
        tr.gameObject.transform.parent = null;
        //  tr.transform.localScale = new Vector3(1, 1, 1);  // ?? 수정?
        ClingObj.transform.parent = null;

        variable &= ~(Constants.BV_Stick);

        rigidBody.isKinematic = false;
    }

    public void Equip(GameObject _item)  // 외부에서 불러주게 됨. 물건잡기
    {
        if (Player_Target == _item)
        {
            print("Equip");
            //isHold = true;
            variable |= Constants.BV_IsHold;
            //    Player_Target.transform.parent = tr; // 이건 아이템쪽에서 하게됨 
            Collider[] colls = _item.GetComponentsInChildren<Collider>();

            for (int i = 0; i < colls.Length; ++i)
                colls[i].enabled = false;

            _item.GetComponent<Rigidbody>().isKinematic = true;
            // Player_Target.GetComponent<Collider>().enabled = false;
            // Player_Target.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private IEnumerator StartConfused(float fTime) // 캐릭터의 상태를 confused로 바꿔주는 함수
    {
        variable |= Constants.BV_bStun;//isConfused = true;
                                       // Damaged(5);
        yield return new WaitForSeconds(fTime);

        variable &= ~(Constants.BV_bStun);//isConfused = false;
    }
    private IEnumerator ChangeSlowVal()
    {
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1f;
        //  print("시간이 돌아왔다, 쿨타임 5초 ");

        yield return new WaitForSeconds(5f);

        variable |= Constants.BV_IsCanSlow;//isCanSlow = true;
    }
    private IEnumerator SetVelocityZero()
    {
        yield return new WaitForSeconds(0.5f);  // 0.5초동안 다른 물체의 영향을 받게 한다 
        rigidBody.velocity = Vector3.zero;
    }
    public void SetVelocity(Vector3 _vel)
    {
        rigidBody.velocity = _vel;
    }
    private IEnumerator DelayStaminaRecovery(float fTime)
    {
        variable &= ~(Constants.BV_bStaminaRecovery);   // 스테미나 회복 불가능 상태 

        yield return new WaitForSeconds(fTime);    // 부스터 사용 후 1초간 스테미너가 증가하지 않게 한다 
        variable |= Constants.BV_bStaminaRecovery;

    }
    public void Damaged(int iDamage)    //데미지를 입으면 스턴 효과도 함께 온다 
    {
        // 데미지를 입으면 HP가 감소하고, 속도가 0이 된다.
        fHP -= iDamage;
        fOwnSpeed = 0f;
        fBoostSpeed = 0f;
        //statusUI.SetPlayerHPBar(iHP);
        StartCoroutine("StartConfused", 0.5f);

        if ((variable & Constants.BV_bCling) > 0) // 어떤 부모에 붙은 상태라면 해제
        {
            if ((variable & Constants.BV_IsCling) > 0)
            {
                print("MakePArentNull - 4");
                SetParentNull();
            }

            variable &= ~(Constants.BV_bCling);
            variable &= ~(Constants.BV_IsCling);
            variable &= ~(Constants.BV_IsHold);

            Player_Target = null;       // 목표 물방울이 없어진다 
            dest_script = null;
            variable &= ~(Constants.BV_ClickRaindrop);// isClickRaindrop = false;
        }
        // 빗방울같은 경우, 목표로 한 빗방울이사라진다면? ( 빗방울은 사라질 때 데미지를 호출한다 ) 

        if (fHP < 0)
            state = Constants.ST_DEATH;
    }

    void OnCollisionEnter(Collision coll)
    {
        if ((variable & Constants.BV_bCling) > 0)//bCling)  // 붙으려고 하는 상태면 
        {
            if (Player_Target != null)
            {

                //text.text = "playerDest는 널이 아님 ";
                if (Player_Target.gameObject == coll.gameObject)    // 충돌한 물체가 목표물과 같다면 달라붙는다 -- 벽이 여기서 에러 
                {
                    if (coll.gameObject.layer != LayerMask.NameToLayer("MOVABLE") && (variable & Constants.BV_IsHold) == 0)
                    {
                        rigidBody.velocity = Vector3.zero;
                        SetParent(coll.transform);
                        variable |= Constants.BV_IsCling;//isCling = true;
                    }

                    //text.text = "bv_iscling으로 바꿈";
                }
                else    // 충돌한 물체가 목표물과 다르다면 붙으려고 하는 상태 해제됨
                {
                    StartCoroutine("SetVelocityZero");// 일정시간후 velocity를 0으로 만들어주는 함수고
                    if (fSpeed > OWNMAXSPEED)   // 플레이어가 붙으려고 하지 않으면 충돌 시 데미지를 줄거지.
                        Damaged(5);
                    // text.text = "bv_iscling이 되지 않음 ";
                }
            }
            else // 붙으려고 하는 상태인데 목표물이 없다면, 목표물이 사라졌음 ( 빗방울 )
            {
                print("벨로시티 제로");
                //text.text = "playerDest는 널임";
                StartCoroutine("SetVelocityZero");// 일정시간후 velocity를 0으로 만들어주는 함수고
                if (fSpeed > OWNMAXSPEED)   // 플레이어가 붙으려고 하지 않으면 충돌 시 데미지를 줄거지.
                {
                    print("데미지");
                    Damaged(5);
                }
            }
        }
        else    // 벽이나 물방울에 붙지 않는 상태인데 일정속도 이상으로 부딪히면 충돌효과를 준다 
        {
            StartCoroutine("SetVelocityZero");  // 일정시간후 velocity를 0으로 만들어주는 함수고
            if (fSpeed > OWNMAXSPEED + 0.1f)    // 플레이어 속도가 빠르면 데미지를 줄거고, 부딪히는 물건이 또 데미지를 추가적으로 준다 
                Damaged(30);                     // 여기에서 스턴효과도 함께 준다 
        }

        if (coll.gameObject.tag == "human")
        {
            print("human");
            variable |= Constants.BV_bBlood;

        }

        if (coll.gameObject.tag == "Arrive")
            iBlood = 200;
    }

    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject != null)
        {
            variable |= (Constants.BV_bCollisionOthers);   // 충돌했음을 알림
            variable &= ~(Constants.BV_IsBoost);
        }
    }

    public void FlyBtDown()
    {
        if (state != Constants.ST_CLING)//((variable & Constants.BV_IsCling) == 0)
        {
            variable |= Constants.BV_IsMove;
            // text.text = "FlyBtDown";
        }

    }
    public void FlyBtUp()
    {
        variable &= ~(Constants.BV_IsMove);
        rigidBody.velocity = Vector3.zero;
    }
    public void boostdown()
    {
        state = Constants.ST_BOOST;
        variable |= Constants.BV_IsBoost;
        variable |= Constants.BV_IsMove;
    }
    public void boostup()
    {
        ///////////////////////////////////////////////// 스페이스바 뗄 때
        variable &= ~(Constants.BV_IsMove);
        variable &= ~(Constants.BV_IsBoost);//isBoost = false;
                                            // StartCoroutine("DelayStaminaRecovery", 1f);
        if (fStamina < 10)
        {
            variable &= ~(Constants.BV_bBoost); //bCheckBoost = false;
        }


    }

    public void ClingBtDown()
    {
        if (Player_Target = CollisionManager.Instance.Get_RaycastCollisionObj(tr.position, tr.forward, 1f))      //붙음
        {
            variable |= Constants.BV_bCling;

            if (Player_Target.gameObject.layer == LayerMask.NameToLayer("MOVABLE"))
            {
                //variable |= Constants.BV_IsHold;
                variable &= ~(Constants.BV_IsCling);
            }
            else if (state != Constants.ST_HOLD)
            {
                variable |= Constants.BV_IsCling;
                variable &= ~(Constants.BV_IsHold);
            }

            return;
        }

        // 붙지 않은상태면 붙으려고  하는 상태인지 체크
        if ((Player_Target = CollisionManager.Instance.Get_RaycastCollisionObj(tr.position, tr.forward, 10f)))
        {
            variable |= Constants.BV_bCling;//bCling = true;
                                            //  if((variable & Constants.BV_IsCling) > 0)
                                            //    variable &= ~(Constants.BV_IsMove);
            //state = Constants.ST_CLING;//|= Constants.ST_CLING;
        }
    }

    public void ClingBtUp()
    {
        if ((variable & Constants.BV_bCling) > 0)//Constants.ST_CLING == state)
        {
            if (Constants.ST_CLING == state || Constants.ST_BLOOD == state || Constants.ST_HOLD == state)
            {
                if ((variable & Constants.BV_IsHold) > 0)
                {
                    Drop();
                }
                else
                {
                    print("이쪽");
                    print("MakePArentNull - 5");
                    SetParentNull();
                }
            }
            /*tr.transform.parent = null;
            //  tr.transform.localScale = new Vector3(1, 1, 1);  // ?? 이거 꼭 필요한가
            ClingObj.transform.parent = null;*/

            variable &= ~(Constants.BV_bCling);//bCling = false;
            variable &= ~(Constants.BV_IsCling);//isCling = false;
            variable &= ~(Constants.BV_bBlood);
        }
    }

    public void SetHP(int _iHP)
    {
        fHP = _iHP;
    }
    /*
    public void MoveHorizontal(Vector3 _vDest)
    {
        rigidBody.MovePosition(Vector3.Lerp(tr.position, _vDest, 0.2f));

        if ((tr.position.x > (_vDest.x - 0.1f)) && (tr.position.x < (_vDest.x + 0.1f)))
        {
            tr.position = _vDest;
        }
    }
    */
    private void Animate()
    {
        switch (state)
        {
            case Constants.ST_FLYING:
                {
                    anim.SetInteger("state", 0);
                    //  anim.speed = PlayerCtrl.Instance.fSpeed * 0.2f;
                    // print("Player speed : " + PlayerCtrl.Instance.fSpeed);
                    // print("anim speed : " + anim.speed);
                    break;
                }
            case Constants.ST_IDLE:
                {
                    anim.SetInteger("state", 1);
                    break;
                }
            case Constants.ST_CLING:
                {
                    anim.SetInteger("state", 2);
                    break;
                }

            case Constants.ST_STUN:
                {
                    anim.SetInteger("state", 3);
                    break;
                }
            case Constants.ST_BLOOD:
                {
                    anim.SetInteger("state", 4);
                    break;
                }
        }
    }
}