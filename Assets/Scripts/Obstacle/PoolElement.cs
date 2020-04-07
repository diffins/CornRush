using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolElement : MonoBehaviour
{
    private ObstacleSpawner _obstacleSpawner;
    private string _cameraTag = "MainCamera";

    private void Start()
    {
        _obstacleSpawner = GetComponentInParent<ObstacleSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == _cameraTag)
        {
            if(other.transform.position.x < transform.position.x)
                _obstacleSpawner.PutElementInPool(gameObject);
        }
    }
}
