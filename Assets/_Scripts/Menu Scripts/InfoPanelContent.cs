using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class InfoPanelContent : MonoBehaviour
{
    public string text1, text2;
    public VideoClip clip1, clip2;

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                gameObject.SetActive(false);
                //return;
            }
        }
    }

    public int Content
    {
        set
        {
            if(value == 1)
            {
                GetComponentInChildren<Text>().text = text1;
                GetComponentInChildren<VideoPlayer>().clip = clip1;
            }
            if (value == 2)
            {
                GetComponentInChildren<Text>().text = text2;
                GetComponentInChildren<VideoPlayer>().clip = clip2;
            }
        }
    }


}
