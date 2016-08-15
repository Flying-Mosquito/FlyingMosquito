using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class CameraEffect : MonoBehaviour
{

    private bool isCollisionOthers = false;
    private CamPivot ParentCamp = null;
    public PlayerCtrl player = null;
    DepthOfField dof;
    Component fx_speedLight;

    void Start()
    {
        //ParentTr = transform.parent;
        //   ParentComp = GameObject.Find("CamPivot");//PlayerCtrl.Instance.GetComponent<FollowCam>();
       // ParentCamp = PlayerCtrl.Instance.GetComponentInChildren<CamPivot>();
        dof = transform.GetComponent<DepthOfField>();
        fx_speedLight = GetComponentInChildren<ParticleSystem>();
        fx_speedLight.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        isCollisionOthers = false;
    }

    void Update()
    {
        if (player && ParentCamp)
        {
            //print("maxBlurSize = " + dof.maxBlurSize);
            if (player.fSpeed > 8f)
            {
                print("스피드 8이상");
                fx_speedLight.gameObject.SetActive(true);
                //transform.GetComponent<DepthTextureMode>();
                //dof.enabled = true;
                //dof.maxBlurSize = 0.52f;
                //  dof.maxBlurSize += 5 * Time.deltaTime;
                // print("maxBlurSize = " + dof.maxBlurSize);
            }
            else if (player.state == Constants.ST_STUN)
            {
                   print("스턴");
                fx_speedLight.gameObject.SetActive(false);
                dof.enabled = true;
                dof.maxBlurSize = 10f;
            }

            else
            {
                 print("히");
                fx_speedLight.gameObject.SetActive(false);
                dof.enabled = false;


                dof.maxBlurSize = Mathf.Lerp(dof.maxBlurSize, 0f, 0.008f);
            }
        }
        
    }

    void OnTriggerStay(Collider coll)
    {

        if (isCollisionOthers == false) // 한번만 보내준다 
        {
         //   ParentCamp.SetIsCollisionOthers(true);

       //     isCollisionOthers = true;
        }

    }

    void OnTriggerExit(Collider coll)
    {
      //  ParentCamp.SetIsCollisionOthers(false);
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player.GetComponent<PlayerCtrl>();
    }
    
    public void SetParentCamp()
    {
        if(player)
           ParentCamp = player.GetComponentInChildren<CamPivot>();
    }
}
