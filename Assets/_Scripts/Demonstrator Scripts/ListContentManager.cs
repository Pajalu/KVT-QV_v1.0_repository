using UnityEngine;
using UnityEngine.UI;

public class ListContentManager : MonoBehaviour
{
    public GameObject spacer;
    public GameObject UI;
    public Transform valve;
    public ToolImageChanger toolBtn;
    public Text title;
    //public Font font;
    public CameraTouchControl camTouch;
    public GameObject progressBar; //evtl anders

    public string[] qvName;
    public GameObject[] qvAusbau;
    public GameObject[] qvEinbau;

    private int currentElement = -1;

    private void CreateElements() //MOMENTAN NICHT BENÖTIGT
    {
        /*
        for (int i = 0; i < qvName.Length; i++)
        {
            var e = new GameObject();
            e.transform.parent = transform;
            e.name = qvName[i] + " (" + i + ")";
            var img = e.AddComponent<Image>();
            var btn = e.AddComponent<Button>();            
            var script = e.AddComponent<ListElement>();
            script.qvInList = i;
            btn.onClick.AddListener(() => { script.ElementPressed(); });
            var t = new GameObject();
            t.transform.parent = e.transform;
            t.name = qvName[i]+"_txt";
            var txt = t.AddComponent<Text>();
            txt.text = qvName[i];
            txt.color = Color.gray;
            txt.font = font;
        }
        */
    }

    public void ElementPressed(int element)
    {
        if(currentElement == element)
        {
            spacer.SetActive(false);
            currentElement = -1;
        }
        else
        {
            currentElement = element;
            spacer.SetActive(true);
            spacer.transform.SetSiblingIndex(element + 1);
        }        
    }

    public void LoadDisassemble()
    {
        foreach (Transform child in valve)
        {
            Destroy(child.gameObject);
        }
        if (currentElement != -1)
        {
            GameObject qv = Instantiate(qvAusbau[currentElement], valve);
            var cs = qv.GetComponent<CurrentStep>();
            if (cs != null)
            {
                cs.enabled = false;
            }
            foreach (Outline o in qv.GetComponentsInChildren<Outline>())
            {
                o.enabled = false;
            }

            title.text = "Disassemble";
            toolBtn.MaximumSteps = qv.GetComponent<MetadataQV>().maxSteps;
            camTouch.ReFocus();
            progressBar.GetComponent<ProgressBar>().NumOfEl = qv.GetComponent<MetadataQV>().maxSteps;
            UI.GetComponent<Dem_UI_CurrentStep>().ResetUI();
        }
    }

    public void LoadAssemble()
    {
        foreach (Transform child in valve)
        {
            Destroy(child.gameObject);
        }
        if (currentElement != -1)
        {
            GameObject qv = Instantiate(qvEinbau[currentElement], valve);
            var cs = qv.GetComponent<CurrentStep>();
            if (cs != null)
            {
                cs.enabled = false;
            }                
            foreach (Outline o in qv.GetComponentsInChildren<Outline>())
            {
                o.enabled = false;
            }

            title.text = "Assemble";
            toolBtn.MaximumSteps = qv.GetComponent<MetadataQV>().maxSteps;
            camTouch.ReFocus();
            progressBar.GetComponent<ProgressBar>().NumOfEl = qv.GetComponent<MetadataQV>().maxSteps;
            UI.GetComponent<Dem_UI_CurrentStep>().ResetUI();
        }
    }
}
