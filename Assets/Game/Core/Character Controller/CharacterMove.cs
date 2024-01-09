using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField]
    private float _forwardSpeed;
    [SerializeField]
    private float _horizontalSpeed;
    public void Move(Vector3 traslation)
    {
        traslation.Normalize();
        traslation.z *= _forwardSpeed;
        traslation.x *= _horizontalSpeed;
        traslation *= Time.deltaTime;
        transform.Translate(traslation,Space.World);
    }
}
