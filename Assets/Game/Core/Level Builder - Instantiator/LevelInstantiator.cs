using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInstantiator : MonoBehaviour
{
    private LevelDataSO _currentLevel;
    [SerializeField]
    private List<LevelDataSO> _allLevels;
    
    public int CurrentLevelIndex;
    private int _currentCollisionableIndex;

    private LevelPart[] _levelParts;

    public event Action OnLevelChanged;

    private void Start()
    {
        CreateNewLevel();
    }

    private void CreateLevel(LevelDataSO levelDataSO)
    {
        if (levelDataSO == null) return;

        _currentLevel = levelDataSO;
        _currentCollisionableIndex = 0;
        _levelParts = new LevelPart[levelDataSO.LevelParts.Count];

        for (int i = 0; i < levelDataSO.LevelParts.Count; i++)
        {
            LevelData lvlD = levelDataSO.LevelParts[i];
            GameObject levelPartGO = Instantiate(lvlD.PrefabGO);
            levelPartGO.transform.position = lvlD.Position;
            levelPartGO.transform.rotation = lvlD.Rotation;
            levelPartGO.transform.localScale = lvlD.Scale;
            if (levelPartGO.TryGetComponent(out CollisionableMultiplierContent collisionableMultiplier))
            {
                ProcessCollisionableMultiplierContent(collisionableMultiplier);
            }

            _levelParts[i] = levelPartGO.GetComponent<LevelPart>();
        }
    }

    private void ProcessCollisionableMultiplierContent(CollisionableMultiplierContent collisionableMultiplier)
    {
        CollisionableMultiplierData.CollisionableConfiguration[] colConfiguration = _currentLevel.MultiplierCollisionablesData[_currentCollisionableIndex].CollisionableConfigurations;
        for (int i = 0; i < colConfiguration.Length; i++)
        {
            collisionableMultiplier.SetOperationToCollisionableMultiplier(colConfiguration[i].Multiplier, colConfiguration[i].OperationType,i);
        }

        _currentCollisionableIndex++;
    }

    private void DestroyLevel()
    {
        if (_levelParts == null) return;
        if (_levelParts.Length == 0) return;

        for (int i = 0; i < _levelParts.Length; i++)
        {
            if(_levelParts[i] == null) continue;

            GameObject levelPart = _levelParts[i].gameObject;
            _levelParts[i] = null;
            Destroy(levelPart);
        }

        _levelParts = null;

    }

    private void CreateNewLevel()
    {
        DestroyLevel();
        OnLevelChanged?.Invoke();
        CreateLevel(_allLevels[CurrentLevelIndex]);
       
    }

    public void NextLevel()
    {
        if (++CurrentLevelIndex == _allLevels.Count) return;
        CreateNewLevel();
    }

    public void PrevLevel()
    {
        if (--CurrentLevelIndex <= -1) return;
        CreateNewLevel();
    }

    public void ResetLevel()
    {
        CreateNewLevel();
    }
}
