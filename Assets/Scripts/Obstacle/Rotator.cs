using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField] private float _speed = 2f;

    private void FixedUpdate()
    {
        Quaternion rotationY = Quaternion.AngleAxis(_speed, Vector3.up);
        transform.rotation *= rotationY;
    }

    
}
