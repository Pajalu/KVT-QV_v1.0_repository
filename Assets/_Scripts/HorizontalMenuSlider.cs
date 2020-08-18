using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HorizontalMenuSlider : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector2 anchorLocation;
    Vector2 minLocation;
    Vector2 maxLocation;

    private RectTransform rectTrans;

    float difference;
    float startPos = 0;
    public bool unFolded;

    public float percentThreshold;
    public float easing;

    public Image[] imgs;
    public Text[] txts;


    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        anchorLocation = rectTrans.anchorMax;
        minLocation = anchorLocation;
        maxLocation = new Vector2(0.5f, 1f);

        DisableAll();
    }

    void Update()
    {
        float pos = map(rectTrans.anchorMax.y, minLocation.y, maxLocation.y, startPos, -startPos);
    }

    public void OnDrag(PointerEventData data)
    {
        difference = (data.position.x - data.pressPosition.x)/ Screen.width;
        rectTrans.anchorMax = anchorLocation + new Vector2(difference, 0);
        EnableAll();
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (Mathf.Abs(difference) >= percentThreshold)
        {
            if (difference < 0) //ZU
                Down();

            else if (difference > 0) //AUF
                Up();           
        }
        else
        {
            if(unFolded)
            {
                StartCoroutine(SmoothMove(rectTrans.anchorMax, maxLocation, easing));
            }
            else if (!unFolded)
            {
                StartCoroutine(SmoothMove(rectTrans.anchorMax, minLocation, easing));
            }
            
        }
    }

    IEnumerator SmoothMove(Vector2 startpos, Vector2 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            rectTrans.anchorMax = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    public void ChangePosition()
    {
        if (unFolded)
            Down();

        else if (!unFolded) 
            Up();
    }

    public void SlideIn()
    {
        if (unFolded)
        {
            Down();
        }
    }

    private void Up()
    {
        EnableAll();
        StartCoroutine(SmoothMove(rectTrans.anchorMax, maxLocation, easing));
        anchorLocation = maxLocation;        
        unFolded = true;
    }

    private void Down()
    {
        StartCoroutine(SmoothMove(rectTrans.anchorMax, minLocation, easing));
        anchorLocation = minLocation;
        DisableAll();
        unFolded = false;
    }

    private void DisableAll()
    {
        foreach(Image i in imgs)
        {
            i.enabled = false;
        }
        foreach(Text t in txts)
        {
            t.enabled = false;
        }
    }

    private void EnableAll()
    {
        foreach (Image i in imgs)
        {
            i.enabled = true;
        }
        foreach (Text t in txts)
        {
            t.enabled = true;
        }
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
