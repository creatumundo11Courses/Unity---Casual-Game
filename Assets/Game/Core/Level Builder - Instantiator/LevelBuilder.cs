using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



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

        if (GUILayout.Button("Save LevelData") && _levelBuilder.LevelDataSO != null) 
        {
            ProcessAllGO();
            ProcessCollisionableMultiplierContent();
            _levelBuilder.SetLevelName(_levelBuilder.LevelName);
            EditorUtility.SetDirty(_levelBuilder.LevelDataSO);
            EditorUtility.DisplayDialog("LevelBuilder", "has been successfully saved", "ok");

        }
        if (GUILayout.Button("Edit LevelData") && _levelBuilder.LevelDataSO != null) 
        {
            List<LevelData> allLevelParts = _levelBuilder.LevelDataSO.LevelParts;
            if (allLevelParts.Count == 0) return;

            foreach (LevelData lvlD in allLevelParts)
            {
                GameObject levelPartGO = Instantiate(lvlD.PrefabGO,_levelBuilder.transform);
                levelPartGO.transform.position = lvlD.Position;
                levelPartGO.transform.rotation = lvlD.Rotation;
                levelPartGO.transform.localScale = lvlD.Scale;
                PrefabUtility.ConvertToPrefabInstance(levelPartGO,lvlD.PrefabGO,new ConvertToPrefabInstanceSettings(), InteractionMode.UserAction);
            }

            _levelBuilder.LevelName = _levelBuilder.LevelDataSO.LevelName;
        }
        if (GUILayout.Button("Delete LevelData")) 
        { 
            LevelPart[] allGoInChildren = _levelBuilder.GetComponentsInChildren<LevelPart>();
            if (allGoInChildren.Length == 0) return;

            foreach (LevelPart levelPart in allGoInChildren)
            {
                DestroyImmediate(levelPart.gameObject);
            }

        }
    }

   

    private void ProcessAllGO()
    {
        _levelBuilder.ClearData();
        LevelPart[] allGOInChildren = _levelBuilder.GetComponentsInChildren<LevelPart>();
        if (allGOInChildren.Length == 0) return;

        for (int i = 0; i < allGOInChildren.Length; i++)
        {
            GameObject referenceGO = allGOInChildren[i].gameObject;
            GameObject originalGO = PrefabUtility.GetCorrespondingObjectFromSource(referenceGO);
            LevelData newLD = new LevelData() { PrefabGO = originalGO, Position = referenceGO.transform.position, Rotation = referenceGO.transform.rotation, Scale = referenceGO.transform.localScale };
            _levelBuilder.AddLevelPart(newLD);
        }
    }

    private void ProcessCollisionableMultiplierContent()
    {
        CollisionableMultiplierContent[] allGOInChildren = _levelBuilder.GetComponentsInChildren<CollisionableMultiplierContent>();
        CollisionableMultiplierData[] collData = _levelBuilder.LevelDataSO.MultiplierCollisionablesData.ToArray();
        if (collData.Length == allGOInChildren.Length) return;
                             
        for (int i = 0 ; i < allGOInChildren.Length - collData.Length; i++)
        {
            ProcessCollisionableMultiplierContent(allGOInChildren[i]);
        }
       

    }

    private void ProcessCollisionableMultiplierContent(CollisionableMultiplierContent collisionableMultiplier)
    {
        if (collisionableMultiplier == null) return;

        if (_levelBuilder.LevelDataSO.MultiplierCollisionablesData == null)
        {
            _levelBuilder.LevelDataSO.MultiplierCollisionablesData = new();
        }

        CollisionableMultiplierData collisionableMultiplierData = new CollisionableMultiplierData();

        collisionableMultiplierData.CollisionableConfigurations = new CollisionableMultiplierData.CollisionableConfiguration[collisionableMultiplier.CollisionableMultipliers.Length];

        _levelBuilder.LevelDataSO.MultiplierCollisionablesData.Add(collisionableMultiplierData);
    }
}
#endif
