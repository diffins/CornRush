using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public int FinalCornCout { get; private set; }
    public event Action OnEndRound;

    private string _cornTag = "Corn";
    private CornPool _cornPool;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == _cornTag)
        {
            FinalCornCout++;

            other.gameObject.GetComponent<CornController>().enabled = false;
            other.gameObject.GetComponent<CornForwardMover>().enabled = false;
            other.gameObject.GetComponent<CornDamage>().enabled = false;

            if (_cornPool == null)
            {
                _cornPool = other.gameObject.GetComponentInParent<CornPool>();

                foreach (GameObject corn in _cornPool.Corns)
                {
                    corn.GetComponent<CornBlowUp>().OnBlowUp += CheckEnd;
                }
            }
            
            CheckEnd();
        }
    }

    private void CheckEnd()
    {
        if(FinalCornCout == _cornPool.Corns.Count)
        {
            OnEndRound?.Invoke();
        }
    }
}
