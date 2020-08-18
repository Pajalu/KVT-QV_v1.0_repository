using UnityEngine;

public class DemonstratorValve : MonoBehaviour
{
    private int step;
    private int maxSteps;
    private Animator anim;

    public void IncreaseStep()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        step = anim.GetInteger("Step");
        maxSteps = GetComponentInChildren<MetadataQV>().maxSteps;

        if (step <= maxSteps)
            anim.SetInteger("Step", step + 1);
    }

    public void DecreaseStep()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        step = anim.GetInteger("Step");

        if (step > 0)
            anim.SetInteger("Step", step - 1);
    }
}
