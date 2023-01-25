using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
public class Preloader : Singleton<Preloader>
{
    [SerializeField]
    private GameObject preloaderCanvas;
    [SerializeField]
    private Slider progressBar;
    [SerializeField]
    private TMP_Text loadingText;
    private string startLoadingText;
    public Action onLevelLoadStart;
    public Action<string> onLevelLoadFinish;
    private void Awake() {
        base.SetAsCrossScene();
        startLoadingText = loadingText.text; 
       
    }
    public void LoadLevel(string levelName, bool addBackAction = true)
    {
        onLevelLoadStart?.Invoke();
        if(addBackAction)
        {
            string thisSceneName = SceneManager.GetActiveScene().name;
            HistoryManager.Instance.AddBackAction(delegate{BackToPrevScene(thisSceneName);});
        }
        preloaderCanvas.SetActive(true);
        StartCoroutine(LoadScene(levelName));
    }
    void BackToPrevScene(string sceneName)
    {
        //Reset timeScale if there was pause
        Time.timeScale = 1;
        LoadLevel(sceneName, false);
    }
    IEnumerator LoadScene(string levelName)
    {
        //maybe add curtains close
        
        float t = 0;
        float dotsCount = 0;
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName);
        //When the load is still in progress, output the progress bar value
        while (!asyncOperation.isDone)
        {
            t += Time.deltaTime;
            if(t >= 0.5f)
            {   
                if(dotsCount <3)
                {
                    dotsCount++;
                    loadingText.text += ".";
                }
                else
                {
                    dotsCount--;
                    loadingText.text.Remove(startLoadingText.Length,3);
                }
                t = 0;
            }

            //Output the current progress
            progressBar.value = asyncOperation.progress;
            yield return null;
        }

        //maybe add curtains open
        loadingText.text = startLoadingText;
        preloaderCanvas.SetActive(false);
        onLevelLoadFinish?.Invoke(levelName);
    }  
}
