using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private int numberOfElements;
    private int step;
    public Text stepText;
    public Color inactiveColor, activeColor;
    private List<Image> elements = new List<Image>();

    private void CreateElements()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
            elements.Clear();
        }

        for (int i=0; i < numberOfElements; i++)
        {
            var e = new GameObject();
            e.transform.parent = transform;
            e.name = "Element_" + i;
            var img = e.AddComponent<Image>();
            img.color = inactiveColor;
            elements.Add(img);
        }
        step = 0;
        stepText.text = step + "/" + numberOfElements;
    }

    private void Refresh()
    {
        for (int i=0; i < step; i++)
        {
            elements[i].color = activeColor;
        }
        for(int j=step; j < numberOfElements; j++)
        {
            elements[j].color = inactiveColor;
        }

        stepText.text = step + "/" + numberOfElements;
    }

    public int NumOfEl
    {
        get { return numberOfElements; }
        set { numberOfElements = value; CreateElements(); }
    }

    public int Step
    {
        set { step = value; Refresh(); }
    }
}
