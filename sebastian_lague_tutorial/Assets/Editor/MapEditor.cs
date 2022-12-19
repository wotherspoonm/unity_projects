using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (MapGenerator))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        MapGenerator map = target as MapGenerator;
        if (GUI.changed) { // Increase perform by only updating the map if there is input to the GUI (i.e a value has been changed)
            map.GenerateMap();
        }
    }
}
