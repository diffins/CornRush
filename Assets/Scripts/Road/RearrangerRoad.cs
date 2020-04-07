using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearrangerRoad : MonoBehaviour
{
    private InfinityRoad _infinityRoad;
    private string _cameraTag = "MainCamera";

    private void Start()
    {
        _infinityRoad = GetComponentInParent<InfinityRoad>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _cameraTag)
        {
            _infinityRoad.RearrangeRoadElement();
        }
    }
}
