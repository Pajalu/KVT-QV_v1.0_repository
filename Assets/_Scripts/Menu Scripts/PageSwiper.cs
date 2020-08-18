using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Vector3 panelLocation;
    Vector3 newLocation;

    public float percentThreshold = 0.1f;
    public float easing = 0.5f;
    private int totalPages = 10;
    public int currentPage = 1;

    public Text qvName;
    private Image[] dots = new Image[10];
    public Transform dotManager;
    private GameObject[] qvs;
    public Sprite[] sprites;
    public Sprite dotGrey;
    public Sprite dotBlue;
    //public MainMenu main;
    public StaticValues staticValues;
    private UnityAction insertFunction;

    void Awake()
    {
        totalPages = sprites.Length;
        qvs = new GameObject[totalPages];

        for (int i = 0; i < totalPages; i++)
        {
            string realQVnumber = (i + 1).ToString();
            float posX = Screen.width / 2 + i * Screen.width;

            qvs[i] = new GameObject("QV" + realQVnumber);
            qvs[i].transform.parent = this.transform;
            qvs[i].AddComponent<Image>();
            qvs[i].AddComponent<Button>();
            qvs[i].GetComponent<Image>().sprite = sprites[i];
            qvs[i].transform.position = new Vector2(posX, transform.position.y);
            qvs[i].GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 1000);
            qvs[i].GetComponent<Button>().onClick = GetComponent<Button>().onClick;
            qvs[i].GetComponent<Button>().onClick.AddListener(() => SelectQV());
            qvs[i].GetComponent<Button>().transition = Selectable.Transition.None;
        }


        qvName = GameObject.Find("QV-Text").GetComponent<Text>();
        //dots = GameObject.Find("Dots").GetComponentsInChildren<Image>();
        CreateDots();
        panelLocation = transform.position;
    }

    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            newLocation = panelLocation;
            if (percentage > 0 && currentPage < totalPages)
            {
                Page("Up");
                newLocation += new Vector3(-Screen.width, 0, 0);
            }
            else if (percentage < 0 && currentPage > 1)
            {
                Page("Down");
                newLocation += new Vector3(Screen.width, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    void Page(string s)
    {
        if (s == "Up")
        {
            currentPage++;
            ArrangeDots();
        }
        else if (s == "Down")
        {
            currentPage--;
            ArrangeDots();
        }

        //main.QVselect(currentPage);
        staticValues.QV = currentPage;

        switch (currentPage)
        {
            case 1:
                qvName.text = "QV-100";
                break;
            case 2:
                qvName.text = "QV-120/QV-140";
                break;
            case 3:
                qvName.text = "QV-200";
                break;
            case 4:
                qvName.text = "QV-220";
                break;
            case 5:
                qvName.text = "QV-240";
                break;
            case 6:
                qvName.text = "QV-260";
                break;
            case 7:
                qvName.text = "QV-600";
                break;
            case 8:
                qvName.text = "QV-635";
                break;
            case 9:
                qvName.text = "QV-800";
                break;
            case 10:
                qvName.text = "QV special";
                break;

        }
    }

    private void CreateDots()
    {
        for(int i=0; i < qvs.Length; i++)
        {
            var dot = Instantiate(new GameObject("Dot_"+i), dotManager);
            Image img = dot.AddComponent<Image>();
            img.sprite = dotGrey;
            dot.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
            dots[i] = img;
        }
        ArrangeDots();
    }

    void ArrangeDots()
    {
        dots = GameObject.Find("Dots").GetComponentsInChildren<Image>();
        foreach (Image i in dots)
        {
            i.sprite = dotGrey;
        }
        dots[currentPage - 1].sprite = dotBlue;
    }

    void OnEnable()
    {
        //transform.position = panelLocation;
        dots = GameObject.Find("Dots").GetComponentsInChildren<Image>();
        print("enabled");
    }

    public void JumpToPage()
    {
        transform.position = new Vector3(-1000*staticValues.QV+540, 1080, 0);
    }

    public void SelectQV()
    {
        //main.QVselect(currentPage);
        staticValues.QV = currentPage;
    }
}