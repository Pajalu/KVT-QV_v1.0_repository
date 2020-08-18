using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject currentTarget;
    private int step;
    public UI_Manager ui_manager;

    private void Refresh()
    {
        if (currentTarget != null)
        {
            currentTarget.GetComponentInChildren<Animator>().SetInteger("Step", step);
        }
    }

    public void TargetFound(GameObject target)
    {
        currentTarget = target;
        step = currentTarget.GetComponentInChildren<Animator>().GetInteger("Step");
        ui_manager.Tracking = true;
        ui_manager.Step = step;
    }

    public void TargetLost()
    {
        currentTarget = null;
        ui_manager.Tracking = false;
    }

    public int Step
    {
        get { return step; }
        set { step = value; Refresh(); }
    }
}
