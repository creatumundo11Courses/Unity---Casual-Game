using System;
using System.Collections.Generic;
using UnityEngine;

public class Multiplicable: MonoBehaviour
{
    [SerializeField]
    private GameObject _multiplicableGO;

    private List<GameObject> _multiplicables = new List<GameObject>();

    public event Action<GameObject> OnCreate;
    public event Action<GameObject> OnDestroy;

    public int GetCount()
    {
        return _multiplicables.Count;
    }
    public void Generate(int numberInstantiation, Transform parentT)
    {
        for (int i = 0; i < numberInstantiation; i++)
        {
            GameObject instanceGO = Instantiate(_multiplicableGO, parentT);
            _multiplicables.Add(instanceGO);
            OnCreate?.Invoke(instanceGO);
        }
    }

    public void Remove(int quantity)
    {
        if (quantity <= 0) return;
        if (_multiplicables.Count == 0) return;
        if (quantity > _multiplicables.Count) return;

        for (int i = 0; i < quantity; i++)
        {
            GameObject go = _multiplicables[0];
            Remove(go);
        }
    }

    public void Remove(GameObject go)
    {
        if (_multiplicables.Count == 0) return;

        OnDestroy?.Invoke(go);
        _multiplicables.Remove(go);
        Destroy(go);
    }


}