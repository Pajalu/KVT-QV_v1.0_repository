using UnityEngine;
using UnityEngine.UI;

public class ButtonSelector : MonoBehaviour
{
    public Image background;
    private Animator anim;
    private bool selected;

    void Awake()
    {
        anim = GetComponent<Animator>();
        selected = false;
    }

    public void ChangeSelection()
    {
        selected = !selected;
        anim.SetBool("Selected", selected);

        if (selected)
            background.enabled = true;
        else if (!selected)
            background.enabled = false;
    }

    public void DeSelect()
    {
        if (selected)
        {
            selected = false;
            anim.SetBool("Selected", selected);
            background.enabled = false;
        }
    }
}
