using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class ValveInspector : EditorWindow
{
    public GameObject selectedQV;
    public string path;

    public Transform arrow;
    public float arrowPos, _arrowPos;
    public float arrowScale, _arrowScale;

    public Transform pressure;
    public float pressurePos, _pressurePos;
    public float pressureScale, _pressureScale;

    public Transform brush;
    public float brushPos, _brushPos;
    public float brushScale, _brushScale;

    public Transform iron;
    public float ironPos, _ironPos;
    public float ironScale, _ironScale;

    public Transform vice;
    public float vicePos, _vicePos;
    public float viceScale, _viceScale;

    public Transform rod;
    public float rodScale, _rodScale;

    public string[] sizes = new string[] { "klein", "mittel", "groß" };
    
    [MenuItem("KVT/Ventil Inspektor")]

    public static void ShowWindow()
    {
        GetWindow<ValveInspector>("Ventil Inspektor");
    }

    void OnGUI()
    {
        selectedQV = (GameObject)EditorGUILayout.ObjectField("Gewähltes Ventil", selectedQV, typeof(GameObject), true);

        EditorGUILayout.Space();
        GuiLine(1);
        GuiLine(1);

        if (Selection.gameObjects.Length == 1)
        {
            var sel = Selection.activeTransform;

            if (sel.GetComponent<MetadataQV>() != null)
            {
                selectedQV = Selection.activeGameObject;
                //DisableOthers();
            }
            else if(sel.parent != null && sel.parent.GetComponent<MetadataQV>() != null)
            {
                selectedQV = sel.parent.gameObject;
                //DisableOthers();
            }

            Refresh();

        }

        //EditorGUILayout.Space();

        GUILayout.Label("Orientierung:", EditorStyles.boldLabel);
     
        if (GUILayout.Button("Zwei Teile tauschen"))
        {
            SwapChildren();
        }

        EditorGUILayout.Space();
        GuiLine(1);
        

        //##########WENDEN-PFEILE##########
        if (arrow != null)
        {
            GUILayout.Label("Wenden-Pfeile:", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Größe", GUILayout.Width(100));
            arrowScale = arrow.localScale.x;
            arrowScale = GUILayout.HorizontalScrollbar(arrowScale, 1f, 5f, 20f);
            GUILayout.EndHorizontal();        

            GUILayout.BeginHorizontal();
            GUILayout.Label("Position", GUILayout.Width(100));
            arrowPos = arrow.position.y;
            arrowPos = GUILayout.HorizontalScrollbar(arrowPos, 1f, 0f, 300f);
            GUILayout.EndHorizontal();

            if (_arrowScale != arrowScale)
            {
                arrow.localScale = new Vector3(arrowScale, arrowScale, arrowScale);
                _arrowScale = arrowScale;
            }
            if (_arrowPos != arrowPos)
            {
                arrow.position = new Vector3(arrow.position.x, arrowPos, arrow.position.z);
                _arrowPos = arrowPos;
            }

            EditorGUILayout.Space();
            GuiLine(1);
        }

   
        //##########LUFTDRUCKANSCHLUSS##########
        if (pressure != null)
        {
            GUILayout.Label("Luftdruckanschluss:", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Größe", GUILayout.Width(100));
            pressureScale = pressure.localScale.x;
            pressureScale = GUILayout.HorizontalScrollbar(pressureScale, 0.1f, 0.3f, 1.5f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Position", GUILayout.Width(100));
            pressurePos = pressure.position.y;
            pressurePos = GUILayout.HorizontalScrollbar(pressurePos, 1f, 0f, 180f);
            GUILayout.EndHorizontal();

            if (_pressureScale != pressureScale)
            {
                pressure.localScale = new Vector3(pressureScale, pressureScale, pressureScale);
                _pressureScale = pressureScale;
            }
            if (_pressurePos != pressurePos)
            {
                pressure.position = new Vector3(pressure.position.x, pressurePos, pressure.position.z);
                _pressurePos = pressurePos;
            }

            EditorGUILayout.Space();
            GuiLine(1);
        }
        
        //##########PINSEL##########
        if(brush != null)
        {
            GUILayout.Label("Pinsel:", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Größe", GUILayout.Width(100));
            brushScale = brush.localScale.x;
            brushScale = GUILayout.HorizontalScrollbar(brushScale, 0.2f, 0.2f, 2f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Position", GUILayout.Width(100));
            brushPos = brush.GetChild(0).position.x;
            brushPos = GUILayout.HorizontalScrollbar(brushPos, 1f, 0f, -300f);
            GUILayout.EndHorizontal();

            if (_brushScale != brushScale)
            {
                brush.localScale = new Vector3(brushScale, brushScale, brushScale);
                _brushScale = brushScale;
            }
            if (_brushPos != brushPos)
            {
                brush.GetChild(0).position = new Vector3(brushPos, brush.GetChild(0).position.y, brush.GetChild(0).position.z);
                _brushPos = brushPos;
            }

            EditorGUILayout.Space();
            GuiLine(1);
        }

        //##########MONTIEREISEN##########
        if(iron != null)
        {
            GUILayout.Label("Montiereisen:", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Größe", GUILayout.Width(100));
            ironScale = iron.localScale.x;
            ironScale = GUILayout.HorizontalScrollbar(ironScale, 0.2f, 0.2f, 2f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Position", GUILayout.Width(100));
            ironPos = iron.GetChild(0).position.x;
            ironPos = GUILayout.HorizontalScrollbar(ironPos, 1f, 0f, -300f);
            GUILayout.EndHorizontal();

            if (_ironScale != ironScale)
            {
                iron.localScale = new Vector3(ironScale, ironScale, ironScale);
                _ironScale = ironScale;
            }
            if (_ironPos != ironPos)
            {
                iron.GetChild(0).position = new Vector3(ironPos, iron.GetChild(0).position.y, iron.GetChild(0).position.z);
                _ironPos = ironPos;
            }

            EditorGUILayout.Space();
            GuiLine(1);
        }

        //##########SCHRAUBSTOCK##########
        if (vice != null)
        {
            GUILayout.Label("Schraubstock:", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Größe", GUILayout.Width(100));
            viceScale = vice.localScale.x;
            viceScale = GUILayout.HorizontalScrollbar(viceScale, .5f, 1f, 5f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Position", GUILayout.Width(100));
            vicePos = vice.position.x;
            vicePos = GUILayout.HorizontalScrollbar(vicePos, 1f, 0f, -300f);
            GUILayout.EndHorizontal();

            if (_viceScale != viceScale)
            {
                vice.localScale = new Vector3(viceScale, viceScale, viceScale);
                _viceScale = viceScale;
            }
            if (_vicePos != vicePos)
            {
                vice.position = new Vector3(vicePos, vice.position.y, vice.position.z);
                _vicePos = vicePos;
            }

            EditorGUILayout.Space();
            GuiLine(1);
        }
        

        //##########GEWINDESTANGE##########
        if (rod != null)
        {
            GUILayout.Label("Gewindestange:", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Länge", GUILayout.Width(100));
            rodScale = rod.localScale.x;
            rodScale = GUILayout.HorizontalScrollbar(rodScale, 1f, 1f, 10f);
            GUILayout.EndHorizontal();

            if (_rodScale != rodScale)
            {
                rod.localScale = new Vector3(rodScale, rod.localScale.y, rod.localScale.z);
                _rodScale = rodScale;
            }

            EditorGUILayout.Space();
            GuiLine(1);
        }

        GuiLine(1);
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Speicherort", GUILayout.Width(100));
        path = EditorGUILayout.TextField(Path.Combine(Application.dataPath, "_QV"));
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if (GUILayout.Button("Ventil speichern!"))
        {
            //SaveMeshes();

            //selectedQV.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            PrefabUtility.SaveAsPrefabAsset(selectedQV, Path.Combine(path, selectedQV.name + ".prefab"));
            Debug.Log("Das Quetschventil" + selectedQV.name + "wurde erfolgreich gespeichert unter:" + Path.Combine(path, selectedQV.name + ".prefab"));
            DestroyImmediate(selectedQV);
        }
    }

    private void Refresh()
    {
        if (selectedQV != null)
        {
            //selectedQV.transform.localScale = new Vector3(1, 1, 1);

            var arrow_ = selectedQV.transform.Find("Wenden-Pfeile");
            if (arrow_ != null)
            {
                arrow = arrow_.transform;
            }
            else
            {
                arrow = null;
            }

            var pressure_ = selectedQV.transform.Find("Luftdruckanschluss");
            if (pressure_ != null)
            {
                pressure = pressure_.transform;
            }
            else
            {
                pressure = null;
            }

            var brush_ = selectedQV.transform.Find("Pinsel");
            if (brush_ != null)
            {
                brush = brush_.transform;
            }
            else
            {
                brush = null;
            }

            var iron_ = selectedQV.transform.Find("Montiereisen");
            if (iron_ != null)
            {
                iron = iron_.transform;
            }
            else
            {
                iron = null;
            }

            var rod_ = selectedQV.transform.Find("Gewindestange");
            if (rod_ != null)
            {
                rod = rod_.transform;
            }
            else
            {
                rod = null;
            }

            var vice_ = selectedQV.transform.Find("Schraubstock");
            if (vice_ != null)
            {
                vice = vice_.transform;
            }
            else
            {
                vice = null;
            }
        }
    }

    /*private void DisableOthers()
    {
        var others = FindObjectsOfType<MetadataQV>();
        foreach(MetadataQV o in others)
        {
            o.gameObject.SetActive(false);           
        }
        selectedQV.SetActive(true);
    }
    */

    private void SaveMeshes()
    {      
        MeshFilter[] filters = selectedQV.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter m in filters)
        {
            if(m.gameObject.name != "Montiereisen" && m.gameObject.name != "Pinsel" && m.gameObject.name != "Luftdruckanschluss")
            {
                var savePath = "Assets/_QV/Meshes/" + selectedQV.name + "_" + m.gameObject.name + ".asset";
                AssetDatabase.CreateAsset(m.sharedMesh, savePath);
            }          
        }       
        AssetDatabase.SaveAssets();
    }

    private void SwapChildren()
    {
        if (Selection.gameObjects.Length == 2)
        {
            GameObject obj0 = Selection.gameObjects[0];
            GameObject obj1 = Selection.gameObjects[1];

            Mesh msh0 = obj0.GetComponent<MeshFilter>().sharedMesh;
            Mesh msh1 = obj1.GetComponent<MeshFilter>().sharedMesh;

            obj0.GetComponent<MeshFilter>().sharedMesh = msh1;
            obj1.GetComponent<MeshFilter>().sharedMesh = msh0;
        }
    }

    private void GuiLine(int i_height = 1)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, i_height);
        rect.height = i_height;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
    }
}
