using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValveImageChanger : MonoBehaviour
{
    public StaticValues staticValues;
    public PageSwiper pageswiper;

    void OnEnable()
    {
        GetComponent<Image>().sprite = pageswiper.sprites[staticValues.QV-1];
    }
}
