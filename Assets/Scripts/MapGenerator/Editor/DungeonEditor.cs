using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class DungeonEditor : Editor
{
    AbstractDungeonGenerator generator;

    private void Awake()
    {
        generator = (AbstractDungeonGenerator) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }

        if (GUILayout.Button("Clear Dungeon"))
        {
            generator.ClearDungeon();
        }
    }
}
