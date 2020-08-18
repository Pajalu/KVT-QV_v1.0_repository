using UnityEngine;

public class MetadataQV : MonoBehaviour
{
    public bool flansch;
    public int scenario; //0=Nichts 1=Ausbau 2=Einbau
    public int maxSteps;    //=0 im Inspector lassen für automatische Zuweiseung

    void Awake()
    {
        if(maxSteps == 0)
        {
            if (flansch)
            {
                if (scenario == 1)
                {
                    maxSteps = 6;
                }
                if (scenario == 2)
                {
                    maxSteps = 13;
                }
            }

            if (!flansch)
            {
                if (scenario == 1)
                {
                    maxSteps = 9;
                }
                if (scenario == 2)
                {
                    maxSteps = 15;
                }
            }
        }    
    }
}
