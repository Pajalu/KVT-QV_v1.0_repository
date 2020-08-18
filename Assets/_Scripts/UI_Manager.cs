using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private int step;
    public TargetManager targetManager;
    public ProgressBar progressBar;
    private bool tracking;

    public GameObject nextBtn, prevBtn, loadBtn, toolBtn, videoBtn;

    // Start is called before the first frame update
    void Start()
    {
        //toolbutton = transform.GetComponentInChildren<ToolImageChanger>();
       

        Refresh();  
    }

    private void Refresh()
    {
        targetManager.Step = step;
        if (tracking)
        {
            int currentTool = targetManager.currentTarget.GetComponentInChildren<ValveUITrigger>().Tool;
            bool showVideoBtn = targetManager.currentTarget.GetComponentInChildren<ValveUITrigger>().Video;
            progressBar.NumOfEl = targetManager.currentTarget.GetComponentInChildren<MetadataQV>().maxSteps;
            progressBar.Step = step;

            nextBtn.SetActive(true);
            if(step > 0)
            {
                prevBtn.SetActive(true);
            }
            else
            {
                prevBtn.SetActive(false);
            }            
            toolBtn.SetActive(true);
            loadBtn.SetActive(false);

            switch (currentTool)
            {
                case 0:
                    toolBtn.GetComponent<Image>().enabled = false;
                    break;
                case 1:

                    break;
            }

            if (showVideoBtn)
            {
                videoBtn.SetActive(true);
            }
            else
            {
                videoBtn.SetActive(false);
            }
        }
        else if (!tracking)
        {
            nextBtn.SetActive(false);
            prevBtn.SetActive(false);
            toolBtn.SetActive(false);
            videoBtn.SetActive(false);
            loadBtn.SetActive(true);
        }



    }

    public int Step
    {
        get { return step; }
        set { step = value; Refresh(); }
    }

    public bool Tracking
    {
        set { tracking = value; Refresh(); }
    }

    public void IncreaseStep()
    {
        int maxSteps = targetManager.currentTarget.GetComponentInChildren<MetadataQV>().maxSteps;
        if (step <= maxSteps)
        {
            Step++;
        }
    }

    public void DecreaseStep()
    {
        if (step > 0)
        {
            Step--;
        }
    }
}
