using UnityEngine;
using UnityEngine.SceneManagement;


public class ShortSceneChange : MonoBehaviour
{
    public void NextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void ChangeScene(int index) => SceneManager.LoadScene(index);
}
