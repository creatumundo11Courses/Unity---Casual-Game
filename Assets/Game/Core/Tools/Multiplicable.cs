using System;
using System.Collections.Generic;
using UnityEngine;

public class Multiplicable: MonoBehaviour
{
    [SerializeField]
    private GameObject _multiplicableGO;

    private List<GameObject> _multiplicables = new List<GameObject>();

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
        }
    }
}