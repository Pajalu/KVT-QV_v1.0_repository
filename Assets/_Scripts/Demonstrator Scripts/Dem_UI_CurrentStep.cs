using UnityEngine;
using UnityEngine.UI;

public class Dem_UI_CurrentStep : MonoBehaviour
{
    private int step = 0;
    private int maxStep = 6; //VORÜBERGEHEND
    private GameObject valve;

    private ProgressBar progressBar;
    private ToolImageChanger toolbutton;
    public Text finishedTxt;
    public Image prevButton;
    public Image nextBtn;

    void Start()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.fullScreen = true;

        valve = GameObject.FindWithTag("Ventil");
        progressBar = transform.GetComponentInChildren<ProgressBar>();
        toolbutton = transform.GetComponentInChildren<ToolImageChanger>();
        progressBar.NumOfEl = valve.GetComponent<MetadataQV>().maxSteps;

        Refresh();
    }

    private void Refresh()
    { 
        progressBar.Step = step;
        maxStep = progressBar.NumOfEl;
        toolbutton.Step = step;

        if (step == 0)
        {
            prevButton.enabled = false;
        }
        else
        {
            prevButton.enabled = true;
        }

        if(step == maxStep +1)
        {
            nextBtn.enabled = false;
            finishedTxt.enabled = true;
        }
        else
        {
            nextBtn.enabled = true;
            finishedTxt.enabled = false;
        }
    }

    public void ResetUI()
    {
        step = 0;
        Refresh();
    }

    public void IncreaseStep()
    {
        if (step < maxStep)
        {
            step++;
            Refresh();
        }
        else if (step == maxStep)
        {
            step++;
            nextBtn.enabled = false;
            finishedTxt.enabled = true;
            toolbutton.NoTool();
        }
    }

    public void DecreaseStep()
    {
        if (step > 0)
        {
            step--;
            Refresh();
        }
    }
}
