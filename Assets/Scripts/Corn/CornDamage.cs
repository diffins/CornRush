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

    private float _contactTime;
    private bool _isHeating;
    private string _hotObstacleTag = "Hot";
    private string _wallTag = "Wall";
    private float _coolingScaler = 0.3f;

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

        if(_contactTime >= GameManager.Instance.Settings.TimeToBlowUp)
        {
            OnAchieveMaxHeat?.Invoke();
        }

        if(_isHeating)
        {
            _contactTime += Time.deltaTime;
            ChangeColor();
        }
        else if(_contactTime != 0)
        {
            _contactTime -= Time.deltaTime * _coolingScaler;

            if (_contactTime < 0)
                _contactTime = 0;

            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        float colorDelta = _contactTime / GameManager.Instance.Settings.TimeToBlowUp;
        _meshRenderer.material.color = Color.Lerp(_defaultColor, _maxHeatColor, colorDelta);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == _hotObstacleTag || (GameManager.Instance.Settings.IsWallsAreHot && collision.gameObject.tag == _wallTag))
        {
            _isHeating = true;
            OnStartHeating?.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == _hotObstacleTag || (GameManager.Instance.Settings.IsWallsAreHot && collision.gameObject.tag == _wallTag))
        {
            _isHeating = false;
            OnStartCooling?.Invoke();
        }
    }

}
