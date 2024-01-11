using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Course/Level Data",fileName = "new Level Data")]
public class LevelDataSO : ScriptableObject
{
    [SerializeField]
    private string _levelName;
    public string LevelName { get => _levelName; set => _levelName = value; }
    [SerializeField]
    private List<LevelData> _levelParts = new List<LevelData>();
    public List<LevelData> LevelParts { get => _levelParts; set => _levelParts = value; }
    
}
[Serializable]
public class LevelData
{
    public GameObject PrefabGO;
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;
}