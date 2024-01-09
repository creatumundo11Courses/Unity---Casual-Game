using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

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

    private void Update()
    {
        OrganizeObjectsInCircularGrid();
    }
    private void OrganizeObjectsInCircularGrid()
    {
        int numObjects = transform.childCount;
        for (int i = 0; i < numObjects; i++)
        {
            _radius = _scaleFactor * Mathf.Sqrt(i);
            float angleObject = i * _angle * Mathf.Deg2Rad;
            float x = _radius * Mathf.Cos(angleObject);
            float y = _radius * Mathf.Sin(angleObject);
            transform.GetChild(i).localPosition = new Vector3(x, 0, y);
        }
        
    }
}
