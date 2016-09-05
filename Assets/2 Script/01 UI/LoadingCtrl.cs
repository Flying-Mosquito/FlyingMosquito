using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingCtrl : MonoBehaviour
{
    public Image LoadingImage;
    public float fLoadingSpeed = 0.015f;

    void Start()
    {
        LoadingImage.fillAmount = 0f;
    }


	// Update is called once per frame
	void Update ()
    {
        LoadingImage.fillAmount += Time.timeScale * fLoadingSpeed;
        if (LoadingImage.fillAmount > 1)
            LoadingImage.fillAmount = 1f;

        if (LoadingImage.fillAmount == 1f)
            GameManager.Instance.isLoadingDone = true;
        

	}
}
