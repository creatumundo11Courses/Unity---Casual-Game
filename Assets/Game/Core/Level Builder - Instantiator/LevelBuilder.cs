using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public string LevelName;
    public LevelDataSO LevelDataSO;

    internal void ClearData()
    {
        if (LevelDataSO == null) return;

        LevelDataSO.LevelParts.Clear();
    }

    internal void AddLevelPart(LevelData newLD)
    {
        if (LevelDataSO == null) return;

        LevelDataSO.LevelParts.Add(newLD);
    }

    internal void SetLevelName(string levelName)
    {
        if (LevelDataSO == null) return;
        
        LevelDataSO.LevelName = levelName;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(LevelBuilder))]
public class LevelBuilderEditor : Editor
{
    private LevelBuilder _levelBuilder;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _levelBuilder = (LevelBuilder)target;

        if (GUILayout.Button("Save LevelData")) 
        {
            _levelBuilder.ClearData();
            LevelPart[] allGOInChildren = _levelBuilder.GetComponentsInChildren<LevelPart>();
            if (allGOInChildren.Length == 0) return;

            for (int i = 0; i < allGOInChildren.Length; i++) 
            {
                GameObject referenceGO = allGOInChildren[i].gameObject;
                GameObject originalGO = PrefabUtility.GetCorrespondingObjectFromOriginalSource(referenceGO);
                LevelData newLD = new LevelData() { PrefabGO = originalGO, Position = referenceGO.transform.position, Rotation = referenceGO.transform.rotation, Scale = referenceGO.transform.localScale };
                _levelBuilder.AddLevelPart(newLD);
            }

            _levelBuilder.SetLevelName(_levelBuilder.LevelName);

        }
        if (GUILayout.Button("Edit LevelData")) { Debug.Log("Se han editado los datos"); }
        if (GUILayout.Button("Delete LevelData")) { Debug.Log("Se han eliminado los datos"); }
    }
}
#endif
