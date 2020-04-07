using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CornDamage), typeof(Animator))]
public class CornBlowUp : MonoBehaviour
{
    public event Action OnBlowUp;

    private CornDamage _cornDamage;
    private Animator _animator;
    private CornPool _cornPool;

    private float _blowRadius = 20f;
    private float _blowPowerScaler = 3000f;

    private void Start()
    {
        _cornDamage = GetComponent<CornDamage>();
        _animator = GetComponent<Animator>();
        _cornPool = GetComponentInParent<CornPool>();

        _cornDamage.OnAchieveMaxHeat += StartBlowUpCorn;
        _animator.enabled = false;
    }

    private void StartBlowUpCorn()
    {
        _cornDamage.enabled = false;
        _animator.enabled = true;
    }

    // Вызывается через Animation Event в анимации взрыва кукурузки
    private void BlowUp()
    {
        Vector3 blowPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(blowPos, _blowRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(_blowPowerScaler * GameManager.Instance.Settings.BlowPower, blowPos, _blowRadius);
            }
        }

        _cornPool.DisableCorn(gameObject);
        OnBlowUp?.Invoke();
    }
}
