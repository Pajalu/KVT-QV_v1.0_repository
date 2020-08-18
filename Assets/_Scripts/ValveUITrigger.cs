using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveUITrigger : MonoBehaviour
{
    private bool video;
    private int tool;
    // 0 = None
    //1 = AllenKey
    //2 = GripTongs
    //3 = Wrench
    

    public int Tool
    {
        get { return tool; }
        set { tool = value; }
    }

    public bool Video
    {
        get { return video; }
        set { video = value; }
    }
}
