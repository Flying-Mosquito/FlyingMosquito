using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    /*
   
    */
    //public static bool isFadeOut;
    // public static string strSceneName;
    public Image alphaImage;
    public float fAlpha;
    public float fAlphaSpeed;
    public bool bSceneChange;   // 페이드 아웃 후 씬을 바꿀 수 있는지   
    public bool isLoadingDone;  // 로딩 완료 후 씬을 바꿀 수 있는지
    public bool isFadeIn; // 밝아지는 상태인지(이건 업데이트에서 돌릴거라)
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
        isFadeIn = false;
        //  Anim = GetComponent<Animator>();
        // isFadeOut = false;
        //strSceneName = null;
        FadeIn();
        fAlpha = 0f;
        fAlphaSpeed = 0.02f;

        // Init_Singleton();
    }
    void Update()
    {
        if (isFadeIn)
        {
            FadeIn();
        }
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
        }*/
    }
    
    public IEnumerator FadeOut()  // 장면이 어두워진다
    {
        //Anim.SetBool("isFadeOut", true);
        if (alphaImage == null)
            alphaImage = GameObject.Find("BlackBackground").GetComponent<Image>();

        print("FadeOut");
        alphaImage.color = new Color(alphaImage.color.r, alphaImage.color.g, alphaImage.color.b, 0f);
        while (true)
        {
            fAlpha += Time.timeScale * fAlphaSpeed;
            alphaImage.color = new Color(alphaImage.color.r, alphaImage.color.g, alphaImage.color.b, fAlpha);

            if (fAlpha >= 1f)
            {
                bSceneChange = true;
                break;
            }
            else
                yield return null;

        }
        
    }
    public void SetBackgroundAlphaColor(float _a)
    {
        if(alphaImage == null)
            alphaImage = GameObject.Find("BlackBackground").GetComponent<Image>();

        alphaImage.color = new Color(alphaImage.color.r, alphaImage.color.g, alphaImage.color.b, _a);
    }
    private void FadeIn()   // 장면이 밝아진다
    {
        if (alphaImage == null)
        {
            alphaImage = GameObject.Find("BlackBackground").GetComponent<Image>();
        }

        //alphaImage.color = new Color(alphaImage.color.r, alphaImage.color.g, alphaImage.color.b, 1f);
       // while (true)
        //{
            //  SetWaitForLoadingSeconds(1.5f);
            // StartCoroutine("StartLoad", strSceneName);
            fAlpha -= Time.fixedDeltaTime * fAlphaSpeed * 100;
            alphaImage.color = new Color(alphaImage.color.r, alphaImage.color.g, alphaImage.color.b, fAlpha);
            if (fAlpha <= 0f)
            {
                isFadeIn = false;
               // bSceneChange = true;
           //     break;
            //}
            //else
              //  yield return null;

        }
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

                if (bSceneChange == true)//time >= waitForLoadingSeconds)  // waitForLoadingSeconds는 현재 2으로 설정해 놓았다.
                {
                    //isLoadGame = false;
                    bSceneChange = false;
                    async.allowSceneActivation = true;  // 2초 후에 씬을 넘김 
                    //UIManager.isFadeOut = false;
                    bSceneChange = false;
                    time = 0f;
                }
                yield return new WaitForFixedUpdate();
            }

            if(alphaImage == null)
                alphaImage = alphaImage = GameObject.Find("BlackBackground").GetComponent<Image>();
            alphaImage.color = new Color(alphaImage.color.r, alphaImage.color.g, alphaImage.color.b, 0f);

            async = SceneManager.LoadSceneAsync(strSceneName);
            async.allowSceneActivation = false;

            while (!async.isDone)
            {
                if (isLoadingDone)
                {
                    isLoadGame = false;
                    bSceneChange = false;
                    async.allowSceneActivation = true;
                    // bSceneChange = false;

                    //if (alphaImage == null)
                    //  alphaImage = alphaImage = GameObject.Find("BlackBackground").GetComponent<Image>();
                    // alphaImage.color = new Color(alphaImage.color.r, alphaImage.color.g, alphaImage.color.b, 1f);
                }
                yield return new WaitForFixedUpdate();
            }

            if (alphaImage == null)
                alphaImage = alphaImage = GameObject.Find("BlackBackground").GetComponent<Image>();
            alphaImage.color = new Color(alphaImage.color.r, alphaImage.color.g, alphaImage.color.b, 1f);

            isFadeIn = true;
            //StartCoroutine("FadeIn");
        }
        isLoadingDone = false;
    }
    public IEnumerator StartLoad(string strSceneName)
    {
        /*if ( "00 Logo" == Application.loadedLevelName )
        {
          
        }
        */
        //Application.loadedLevel;
 
        if (isLoadGame == false)
        {
            isLoadGame = true;

            AsyncOperation async = SceneManager.LoadSceneAsync(strSceneName);//Application.LoadLevelAsync(strSceneName);


            async.allowSceneActivation = false; // 씬을 로딩후 자동으로 넘어가지 못하게 한다.

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

    public IEnumerator StartReload()
    {
        /*if ( "00 Logo" == Application.loadedLevelName )
        {
          
        }
        */
        //Application.loadedLevel;
        string strSceneName = SceneManager.GetActiveScene().name;

        if (isLoadGame == false)
        {
            isLoadGame = true;

            AsyncOperation async = SceneManager.LoadSceneAsync(strSceneName);//Application.LoadLevelAsync(strSceneName);

            async.allowSceneActivation = false; // 씬을 로딩후 자동으로 넘어가지 못하게 한다.

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
                yield return null;//new WaitForFixedUpdate();
            }
        }
    }

    public void SetWaitForLoadingSeconds(float _fTime)
    {
        waitForLoadingSeconds = _fTime;
    }





}

