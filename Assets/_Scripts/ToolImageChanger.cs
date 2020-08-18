using UnityEngine;
using UnityEngine.UI;

public class ToolImageChanger : MonoBehaviour
{
    private Image img;
    private Text txt;
    public GameObject videoBtn;
    public Sprite imbus, gripzange, gabelschlüssel, hebeisen, pinsel, druckluft;
    private int step;
    private int maxSteps = 6;


    void Awake()
    {
        img = GetComponent<Image>();//.sprite;
        txt = GetComponentInChildren<Text>();
    }

    private void Refresh()
    {
        if (maxSteps == 6) //Mit Flansch Ausbau
        {
            switch (step)
            {
                default:
                    NoTool();
                    videoBtn.SetActive(false);
                    break;
                case 1:
                    Tool("Allen Key", imbus);
                    break;
                case 4:
                    Tool("Allen Key", imbus);
                    break;
                case 6:
                    Tool("Grip Tongs", gripzange);
                    break;
            }
        }
        if (maxSteps == 9)   //Ohne Flansch Ausbau
        {
            switch (step)
            {
                default:
                    NoTool();
                    videoBtn.SetActive(false);
                    break;
                case 6:
                    Tool("Wrench", gabelschlüssel);
                    break;
                case 8:
                    Tool("Wrench", gabelschlüssel);
                    break;
            }
        }
        if (maxSteps == 13)  //Mit Flansch Einbau
        {
            switch (step)
            {
                default:
                    NoTool();
                    break;
                case 3:
                    Tool("Allen Key", imbus);
                    break;
                case 4:
                    Tool("H20 + Soap", pinsel);
                    break;
                case 6:
                    Tool("Allen Key", imbus);
                    break;
                case 7:
                    Tool("H20 + Soap", pinsel);
                    break;
                case 8:
                    Tool("Allen Key", imbus);
                    videoBtn.SetActive(false);
                    break;
                case 9:
                    Tool("Mounting Iron", hebeisen);
                    videoBtn.SetActive(true);
                    break;
                case 10:
                    Tool("Compressed Air (3 bar)", druckluft);
                    videoBtn.SetActive(false);
                    break;
                case 11:
                    Tool("Allen Key", imbus);
                    break;
                case 12:
                    Tool("Compressed Air (3 bar)", druckluft);
                    break;
            }
        }
        if (maxSteps == 15)  //Ohne Flansch Einbau
        {
            switch (step)
            {
                default:
                    NoTool();
                    break;
                case 7:
                    Tool("H20 + Soap", pinsel);
                    break;
                case 8:
                    Tool("Wrench", gabelschlüssel);
                    videoBtn.SetActive(false);
                    break;
                case 9:
                    Tool("Mounting Iron", hebeisen);
                    videoBtn.SetActive(true);
                    break;
                case 10:
                    Tool("Compressed Air (3 bar)", druckluft);
                    videoBtn.SetActive(false);
                    break;
            }
        }
    }
    private void Tool(string t, Sprite s)   //NEU
    {
        img.enabled = true;
        img.sprite = s;
        txt.text = t;
    }
    public void NoTool()   //NEU
    {
        img.enabled = false;
        txt.text = "";
    }

    public int Step
    {
        set { step = value; Refresh(); }
    }
    public int MaximumSteps     //NEU
    {
        set { maxSteps = value; step = 0; Refresh(); }
    }
}
