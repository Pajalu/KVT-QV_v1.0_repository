using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Button btn;
    public GameObject okay;
    public GameObject loadingTxt;
    public GameObject slideMenu;
    public AsyncOperation operation;
    public StaticValues staticValues;
    public int qv;
    public int scenario;
    private bool demonstrator = false;

    private void OnEnable()
    {
        if (demonstrator)
        {
            LoadLevel("DemonstratorVR");
        }
        else
        {
            qv = staticValues.QV;
            scenario = staticValues.Scenario;

            LoadLevel("QV" + qv + "-" + scenario);
        }     
    }

    IEnumerator LoadAsynchronously (string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;    
        yield return new WaitUntil(() => operation.progress == 0.9f);
        btn.enabled = true;
        okay.SetActive(true);       
    }
    public void OKload()
    {
        okay.SetActive(false);
        slideMenu.SetActive(false);
        loadingTxt.SetActive(true);
        operation.allowSceneActivation = true;
    }

    public bool Demonstrator
    {
        set { demonstrator = value; }
    }

    public void LoadLevel(string sceneName) => StartCoroutine(LoadAsynchronously(sceneName));

    public void NextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void ChangeScene(int index) => SceneManager.LoadScene(index);
}
