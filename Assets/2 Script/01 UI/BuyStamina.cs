using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BuyStamina : MonoBehaviour {
    
    public Text Stamina;
    public Text MyG;
	// Use this for initialization
	void Start () {
        UI.Instance.MyGold = UI.Instance.Tscore;

    }
	
	// Update is called once per frame
	void Update () {
      
        Stamina.text = ((UI.Instance.plusSt * 10) + 200).ToString();
        MyG.text = UI.Instance.Tscore.ToString();
    }
  public void buyS()
    {
        if (UI.Instance.Tscore > 50)
        {
            UI.Instance.plusSt++;
            UI.Instance.Tscore -= 50;
        }
    }
}
