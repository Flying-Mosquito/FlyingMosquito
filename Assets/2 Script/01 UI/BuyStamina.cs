using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BuyStamina : MonoBehaviour {
    
    public Text Stamina;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Stamina.text = ((UI.Instance.plusSt * 10) + 200).ToString();
    }
}
