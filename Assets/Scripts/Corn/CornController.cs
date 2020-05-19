using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CornGroundChecker))]
public class CornController : MonoBehaviour
{
    public event Action OnJump;

    private Rigidbody _rigidBody;
    private CornGroundChecker _groundChecker;

    private float _offsetVelocityScaler = 40.0f;
    private float _jumpForwardPushScaler = 10.0f;
    private float _jumpPowerScaler = 60.0f;
    private Vector3 _pullDownForce = new Vector3(0, -0.2f, 0);

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _groundChecker = GetComponent<CornGroundChecker>();
    }

    public void MoveOffset(float direction)
    {
        Vector3 vectorDirection = new Vector3();
        if (direction > 0) vectorDirection = Vector3.forward;
        else vectorDirection = Vector3.back;

        float offsetVelocity = _offsetVelocityScaler * GameManager.Instance.Settings.OffsetPower;
        _rigidBody.AddForce(vectorDirection * offsetVelocity, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        if(!_groundChecker.IsOnGround)
        {
            _rigidBody.AddForce(Vector3.left * _jumpForwardPushScaler * GameManager.Instance.Settings.JumpPower, ForceMode.Force);
            _rigidBody.velocity += _pullDownForce;
        }
    }

    public void Jump()
    {
        if (_groundChecker.IsOnGround)
        {
            OnJump?.Invoke();

            float jumpPower = _jumpPowerScaler * GameManager.Instance.Settings.JumpPower;
            _rigidBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }


}
