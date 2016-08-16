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
        Timer.Instance.gameover.gameObject.SetActive(false);
        Timer.Instance.gameClear.gameObject.SetActive(false);

        SceneManager.LoadScene(1);
        //Text.text = "OnTouchEnd";
    }
}
