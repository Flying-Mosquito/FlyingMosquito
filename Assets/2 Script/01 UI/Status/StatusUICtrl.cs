using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusUICtrl : MonoBehaviour {

    private float MAXHP;
    public Image hpImage;

    void Start()
    {
        MAXHP = 100f;
        hpImage = GameObject.Find("Heart").GetComponent<Image>();
    }

    public void SetPlayerHP(float _fHp)
    {
        print("UICtrl - 데미지를받다 ! 현재 hp : " + _fHp);
        hpImage.fillAmount = _fHp / MAXHP;
    }

}
