using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class CornDamage : MonoBehaviour
{
    public event Action OnStartHeating;
    public event Action OnStartCooling;
    public event Action OnAchieveMaxHeat;

    private float _heatingTime;
    private bool _isHeating;
    private bool _isOnFloor;
    private string _hotObstacleTag = "Hot";
    private string _floorTag = "Floor";
    private string _wallTag = "Wall";
    private float _coolingScaler = 0.6f;
    private float _airHeatingScaler = .8f;

    private MeshRenderer _meshRenderer;
    private Color _defaultColor;
    private Color _maxHeatColor = new Color(0.9f, 0.2f, 0.08f);

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultColor = _meshRenderer.material.color;
    }

    private void Update()
    {

        if(_heatingTime >= GameManager.Instance.Settings.TimeToBlowUp)
        {
            OnAchieveMaxHeat?.Invoke();
        }

        if(!_isOnFloor)
        {
            _heatingTime += Time.deltaTime * _airHeatingScaler;
            Debug.Log("Heating");
            ChangeColor();
        }

        if(_isHeating)
        {
            _heatingTime += Time.deltaTime;
            ChangeColor();
        }
        else if(_heatingTime != 0)
        {
            _heatingTime -= Time.deltaTime * _coolingScaler;

            if (_heatingTime < 0)
                _heatingTime = 0;

            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        float colorDelta = _heatingTime / GameManager.Instance.Settings.TimeToBlowUp;
        _meshRenderer.material.color = Color.Lerp(_defaultColor, _maxHeatColor, colorDelta);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == _hotObstacleTag || (GameManager.Instance.Settings.IsWallsAreHot && collision.gameObject.tag == _wallTag))
        {
            _isHeating = true;
            OnStartHeating?.Invoke();
        }

        if(collision.gameObject.tag == _floorTag)
        {
            _isOnFloor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == _hotObstacleTag || (GameManager.Instance.Settings.IsWallsAreHot && collision.gameObject.tag == _wallTag))
        {
            _isHeating = false;
            OnStartCooling?.Invoke();
        }

        if (collision.gameObject.tag == _floorTag)
        {
            _isOnFloor = false;
        }
    }

}
