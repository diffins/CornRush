using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CornGroundChecker))]
public class CornForwardMover : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private CornGroundChecker _groundChecker;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _groundChecker = GetComponent<CornGroundChecker>();
    }

    private void FixedUpdate()
    {
        // Если зёрна находятся в полёте - замедляем вращение в 2 раза для лучшего визуального эффекта
        if (_groundChecker.IsOnGround)
            _rigidBody.AddTorque(Vector3.forward * GameManager.Instance.Settings.CornForwardSpeed, ForceMode.Impulse);
        else
            _rigidBody.AddTorque(Vector3.forward * GameManager.Instance.Settings.CornForwardSpeed * .5f, ForceMode.Impulse);
            
    }

}
