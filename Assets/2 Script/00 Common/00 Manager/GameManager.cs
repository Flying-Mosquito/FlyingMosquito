using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    /*
   
    */
    public static bool isFadeOut;
    public static string strSceneName;
    public float fAlpha;
    public float fAlphaSpeed;
    public bool bSceneChange;   // 페이드 아웃 후 씬을 바꿀 수 있는지    // ??필요 없을 것 같은데?
    public bool isLoadingDone;  // 로딩 완료 후 씬을 바꿀 수 있는지
  //  private Animator Anim;

    public float waitForLoadingSeconds;
    private bool isLoadGame = false;                                    // loding drag
    private float time;
    

    // public Image fadeImage;
    void Awake()
    {
        DontDestroyOnLoad(this);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        bSceneChange = false;
        isLoadingDone = false;
        //  Anim = GetComponent<Animator>();
        isFadeOut = false;
        strSceneName = null;
        FadeIn();
        fAlpha = 0f;
        fAlphaSpeed = 1f;

        // Init_Singleton();
    }
   /* void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
        }
    }
    */
    public IEnumerator FadeOut()  // 장면이 어두워진다
    {
        //Anim.SetBool("isFadeOut", true);
        while (true)
        {
            fAlpha += Time.timeScale * fAlphaSpeed;

            if (fAlpha > 1f)
            {
                bSceneChange = true;
                break;
            }
            else
                yield return null;

        }
        
    }

    private IEnumerator FadeIn()   // 장면이 밝아진다
    {
        while (true)
        {
            //  SetWaitForLoadingSeconds(1.5f);
            // StartCoroutine("StartLoad", strSceneName);
            fAlpha -= Time.timeScale * fAlphaSpeed;
            if (fAlpha < 0f)
            {
                bSceneChange = true;
                break;
            }
            else
                yield return null;

        }
    }

    private void Scene_change()
    {

    }

    public IEnumerator StartSceneLoadWithLoading(string strSceneName)   // 로딩씬 불러옴 
    {

        if (isLoadGame == false)
        {
            isLoadGame = true;

            AsyncOperation async = SceneManager.LoadSceneAsync("Loading");//Application.LoadLevelAsync(strSceneName);


            async.allowSceneActivation = false; // 씬을 로딩후 자동으로 넘어가지 못하게 한다.

            StartCoroutine("FadeOut");

            while (!async.isDone)
            {
                time += Time.deltaTime;

                if (time >= waitForLoadingSeconds)  // waitForLoadingSeconds는 현재 2으로 설정해 놓았다.
                {
                    //isLoadGame = false;
                    async.allowSceneActivation = true;  // 2초 후에 씬을 넘김 
                    //UIManager.isFadeOut = false;
                    bSceneChange = false;
                    time = 0f;
                }
                yield return new WaitForFixedUpdate();
            }


            async = SceneManager.LoadSceneAsync(strSceneName);
            async.allowSceneActivation = false;
            StartCoroutine("FadeIn");

            while (!async.isDone)
            {
                if (isLoadingDone)
                {
                    isLoadGame = false;
                    async.allowSceneActivation = true;
                    bSceneChange = false;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
    public IEnumerator StartLoad(string strSceneName)
    {
        /*if ( "00 Logo" == Application.loadedLevelName )
        {
          
        }
        */

        if (isLoadGame == false)
        {
            isLoadGame = true;

            AsyncOperation async = SceneManager.LoadSceneAsync(strSceneName);//Application.LoadLevelAsync(strSceneName);


            async.allowSceneActivation = false; // 씬을 로딩후 자동으로 넘어가지 못하게 한다.

            StartCoroutine("FadeOut"); 

            while (!async.isDone)
            {
                time += Time.deltaTime;

                if (time >= waitForLoadingSeconds)  // waitForLoadingSeconds는 현재 2으로 설정해 놓았다.
                {
                    isLoadGame = false;
                    async.allowSceneActivation = true;  // 2초 후에 씬을 넘김 
                    //UIManager.isFadeOut = false;
                    bSceneChange = false;
                    time = 0f;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public void SetWaitForLoadingSeconds(float _fTime)
    {
        waitForLoadingSeconds = _fTime;
    }





}

