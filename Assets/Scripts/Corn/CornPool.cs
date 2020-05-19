using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornPool : MonoBehaviour
{
    public int CornMaxCount { get; private set; }
    public List<GameObject> Corns
    {
        get { return _cornActive; }
        protected set { }
    }

    public event Action OnAllCornsDisable;

    private List<GameObject> _cornActive = new List<GameObject>();
    private List<GameObject> _cornDisable = new List<GameObject>();


    private void Start()
    {
        foreach (Transform corn in transform)
        {
            _cornActive.Add(corn.gameObject);
        }

        CornMaxCount = _cornActive.Count;
    }

    public void DisableCorn(GameObject corn)
    {
        _cornActive.Remove(corn);
        _cornDisable.Add(corn);
        corn.SetActive(false);

        if(_cornActive.Count == 0)
            OnAllCornsDisable?.Invoke();
    }
}
