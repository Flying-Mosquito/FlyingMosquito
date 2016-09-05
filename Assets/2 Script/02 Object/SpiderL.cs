using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpiderL : MonoBehaviour
{
    public Image img;
    public UnityEngine.UI.Button btn;
    public float cooltime = 60.0f;
    public bool disableOnStart = true;
    float leftTime = 60.0f;
    public bool[] check = new bool[3];
    public PlayerCtrl player;
    public Transform b1;
    public Transform b2;
    public Transform b3;
    public SpiderCtrl cow;
    void Start()
    {
        cow  = GameObject.FindObjectOfType<SpiderCtrl>();
        player = GameObject.FindObjectOfType<PlayerCtrl>();
        check[0] = false;
        check[1] = false;
        check[2] = false;
        if (img == null)
            img = gameObject.GetComponent<Image>();
        if (btn == null)
            btn = gameObject.GetComponent<UnityEngine.UI.Button>();
        if (disableOnStart)
            ResetCooltime();
    }

    void Update()
    {
      
        
        if (check[0]==true && check[1] == true&& check[0] == true)
        {
            this.transform.gameObject.SetActive(false);
            player.SetParentNull();
            player.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        if (leftTime > 0)
            
        {
            leftTime -= Time.deltaTime*3*Random.value*10;
            if (leftTime < 0)
            {
                leftTime = 0;
                if (btn)
                    btn.enabled = true;
            }
            float ratio = 1.0f - (leftTime / cooltime);
            if (img)
                img.fillAmount = ratio;
        }
    }
    public bool CheckCooltime()
    {
        if (leftTime > 0)
            return false;
        else return true;
    }
    public void ResetCooltime()
    {
        leftTime = cooltime;
        if (btn)
            btn.enabled = false;
    }
    public void CheckClick1()
    {
        check[0] =true;
        b1.gameObject.SetActive(false);
    }
    public void CheckClick2()
    {
        check[1] = true;
        b2.gameObject.SetActive(false);
    }
    public void CheckClick3()
    {
        check[2] = true;
        b3.gameObject.SetActive(false);
    }
    void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag == "human")
        {
            this.transform.gameObject.SetActive(false);


        }
        if (coll.gameObject.tag == "PLAYER")
        {
            if ((player.variable & Constants.BV_Stick) > 0)
                return;
            b1.gameObject.SetActive(true);
            b2.gameObject.SetActive(true);
            b3.gameObject.SetActive(true);
            

            player.variable |= Constants.BV_Stick;
           
           
           
        }
    }
}