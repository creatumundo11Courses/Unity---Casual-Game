using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Course/Level Data",fileName = "new Level Data")]
public class LevelDataSO : ScriptableObject
{
    [SerializeField]
    private List<LevelData> LevelParts = new List<LevelData>();
}
[Serializable]
public class LevelData
{
    public GameObject PrefabGO;
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;
}