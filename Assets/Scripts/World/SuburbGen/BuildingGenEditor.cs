using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildingGen))]
public class BuildingGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        BuildingGen buildingGen = (BuildingGen)target;
        if (GUILayout.Button(("Generate")))
        {
            buildingGen.generate();
        }
        
        if (GUILayout.Button(("ReloadHouse")))
        {
            buildingGen.reloadHouse();
        }

        //if (GUI.changed)
        //{
        //    buildingGen.generate();
        //}
    }
}
