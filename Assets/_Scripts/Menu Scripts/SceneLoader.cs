using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AsyncOperation operation;
    public Image loadingCircle;
    //public Animator animator;

    void Start()
    {
        loadingCircle.fillAmount = 0;
        LoadMenuAsync();
    }

    IEnumerator LoadAppOnStart()
    {
        operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        
        while(operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingCircle.fillAmount = progress;
            yield return null;
        }
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName);
        OKload = false;
        yield return new WaitUntil(() => operation.progress == 0.9f);
    }

    public bool OKload
    {
        set { operation.allowSceneActivation = value; }       
    }

    public void LoadSceneAsync(string sceneName) => StartCoroutine(LoadAsynchronously(sceneName));

    public void LoadMenuAsync()
    {
        if (loadingCircle != null)
        {
            StartCoroutine(LoadAppOnStart());
        }
    }

    public void NextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void ChangeScene(int index) => SceneManager.LoadScene(index);
}
