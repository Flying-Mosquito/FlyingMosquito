using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReturnBtCtrl : BaseButton {

    public override void OnTouchEnd(Vector2 _pos)
    {
        isMouseDown = false;

        //playerctrl.iBlood = 0;
        //playerctrl.SetParentNull();
        //playerctrl.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        

        SceneManager.LoadScene(1);
        //Text.text = "OnTouchEnd";
    }
}
