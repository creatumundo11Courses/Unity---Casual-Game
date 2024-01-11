using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Course/Level Data",fileName = "new Level Data")]
public class LevelDataSO : ScriptableObject
{
    [SerializeField]
    private List<GameObject> LevelParts = new List<GameObject>();
}
