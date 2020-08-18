using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ValveImport : EditorWindow
{
    public string qvName = "QV-XXX-XXX";
    public bool flansch = false;
    public bool flanschToggle = false;
    public bool ein = true;
    public bool aus = true;
    public MainMaterial mm;
    public int currentMaterial;
    public bool showObjects = false;
    public bool showExtraObjects = false;
    public bool showMaterials = false;

    public GameObject newQV;

    public Object[] parts = new Object[12];
    public static Object[] extraParts = new Object[5];
    public static Material[] materials = new Material[7];


    [MenuItem("KVT/Ventil importieren")]

    public static void ShowWindow()
    {
        GetWindow<ValveImport>("Ventil Import");

        materials[0] = (Material)AssetDatabase.LoadAssetAtPath("Assets/_Materials/Edelstahl/Edelstahl.mat", typeof(Material));
        materials[1] = (Material)AssetDatabase.LoadAssetAtPath("Assets/_Materials/Aluminium/Aluminium.mat", typeof(Material));
        materials[2] = (Material)AssetDatabase.LoadAssetAtPath("Assets/_Materials/Kunststoff_weiss/Kunststoff_weiss.mat", typeof(Material));
        materials[3] = (Material)AssetDatabase.LoadAssetAtPath("Assets/_Materials/Kunststoff_schwarz/Kunststoff_schwarz.mat", typeof(Material));
        materials[4] = (Material)AssetDatabase.LoadAssetAtPath("Assets/_Materials/Kunststoff_beige/Kunststoff_beige.mat", typeof(Material));
        materials[5] = (Material)AssetDatabase.LoadAssetAtPath("Assets/_Materials/QM_Gummi/QM_Gummi.mat", typeof(Material));
        materials[6] = (Material)AssetDatabase.LoadAssetAtPath("Assets/_Materials/Kunststoff_weiss/Kunststoff_weiss.mat", typeof(Material));

        extraParts[0] = (Object)AssetDatabase.LoadAssetAtPath("Assets/_Tools/Prefabs/montiereisen.obj", typeof(Object));
        extraParts[1] = (Object)AssetDatabase.LoadAssetAtPath("Assets/_Tools/Prefabs/Brush.prefab", typeof(Object));
        extraParts[2] = (Object)AssetDatabase.LoadAssetAtPath("Assets/_Tools/Prefabs/Luftdruckanschluss.prefab", typeof(Object));
        extraParts[3] = (Object)AssetDatabase.LoadAssetAtPath("Assets/_Tools/Prefabs/Vice.prefab", typeof(Object));
        extraParts[4] = (Object)AssetDatabase.LoadAssetAtPath("Assets/_Tools/Prefabs/TurnArrow.prefab", typeof(Object));

        EditorSceneManager.OpenScene("Assets/_Scenes/VentilEditor.unity");
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Bezeichnung", GUILayout.Width(100));
        qvName = EditorGUILayout.TextField(qvName);
        if (GUILayout.Button("Zurücksetzen"))
        {
            ResetWindow();
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        GuiLine(1);
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Flansch", GUILayout.Width(100));
        flansch = EditorGUILayout.Toggle(flansch);
        GUILayout.EndHorizontal();
        if (flansch && !flanschToggle)
        {
            parts = new Object[7];
            flanschToggle = true;
        }
        else if (!flansch && flanschToggle)
        {
            parts = new Object[12];
            flanschToggle = false;
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Ausbau", GUILayout.Width(100));
        aus = EditorGUILayout.Toggle(aus);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Einbau", GUILayout.Width(100));
        ein = EditorGUILayout.Toggle(ein);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("Material", GUILayout.Width(100));      
        mm = (MainMaterial)EditorGUILayout.EnumPopup(mm);
        GUILayout.EndHorizontal();
        switch (mm)
        {
            case MainMaterial.Edelstahl:
                currentMaterial = 0;
                break;
            case MainMaterial.Aluminium:
                currentMaterial = 1;
                break;
            case MainMaterial.Kunststoff_weiß:
                currentMaterial = 2;
                break;
            case MainMaterial.Kunststoff_schwarz:
                currentMaterial = 3;
                break;
            case MainMaterial.Kunststoff_beige:
                currentMaterial = 4;
                break;
        }

        EditorGUILayout.Space();
        GuiLine(1);
        EditorGUILayout.Space();

        showObjects = EditorGUILayout.Foldout(showObjects, "Hier die Teile einsetzen:");

        if (showObjects)
        {
            parts[0] = EditorGUILayout.ObjectField("Gehäuse", parts[0], typeof(Object), true);
            parts[1] = EditorGUILayout.ObjectField("Quetschmanschette", parts[1], typeof(Object), true);

            if (flansch)
            {
                parts[2] = EditorGUILayout.ObjectField("Klemmflansch oben", parts[2], typeof(Object), true);
                parts[3] = EditorGUILayout.ObjectField("Klemmflansch unten", parts[3], typeof(Object), true);
                parts[4] = EditorGUILayout.ObjectField("Schrauben oben", parts[4], typeof(Object), true);
                parts[5] = EditorGUILayout.ObjectField("Schrauben unten", parts[5], typeof(Object), true);
                if (ein)
                    parts[6] = EditorGUILayout.ObjectField("Stützrohr", parts[6], typeof(Object), true);

            }
            if (!flansch)
            {
                parts[2] = EditorGUILayout.ObjectField("Klemmkonus links", parts[2], typeof(Object), true);
                parts[3] = EditorGUILayout.ObjectField("Klemmkonus rechts", parts[3], typeof(Object), true);
                parts[4] = EditorGUILayout.ObjectField("Überwurfmutter links", parts[4], typeof(Object), true);
                parts[5] = EditorGUILayout.ObjectField("Überwurfmutter rechts", parts[5], typeof(Object), true);
                parts[6] = EditorGUILayout.ObjectField("Druckstück links", parts[6], typeof(Object), true);
                parts[7] = EditorGUILayout.ObjectField("Druckstück rechts", parts[7], typeof(Object), true);
                parts[8] = EditorGUILayout.ObjectField("Sechskantmutter links", parts[8], typeof(Object), true);
                parts[9] = EditorGUILayout.ObjectField("Sechskantmutter rechts", parts[9], typeof(Object), true);
                parts[10] = EditorGUILayout.ObjectField("Stützrohr", parts[10], typeof(Object), true);
                parts[11] = EditorGUILayout.ObjectField("Gewindestange", parts[11], typeof(Object), true);
            }
        }

        EditorGUILayout.Space();
        GuiLine(1);
        EditorGUILayout.Space();

        if (GUILayout.Button("Ventil erstellen!"))
        {
            if (aus)
                CreateNewQV("Ausbau");
            if (ein)
                CreateNewQV("Einbau");

            InitWindow();
        }

        EditorGUILayout.Space();
        GuiLine(1);
        EditorGUILayout.Space();

        GUILayout.Label("Sonstiges:");

        showExtraObjects = EditorGUILayout.Foldout(showExtraObjects, "Werkzeuge etc.");

        if (showExtraObjects)
        {
            extraParts[0] = EditorGUILayout.ObjectField("Montiereisen", extraParts[0], typeof(Object), true);
            extraParts[1] = EditorGUILayout.ObjectField("Pinsel", extraParts[1], typeof(Object), true);
            extraParts[2] = EditorGUILayout.ObjectField("Luftdruckanschluss", extraParts[2], typeof(Object), true);
            extraParts[3] = EditorGUILayout.ObjectField("Schraubstock", extraParts[3], typeof(Object), true);
            extraParts[4] = EditorGUILayout.ObjectField("Wenden-Pfeile", extraParts[4], typeof(Object), true);
        }

        showMaterials = EditorGUILayout.Foldout(showMaterials, "Materialien");

        if (showMaterials)
        {
            GUILayout.Label("Abhängig von Auswahl:", EditorStyles.boldLabel);
            materials[0] = (Material)EditorGUILayout.ObjectField("Edelstahl", materials[0], typeof(Material), true);
            materials[1] = (Material)EditorGUILayout.ObjectField("Aluminium", materials[1], typeof(Material), true);
            materials[2] = (Material)EditorGUILayout.ObjectField("Kunststoff_weiß", materials[2], typeof(Material), true);
            materials[3] = (Material)EditorGUILayout.ObjectField("Kunststoff_schwarz", materials[3], typeof(Material), true);
            materials[4] = (Material)EditorGUILayout.ObjectField("Kunststoff_beige", materials[4], typeof(Material), true);
            GUILayout.Label("Immer verwendet:", EditorStyles.boldLabel);
            materials[5] = (Material)EditorGUILayout.ObjectField("Quetschmanschette", materials[5], typeof(Material), true);
            materials[6] = (Material)EditorGUILayout.ObjectField("Druckstücke", materials[6], typeof(Material), true);
        }
    }


    public enum MainMaterial
    {
        Edelstahl = 0,
        Aluminium = 1,
        Kunststoff_weiß = 2,
        Kunststoff_schwarz = 3,
        Kunststoff_beige = 4
    }


    private void CreateNewQV(string scenario) //=Ausbau oder =Einbau
    {
        string qvTitle = qvName + "_" + scenario;

        newQV = new GameObject(qvTitle);

        var anim = newQV.AddComponent<Animator>();
        var mqv = newQV.AddComponent<MetadataQV>();
        var outline = newQV.AddComponent<Outline>();

        mqv.flansch = flansch;

        CreatePart(0, "Gehäuse", currentMaterial);
        CreatePart(1, "Quetschmanschette", 5);

        if (scenario == "Ausbau")
        {
            mqv.scenario = 1;


            if (flansch)
            {
                mqv.maxSteps = 6;
                anim.runtimeAnimatorController = (RuntimeAnimatorController)AssetDatabase.LoadAssetAtPath("Assets/_Animations/MF-aus_animator/MF-aus_animator.controller", typeof(RuntimeAnimatorController));

                CreatePart(2, "Klemmflansch oben", currentMaterial);
                CreatePart(3, "Klemmflansch unten", currentMaterial);
                CreatePart(4, "Schrauben oben", currentMaterial);
                CreatePart(5, "Schrauben unten", currentMaterial);
                
                CreateExtraPart(4, "Wenden-Pfeile", new Vector3(0, 100, 0));

            }

            if (!flansch)
            {
                mqv.maxSteps = 9;
                anim.runtimeAnimatorController = (RuntimeAnimatorController)AssetDatabase.LoadAssetAtPath("Assets/_Animations/OF-aus_animator/OF-aus_animator.controller", typeof(RuntimeAnimatorController));


                CreatePart(2, "Klemmkonus links", currentMaterial);
                CreatePart(3, "Klemmkonus rechts", currentMaterial);
                CreatePart(4, "Überwurfmutter links", currentMaterial);
                CreatePart(5, "Überwurfmutter rechts", currentMaterial);
                CreatePart(6, "Druckstück links", 6); //KUNSTSTOFF_GRAU
                CreatePart(7, "Druckstück rechts", 6); //KUNSTSTOFF_GRAU
                CreatePart(8, "Sechskantmutter links", currentMaterial);
                CreatePart(9, "Sechskantmutter rechts", currentMaterial);

                CreatePart(10, "Stützrohr", 6); //KUNSTSTOFF_GRAU
                CreatePart(11, "Gewindestange", 0, new Vector3(2, 1, 1));
                //CreateExtraPart(3, "Schraubstock", new Vector3(0, 0, 0));

                newQV.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        if (scenario == "Einbau")
        {
            mqv.scenario = 2;


            if (flansch)
            {
                mqv.maxSteps = 13;
                anim.runtimeAnimatorController = (RuntimeAnimatorController)AssetDatabase.LoadAssetAtPath("Assets/_Animations/MF-ein_animator/MF-ein_animator.controller", typeof(RuntimeAnimatorController));

                CreatePart(2, "Klemmflansch oben", currentMaterial);
                CreatePart(3, "Klemmflansch unten", currentMaterial);
                CreatePart(4, "Schrauben oben", currentMaterial);
                CreatePart(5, "Schrauben unten", currentMaterial);
                CreatePart(6, "Stützrohr", 6);

                CreateExtraPart(4, "Wenden-Pfeile", new Vector3(0, 100, 0));
                CreateExtraPart(2, "Luftdruckanschluss", new Vector3(0, 0, 0), new Vector3(0, 180, -90));
            }

            if (!flansch)
            {
                mqv.maxSteps = 15;
                anim.runtimeAnimatorController = (RuntimeAnimatorController)AssetDatabase.LoadAssetAtPath("Assets/_Animations/OF-ein_animator/OF-ein_animator.controller", typeof(RuntimeAnimatorController));

                CreatePart(2, "Klemmkonus links", currentMaterial);
                CreatePart(3, "Klemmkonus rechts", currentMaterial);
                CreatePart(4, "Überwurfmutter links", currentMaterial);
                CreatePart(5, "Überwurfmutter rechts", currentMaterial);
                CreatePart(6, "Druckstück links", 6); //KUNSTSTOFF_GRAU
                CreatePart(7, "Druckstück rechts", 6); //KUNSTSTOFF_GRAU
                CreatePart(8, "Sechskantmutter links", currentMaterial);
                CreatePart(9, "Sechskantmutter rechts", currentMaterial);
                CreatePart(10, "Stützrohr", 6); //KUNSTSTOFF_GRAU
                CreatePart(11, "Gewindestange", 0, new Vector3(2, 1, 1));
                //CreateExtraPart(3, "Schraubstock", new Vector3(0, 0, 0));
                CreateExtraPart(2, "Luftdruckanschluss", new Vector3(0, 0, 0), new Vector3(0, 90, -90));
                newQV.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            CreateExtraPart(0, "Montiereisen", new Vector3(0, 0, 0));
            CreateExtraPart(1, "Pinsel", new Vector3(0, 0, 0), new Vector3(0, 0, -90)); 
            
        }
        AssetDatabase.SaveAssets();
    }

    private void CreatePart(int part, string name, int material)
    {
        GameObject g = Instantiate(parts[part]) as GameObject;
        g.name = name;
        g.transform.parent = newQV.transform;

        CombineMeshes(g.transform, materials[material]);
        /*
        foreach (MeshRenderer r in g.GetComponentsInChildren<MeshRenderer>())
            r.material = materials[material];
            */
    }

    private void CreatePart(int part, string name, int material, Vector3 scale)
    {
        GameObject g = Instantiate(parts[part]) as GameObject;
        g.name = name;
        g.transform.parent = newQV.transform;
        g.transform.localScale = scale;

        CombineMeshes(g.transform, materials[material]);
        /*
        foreach (MeshRenderer r in g.GetComponentsInChildren<MeshRenderer>())
            r.material = materials[material];
            */
    }

    private void CreateExtraPart(int part, string name, Vector3 position)
    {
        GameObject g = Instantiate(extraParts[part]) as GameObject;
        g.name = name;
        g.transform.parent = newQV.transform;
        g.transform.position = position;
    }

    private void CreateExtraPart(int part, string name, Vector3 position, Vector3 rotation)
    {
        GameObject g = Instantiate(extraParts[part]) as GameObject;
        g.name = name;
        g.transform.parent = newQV.transform;
        g.transform.position = position;
        g.transform.eulerAngles = rotation;
    }

    static void InitWindow()
    {
        ValveInspector window = (ValveInspector)GetWindow(typeof(ValveInspector));
        window.Show();
        //GetWindow<ValveImport>().Close();
    }

    private void ResetWindow()
    {
        EditorSceneManager.OpenScene("Assets/_Scenes/VentilEditor.unity");

        qvName = "QV-XXX-XXX";
        flansch = false;
        flanschToggle = false;
        ein = true;
        aus = true;
        mm = MainMaterial.Edelstahl;
        currentMaterial = 0;
        showObjects = false;
        showExtraObjects = false;
        showMaterials = false;

        for (int i = 0; i < parts.Length; i++)
            parts[i] = null;
    }

    public void CombineMeshes(Transform parent, Material finalMaterial)
    {
        MeshFilter[] filters = parent.GetComponentsInChildren<MeshFilter>();

        Debug.Log(name + "is combining meshes!" + filters.Length);

        Mesh finalMesh = new Mesh();

        CombineInstance[] combiners = new CombineInstance[filters.Length];

        for (int a = 0; a < filters.Length; a++)
        {
            combiners[a].subMeshIndex = 0;
            combiners[a].mesh = filters[a].sharedMesh;
            combiners[a].transform = filters[a].transform.localToWorldMatrix;
        }

        finalMesh.CombineMeshes(combiners);

        parent.gameObject.AddComponent<MeshFilter>().sharedMesh = finalMesh;
        parent.gameObject.AddComponent<MeshRenderer>().material = finalMaterial;

        for (int a = 0; a < filters.Length; a++)
        {
            DestroyImmediate(filters[a].gameObject);
        }
        var savePath = "Assets/_QV/Meshes/" + qvName + "_" + parent.name + ".asset";
        AssetDatabase.CreateAsset(finalMesh, savePath);
    }

    private void GuiLine(int i_height = 1)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, i_height);
        rect.height = i_height;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
    }
}
