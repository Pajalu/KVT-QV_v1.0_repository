using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListElement : MonoBehaviour
{
    public int qvInList;
    private ListContentManager contentManager;
    //private bool selected = false;

    // Start is called before the first frame update
    void Start()
    {
        contentManager = transform.GetComponentInParent<ListContentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ElementPressed()
    {
        /*
        selected = !selected;
        if(selected)
        {
            spacer.gameObject.SetActive(true);
            spacer.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
        }
        */
        contentManager.ElementPressed(qvInList);
    }

    public void DeSelect()
    {
        /*
        selected = false;
        spacer.gameObject.SetActive(false);
        */
    }
}
