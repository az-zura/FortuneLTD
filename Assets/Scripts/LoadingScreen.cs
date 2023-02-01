using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject loadingScreen;

    private void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        
        loadingScreen.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        LoadScene(SceneManager.GetSceneByName(sceneName).buildIndex);
    }
    
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneIndex);

        while (!ao.isDone)
        {
            float progress = Mathf.Clamp01(ao.progress / .9f);
            slider.value = progress;
            
            yield return null;
        }
        
        loadingScreen.SetActive(false);
    }
}
