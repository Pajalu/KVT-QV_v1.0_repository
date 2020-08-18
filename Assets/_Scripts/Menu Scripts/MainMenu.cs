using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private List<Transform> sites = new List<Transform>();
    private int currentSite;
    public bool animationFinished;
    public Animator anim;
    private bool keysLocked;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.fullScreen = false;

        anim = GetComponent<Animator>();

        foreach (Transform child in transform)
        {
            sites.Add(child);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !keysLocked)
        {
            PreviousSite();
        }
    }

    public void NextSite()
    {
        if(currentSite < sites.Count)
        {
            currentSite++;
            anim.SetInteger("ActiveSite", currentSite);
            //StartCoroutine(ChangeBetweenSites(currentSite - 1, currentSite));
        }
    }

    public void PreviousSite()
    {
        if (currentSite > 0)
        {
            currentSite--;
            //StartCoroutine(ChangeBetweenSites(currentSite + 1, currentSite));
            anim.SetInteger("ActiveSite", currentSite);
        }      
    }


    /*
    IEnumerator ChangeBetweenSites(int previous, int current)
    {
        keysLocked = true;
        Animator anim = sites[previous].GetComponent<Animator>();
        anim.SetTrigger("Disable");
        if (previous == 0)
        {
            logo.SetBool("Small", true);
        }

        yield return new WaitUntil(() => animationFinished);

        sites[current].gameObject.SetActive(true);
        if (current == 0)
        {
            logo.SetBool("Small", false);
        }
        animationFinished = false;
        keysLocked = false;
    }

    */
}
