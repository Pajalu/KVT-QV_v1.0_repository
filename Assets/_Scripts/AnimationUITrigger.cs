using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUITrigger : MonoBehaviour
{
    public void NoTool()
    {

    }

    public void Tools(toolSelect t)
    {
        switch (t)
        {
            case toolSelect.AllenKey:
                break;
        }
    }
    public enum toolSelect
    {
        AllenKey,
        GripTongs,
        Wrench
    }

    public bool Video
    {
        set 
        {
            if (true)
            {

            }
        }
    }
}
