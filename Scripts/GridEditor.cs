using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GridManager managerGrid = (GridManager) target;
        if(GUILayout.Button("CREATE GRID"))
        {
            managerGrid.CreateAGrid();
        }

        if (GUILayout.Button("DELETE GRID"))
        {
            managerGrid.DestroyGrid();
        }

        if (GUILayout.Button("CREATE OBSTACLES"))
        {
            managerGrid.PutTheObstacles();
        }
    }
}
