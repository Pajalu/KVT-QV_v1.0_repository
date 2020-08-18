using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticValues : MonoBehaviour
{
    static public int currentQV;
    static public int currentScenario;
    static public bool alreadyStarted;
    static public bool autoTargetDetection;

    void Awake()
    {
        string scene = SceneManager.GetActiveScene().name;               

        if (scene == "QV1-1")
        {
            QV = 1;
            Scenario = 1;
        }
        if (scene == "QV1-2")
        {
            QV = 1;
            Scenario = 2;
        }

        if (scene == "QV2-1")
        {
            QV = 2;
            Scenario = 1;
        }
        if (scene == "QV2-2")
        {
            QV = 2;
            Scenario = 2;
        }

        if (scene == "QV3-1")
        {
            QV = 3;
            Scenario = 1;
        }
        if (scene == "QV3-2")
        {
            QV = 3;
            Scenario = 2;
        }

        if (scene == "QV4-1")
        {
            QV = 4;
            Scenario = 1;
        }
        if (scene == "QV4-2")
        {
            QV = 4;
            Scenario = 2;
        }

        if (scene == "QV5-1")
        {
            QV = 5;
            Scenario = 1;
        }
        if (scene == "QV5-2")
        {
            QV = 5;
            Scenario = 2;
        }

        if (scene == "QV6-1")
        {
            QV = 6;
            Scenario = 1;
        }
        if (scene == "QV6-2")
        {
            QV = 6;
            Scenario = 2;
        }

        if (scene == "QV7-1")
        {
            QV = 7;
            Scenario = 1;
        }
        if (scene == "QV7-2")
        {
            QV = 7;
            Scenario = 2;
        }

        if (scene == "QV8-1")
        {
            QV = 8;
            Scenario = 1;
        }
        if (scene == "QV8-2")
        {
            QV = 8;
            Scenario = 2;
        }

        if (scene == "QV9-1")
        {
            QV = 9;
            Scenario = 1;
        }
        if (scene == "QV9-2")
        {
            QV = 9;
            Scenario = 2;
        }

        if (scene == "QV10-1")
        {
            QV = 10;
            Scenario = 1;
        }
        if (scene == "QV10-2")
        {
            QV = 10;
            Scenario = 2;
        }
    }

    public int QV
    {
        get { return currentQV; }
        set { currentQV = value; print("CURRENT QV:" + currentQV + "-" + currentScenario); }
    }

    public int Scenario
    {
        get { return currentScenario; }
        set { currentScenario = value; print("CURRENT QV:" + currentQV +"-" + currentScenario); }
    }

    public bool AutoDetection
    {
        get { return autoTargetDetection; }
        set { autoTargetDetection = value; }
    }

    //evtl nicht benötigt!!!
    //stattdessen if(currentQV==0)
    public bool Started
    {
        get { return alreadyStarted; }
        set { alreadyStarted = value; print("Started"); }
    }
}
