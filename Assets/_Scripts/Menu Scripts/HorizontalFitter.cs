using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalFitter : MonoBehaviour
{
    void Start()
    {
        ScaleToWidth();
    }

    private void ScaleToWidth()
    {
        GetComponent<HorizontalLayoutGroup>().spacing = Screen.width;
    }
}
