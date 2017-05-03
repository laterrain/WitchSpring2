using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScenes : MonoBehaviour 
{//异步加载场景
    public Slider loadingBar;//加载时显示的进度条
    public Text loadingProgress;//加载时进度条显示的百分比
    AsyncOperation op;//异步加载类
	// Use this for initialization
	void Start () 
    {//开启携程加载场景
        StartCoroutine(LoadNormalScene("LunaHome", 0));
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {//当点击鼠标左键的时候才允许跳转场景
            op.allowSceneActivation = true;
        }
	}
    IEnumerator LoadNormalScene(string sceneName, float startPercent = 0)
    {
        //GameRoot.Instance.CurrentSceneId = (int)LoadingWindow.nextSceneID;        

        //loadingText.text = "加载场景中...";  
        
        int startProgress = (int)(startPercent * 100);//开始加载场景时的百分比
        int displayProgress = startProgress;//未加载场景的百分比
        int toProgress = startProgress;//已经加载场景的百分比
        op = SceneManager.LoadSceneAsync(sceneName);
        //AsyncOperation op = Application.LoadLevelAsync(sceneName);
        op.allowSceneActivation = false;//不允许跳转场景
        while (op.progress < 0.9f)
        {//progress最大只能加载到0.9
            toProgress = startProgress + (int)(op.progress * (1.0f - startPercent) * 100);
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetProgress(displayProgress);
                yield return null;
            }
            yield return null;
        }

        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetProgress(displayProgress);
            yield return null;
        }
                
    }
    void SetProgress(int progress)
    {//进度条和百分比的显示
        loadingBar.value = progress * 0.01f;
        loadingProgress.text = progress.ToString() + " %";
    }  
}
