using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using DG.Tweening;
public class CircleLayout : MonoBehaviour
{
    private float _radius;
    [SerializeField]
    private float _scaleFactor;
    [SerializeField]
    private float _angle;

    private void Start()
    {
        OrganizeObjectsInCircularGrid();
    }

    public float GetRadius()
    {
        return _radius;
    }
    public void Organize()
    {
        OrganizeObjectsInCircularGrid();
    }
    private void OrganizeObjectsInCircularGrid()
    {
        int numObjects = transform.childCount;
        for (int i = 0; i < numObjects; i++)
        {
            Vector3 endPosition = GetObjectPos(i);
            transform.GetChild(i).DOLocalMove(endPosition, 0.4f);
        }

    }

    private Vector3 GetObjectPos(int i)
    {
        _radius = _scaleFactor * Mathf.Sqrt(i);
        float angleObject = i * _angle * Mathf.Deg2Rad;
        float x = _radius * Mathf.Cos(angleObject);
        float y = _radius * Mathf.Sin(angleObject);
        return new Vector3(x, 0, y);
    }

    
}
