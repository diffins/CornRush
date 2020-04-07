using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private float _offset = 15f;

    [SerializeField] private CornPool _cornPool;


    void Update()
    {
        if (_cornPool.Corns.Count == 0)
            return;

        Vector3 targetPosition = new Vector3(GetTargetPointX() + _offset, transform.position.y, transform.position.z);
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        transform.position = newPosition;
    }

    private float GetTargetPointX()
    {
        if(_cornPool.Corns.Count == 1)
        {
            return _cornPool.Corns[0].transform.position.x;
        }

        float min;
        float max;
        min = max = _cornPool.Corns[0].transform.position.x;

        foreach (GameObject corn in _cornPool.Corns)
        {
            float curX = corn.transform.position.x;

            if (curX > max)
                max = curX;
            if (curX < min)
                min = curX;
        }

        return Mathf.Lerp(min, max, .5f);
    }
}
