﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CornGroundChecker))]
public class CornController : MonoBehaviour
{
    public event Action OnJump;

    private Rigidbody _rigidBody;
    private CornGroundChecker _groundChecker;
    private float _pressTime;

    private float _offsetVelocityScaler = 40.0f;
    private float _jumpForwardPushScaler = 10.0f;
    private float _jumpPowerScaler = 60.0f;
    private Vector3 _pullDownForce = new Vector3(0, -0.2f, 0);

    private Direction _lastDirection = Direction.NONE;

    private enum Direction
    {
        NONE,
        LEFT,
        RIGHT,
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _groundChecker = GetComponent<CornGroundChecker>();
    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            _pressTime += Time.fixedDeltaTime;

            if(_pressTime >= GameManager.Instance.Settings.PressTime)
            {
                MoveOffset(touch);
            }
        }
        else if(_pressTime != 0)
        {
            if(_pressTime < GameManager.Instance.Settings.PressTime)
            {
                if(_groundChecker.IsOnGround)
                {
                    Jump();
                }
            }

            if(_groundChecker.IsOnGround)
            {
                ResetVelocity();
            }

            _pressTime = 0;
        }

        if(!_groundChecker.IsOnGround)
        {
            _rigidBody.AddForce(Vector3.left * _jumpForwardPushScaler * GameManager.Instance.Settings.JumpPower, ForceMode.Force);
            _rigidBody.velocity += _pullDownForce;
        }
    }

    private void MoveOffset(Touch touch)
    {
        float offsetVelocity = _offsetVelocityScaler * GameManager.Instance.Settings.OffsetPower;

        if (touch.position.x < Screen.width / 2)
        {
            if(_lastDirection == Direction.RIGHT && _groundChecker.IsOnGround)
            {
                ResetVelocity();
            }
            _lastDirection = Direction.LEFT;

            _rigidBody.AddTorque(Vector3.left * offsetVelocity, ForceMode.Force);
        }
        else if (touch.position.x > Screen.width / 2)
        {
            if (_lastDirection == Direction.LEFT && _groundChecker.IsOnGround)
            {
                ResetVelocity();
            }
            _lastDirection = Direction.RIGHT;

            _rigidBody.AddTorque(Vector3.right * offsetVelocity, ForceMode.Force);
        }
    }


    private void Jump()
    {
        OnJump?.Invoke();

        float jumpPower = _jumpPowerScaler * GameManager.Instance.Settings.JumpPower;
        _rigidBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void ResetVelocity()
    {
        Vector3 newVelocity = new Vector3(_rigidBody.velocity.x, 0, 0);
        _rigidBody.velocity = newVelocity;
    }

    
}