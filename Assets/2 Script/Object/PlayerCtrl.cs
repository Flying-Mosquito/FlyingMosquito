using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using DG.Tweening;

// 해야할 일 : 기울기에 따라 플레이어가 너무 움직인다 - 조정필요 
// 2. 기본 가속도? 
public class PlayerCtrl : Singleton<PlayerCtrl>//MonoBehaviour
{
    //  public Text text;
    //private CharacterController controller;
    private Transform tr;
    private Transform[] tr_Mesh;
    public Transform targetPlus;
    private Rigidbody rigidBody;
    private Vector3 movement; // 수정- 없어도 될듯 하다 , 물론 코드 바꿔야 함 
    private Vector3 vDir;
    private GameObject Player_Target;
    private RainDrop dest_script;
    private GameObject fx_boost;
    public Vector3 prePosition;
    public float startTime;
    // 플레이어 상태와 변수상태가 들어가 있는 변수 
    public ulong state { get; private set; }
    public ulong variable;

    public float iHP { get; private set; }
    public float fStamina; // 스테미나 총량
    private float fStaminaMinus;    // 스테미나 감소량 
    public float fSpeed { get; private set; }       // 플레이어 최종 속도값
    private float fSpeedVal = 2f;                    // 플레이어 속도 증가값 
    private float fBoostMinus = 20f;                  // 부스터 사용 후에 속도감소 변화값 
    private float fBoostPlus = 10f;                     // 부스터 사용 시 속도증가 값
    private float fBoostSpeed;    // 기본속도에 더해지는 가속도값   private
    private float MAXBOOST;// { get; private set; }     // Boost 사용시의 최대 가속도값   private
    public float OWNMAXSPEED = 6f;                       // 일반속도 최대값        private
    private float fOwnSpeed;//    { get; private set; }    // 일반속도 값                   private
    private float fRotSpeed;// { get; private set; }                    //private
                            // public  float fOwnRotSpeed  { get; private set; }     //private 

    public float fXAngle { get; private set; }      // 좌우   회전값
    public float fYAngle { get; private set; }      // 위아래 회전값


    public GameObject ClingObj;
    EnemyAI enemyai;
    public int iBlood = 0; // 흡혈량 ( 미구현 )
    private bool isMovable; // Cling할 물체가 플레이어가 이동시킬 수 있는 물체인지 확인 

    public int[] stage = new int[2] { 0, 1 };

    void Awake()
    {
        DontDestroyOnLoad(this);
        stage = new int[9];
        for (int i = 0; i < 9; i++)
        {
            stage[i] = 0;
        }
        enemyai = gameObject.GetComponent("EnemyAI") as EnemyAI;
        ClingObj = GameObject.Find("ClingObject");
        tr = GetComponent<Transform>();
        tr_Mesh = GetComponentsInChildren<Transform>();
        rigidBody = GetComponent<Rigidbody>();
        fx_boost = GetComponentInChildren<ParticleSystem>().gameObject;
        fx_boost.SetActive(false);

        vDir = Vector3.zero;
        state = Constants.ST_IDLE;//ST_CLING;
        variable = Constants.BV_bBoost | Constants.BV_IsCanSlow | Constants.BV_bStaminaRecovery;


        fStamina = 200f;
        fStaminaMinus = 40f;

        fXAngle = 0f;
        fYAngle = 0f;

        fSpeed = 0f;
        fRotSpeed = 55f;
        // fOwnRotSpeed = 55f;

        fBoostSpeed = 0f;
        MAXBOOST = 10f;

        isMovable = false;
    }
    void FixedUpdate()
    {
        prePosition = tr.position;
        Move();
        variable &= ~(Constants.BV_bCollisionOthers);


    }
    void Update()
    {
        KeyInput();
        Action();
        RotateAnimation();
        CheckS();
       
        
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
                
                if (Player_dest != null)
                    print("Player_dest : " + Player_dest.tag);
                else
                    print("NULL");
                    */
    }
    public void CheckS()
    {
        for (int i = 0; i < 9; i++) {
            if (Application.loadedLevelName == "Stage"+i.ToString())
            {
                stage[i] = 1;
                if (Application.loadedLevelName == "Stage3")
                blooding();

            }
        }
       
    }
    public void blooding()
    {
        //if (Vector3.Distance(new Vector3(this.transform.position.x, 0, 0), new Vector3(enemyai.transform.position.x, 0, 0)) < 5)
        if ((variable & Constants.BV_bBlood) > 0)
        {
            startTime += Time.deltaTime;
            //  print(startTime);
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

                    if (Player_Target == null)
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

        /*

        if (Input.GetMouseButton(0))
        {
            // state = Constants.ST_FLYING;
            // if (true == isInRainzone) // Rainzone 안에 있으면서 물방울을 향하는 행동이나, 물방울에서 떨어지는 상태 변경 
            // {
            if (Input.GetMouseButtonDown(0)) // 입력을 한번만 받는다
            {
                if (Constants.ST_CLING == state && (variable & Constants.BV_ClickRaindrop) > 0)//true == isCling)    // 붙어있는 상태라면 떨어질 수 있게 한다 . 붙은 상태여야 떨어질 수 있음 
                {
                    SetParentNull();
                    variable &= ~(Constants.BV_bCling); //  bCling = false;
                    variable &= ~(Constants.BV_IsBoost);

                    Player_dest = null;       // 목표 물방울이 없어진다 
                    if (dest_script != null)
                        dest_script.Change_CheckState(false); // 타겟빗방울에게 플레이어가 타겟으로 삼지 않음을 알림 
                    dest_script = null;

                    variable &= ~(Constants.BV_ClickRaindrop);// isClickRaindrop = false;
                    variable &= ~(Constants.BV_IsCling);// isCling = false;// state = Constants.ST_FLYING; // 날아가는 상태로 바꿔주자
                                                        //state = Constants.//isCling = false;            // 붙지 않은 상태가 된다 - 다음번에 알아서 
                }

                // 후에 거리 추가해야함!!! - 일단 물방울과 마우스의 충돌체크 
                // 물방울에 붙기위한 충돌 체크 
                else if ((variable & Constants.BV_bCling) == 0)//false == bCling)//bClickRaindrop)   // 물방울이 클릭 되지 않은 상태면서 물방울에 붙은 상태가 아니라면 물방울을 클릭한 상태로 바꿔준다, // RAINDROP 레이어어를 가진 물체와 raycast // 상태변경 
                {
         
                    if (Player_dest == null)
                    {

                        Player_dest = CollisionManager.Instance.Get_MouseCollisionObj(100f, "RAINDROP");
                        if (Player_dest != null)
                        {
                            dest_script = Player_dest.GetComponent<RainDrop>();
                            if(dest_script != null)
                                dest_script.Change_CheckState(true);    // 빗방울에게 플레이어가 타겟으로 삼았음을 알림

                            variable |= Constants.BV_bCling;//bCling = true;
                            variable |= Constants.BV_ClickRaindrop;//isClickRaindrop = true;

                            variable |= Constants.BV_IsBoost;
                        }
                    }
                }
            }
        }
        */
        ///////////////////////////////////////////////// 마우스 왼쪽 뗄 때 


        ///////////////////////////////////////////////// 마우스 오른쪽 클릭
        /*  if (Input.GetMouseButtonDown(1))    // 버튼방식으로 바뀌어야 함 
          {
              //print("마우스 오른쪽 클릭");
              if ((Player_dest = CollisionManager.Instance.Get_MouseCollisionObj(10f)) != null)
               //   || (variable & Constants.BV_bCollisionOthers)>0 )
              // if (CollisionManager.Instance.Check_RayHit(tr.position, tr.forward, "WALL", 3f))  // 벽에 붙을지 체크 
              {
                 // print("마우스 입력해서 들어왔다네 Tag는 :" + Player_dest.gameObject.tag);
                  variable |= Constants.BV_bCling;//bCling = true;

                  //state = Constants.ST_CLING;//|= Constants.ST_CLING;
              }

          }

          */

        ///////////////////////////////////////////////// 마우스 오른쪽 뗄 때

    }
    private void Action()   // 플레이어 모델이 직접 움직이지는 않으나, 속도변경 같은 코드가 들어감. State의 상태는 여기서만 바뀌게 된다.
    {
        if ((variable & Constants.BV_IsInStage) == 0)//state == Constants.ST_IDLE) // Idle 이면 시작전 
            return;

        if ((variable & Constants.BV_IsMove) > 0)
        {
            state = Constants.ST_FLYING;
            //    print(" state = flying");
        }
        else
        {
            state = Constants.ST_IDLE;
            fOwnSpeed = 0f;
            fSpeed = 0f;
            // fSpeed = 0f;
            //   print("state= idle");
        }



        if (Boost())
        {
            //print("boost true");
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
            if (isMovable)
                state = Constants.ST_HOLD;
            else
            {
                state = Constants.ST_CLING;
               // print("ismove해제");
                variable &= ~(Constants.BV_IsMove);     // 붙은상태면 움직이지 않게 함 
            }

            //  if((variable & Constants.BV_IsCling) > 0)

        }

        if ((variable & Constants.BV_bStun) > 0)
            state = Constants.ST_STUN;

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
                        //   print("목표는 없다");
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

            if (((variable & Constants.BV_IsCling) > 0) && !isMovable)  ///////////// Cling 상태라면, 속도를 낮춘다 
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
                //  fBoostVal -= 80f * Time.deltaTime;
                /* if (fBoostSpeed < 1)
                     fBoostSpeed = 1f;
                     */
                if (fStamina > 200)       // 수정필요
                    fStamina = 200;

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
            //   print("1번");
        }
        else if ((state == Constants.ST_STUN)) // 플레이어가 스턴상태이면 중력을 받는 것 처럼 떨어뜨림
        {
            //  print("2번");
            // rigidBody.MovePosition(tr.position + (-Vector3.up * Time.deltaTime));
            rigidBody.velocity = (-Vector3.up * 5f * Time.deltaTime);// tr.position + (-Vector3.up * Time.deltaTime);

        }

        else if ((!(Constants.ST_CLING == state) && !(Constants.ST_BLOOD == state)) || (Constants.ST_CLING == state && isMovable == true)) // 어딘가에 붙어있지 않다면. 일반적인 움직임, Boost || 물건을 들고있는경우 
        {
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


                float _fY = tr.eulerAngles.x + (-fYAngle * Time.deltaTime * fRotSpeed);
                float _fX = tr.eulerAngles.y + (fXAngle * Time.deltaTime * fRotSpeed);

                // print("fYAngle:" + fYAngle);
                // print("tr.eulerAngles.x  : " + tr.eulerAngles.x);
                if ((fYAngle >= 0) && (tr.eulerAngles.x >= 270f) && (_fY < 271f))
                    _fY = 271f;

                if ((fYAngle <= 0) && (tr.eulerAngles.x <= 76f) && (_fY > 75f))
                    _fY = 75f;

                tr.localRotation = Quaternion.Euler(_fY, _fX, 0f);




                // state로 해도 될걸-수정
                if ((variable & Constants.BV_IsMove) > 0)
                {
                    //      print("4번 - 전진!");
                    // 회전 


                    // 움직임
                    movement.Set(tr.forward.x, tr.forward.y, tr.forward.z);
                    rigidBody.velocity = (movement * fSpeed);// * Time.deltaTime);
                }
                else
                {
                    //     print("5번");
                    rigidBody.velocity = Vector3.zero;
                }
            }
        }
        else // 붙어 있을 시 아무 동작도 하지 않도록 함 
        {
         //   print("6번");
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
    public Transform GetParent()
    {
        if (tr.transform.parent)
            return tr.transform.parent;

        return null;

    }
    public void SetParentNull()
    {
        tr.transform.parent = null;
        //  tr.transform.localScale = new Vector3(1, 1, 1);  // ?? 수정?
        ClingObj.transform.parent = null;

    }
    public void SetChildNull()
    {
        if (Player_Target != null)
        {
            isMovable = false;
            Player_Target.transform.parent = null;
            Player_Target.GetComponent<Collider>().enabled = true;
            Player_Target.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    private void SetParent(Transform collTr)
    {
        ClingObj.transform.parent = collTr;
        tr.transform.parent = ClingObj.transform;
    }
    public void SetTargetToChild()
    {
        if (Player_Target != null)
        {
            isMovable = true;
            Player_Target.transform.parent = tr;
            Player_Target.GetComponent<Collider>().enabled = false;
            Player_Target.GetComponent<Rigidbody>().isKinematic = true;
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
        //  if (true == isCanSlow)  // 키입력으로 인해 이미 변수상태가 변경된 상태라면 코루틴 꺼줌
        //   yield return null;
        // 1초 후 시간을 원래대로 바꿔주고, 쿨타임은 2초 
        //  print("1초 후 시간을 원래대로");
        yield return new WaitForSeconds(0.3f); // 변수 지정해줘야함
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
        iHP -= iDamage;
        fOwnSpeed = 0f;
        fBoostSpeed = 0f;
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

            Player_Target = null;       // 목표 물방울이 없어진다 
            dest_script = null;
            variable &= ~(Constants.BV_ClickRaindrop);// isClickRaindrop = false;
        }
        // 빗방울같은 경우, 목표로 한 빗방울이사라진다면? ( 빗방울은 사라질 때 데미지를 호출한다 ) 


        if (iHP < 0)
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
                    print("bvcling");
                    variable |= Constants.BV_IsCling;//isCling = true;


                    if (coll.gameObject.layer == LayerMask.NameToLayer("MOVABLE"))
                    {

                        SetTargetToChild();
                    }
                    else
                    {
                        rigidBody.velocity = Vector3.zero;
                        SetParent(coll.transform);
                    }

                    //text.text = "bv_iscling으로 바꿈";
                }
                else    // 충돌한 물체가 목표물과 다르다면 붙으려고 하는 상태 해제됨
                {
                    print("벨로시티 제로");
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

        if (coll.gameObject.tag == "FROG_TONGUE")
        {
            variable |= Constants.BV_Stick;//isStick = true;
            SetParent(coll.transform);
            rigidBody.isKinematic = true;   // 물리적인 영향을 끔 
            iHP = 0;
        }





        if (coll.gameObject.tag == "Arrive")
        {
            iBlood = 200;



        }

    }

    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject != null)
        {
            variable |= (Constants.BV_bCollisionOthers);   // 충돌했음을 알림
            variable &= ~(Constants.BV_IsBoost);
        }


        if ((variable & Constants.BV_bCling) > 0)//bCling)  // 붙으려고 하는 상태면 
        {//'''''''''''''''''''''''''''''''''''''''''''''
         // print("클링상태");
            if (Player_Target != null)
            {
                //  print("목표물 있고");
                // text.text = "스테이 - 데스트 존재 ";
                if (Player_Target.gameObject == coll.gameObject)    // 충돌한 물체가 목표물과 같다면 달라붙는다 -- 벽이 여기서 에러 
                {
                    variable |= Constants.BV_IsCling;//isCling = true;


                    if (coll.gameObject.layer == LayerMask.NameToLayer("MOVABLE"))
                    {

                        SetTargetToChild();
                    }
                    else
                    {
                        rigidBody.velocity = Vector3.zero;
                        SetParent(coll.transform);
                    }

                    //     text.text = "IsCling 은 트루 ";
                }

            }

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
        //variable &= ~(Constants.BV_IsMove);

        if ((Player_Target = CollisionManager.Instance.Get_RaycastCollisionObj(tr.position, tr.forward, 10f)))//CollisionManager.Instance.Get_MouseCollisionObj(100f)) != null)
        // if (CollisionManager.Instance.Check_RayHit(tr.position, tr.forward, "WALL", 3f))  // 벽에 붙을지 체크 
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
                if (isMovable)
                    SetChildNull();
                else
                    SetParentNull();


            }
            /*tr.transform.parent = null;
            //  tr.transform.localScale = new Vector3(1, 1, 1);  // ?? 이거 꼭 필요한가
            ClingObj.transform.parent = null;*/

            variable &= ~(Constants.BV_bCling);//bCling = false;
            variable &= ~(Constants.BV_IsCling);//isCling = false;
            variable &= ~(Constants.BV_bBlood);
            // state = Constants.ST_FLYING;

        }

    }

    public void SetHP(int _iHP)
    {
        iHP = _iHP;
    }

    public void MoveHorizontal(Vector3 _vDest)
    {
        rigidBody.MovePosition(Vector3.Lerp(tr.position, _vDest, 0.2f));

        if ((tr.position.x > (_vDest.x - 0.1f)) && (tr.position.x < (_vDest.x + 0.1f)))
        {
            tr.position = _vDest;
        }
        //print("( tr.position.x  > (_vDest.x - float.Epsilon) )"  + (tr.position.x > (_vDest.x - float.Epsilon)));
        // print(" ( tr.position.x  < (_vDest.x + float.Epsilon) )" + (tr.position.x < (_vDest.x + float.Epsilon)));

        //rigidBody.velocity = (tr.right * _fSpeed);
        // print("pos  : " + tr.position);
    }


}