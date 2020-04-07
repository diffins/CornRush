using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityRoad : MonoBehaviour
{
    [SerializeField] private List<GameObject> _roadElements;

    private float _step;
    private Vector3 _lastElementPosition;

    private void Start()
    {
        _step = Vector3.Distance(_roadElements[0].transform.position, _roadElements[1].transform.position);
        _lastElementPosition = _roadElements[_roadElements.Count - 1].transform.position;
    }

    public void RearrangeRoadElement()
    {
        Vector3 newPosition = _lastElementPosition - new Vector3(_step, 0, 0);
        _roadElements[0].transform.position = newPosition;
        _lastElementPosition = newPosition;


        // Перемещаем нулевой элемент в конец очереди
        GameObject go = _roadElements[0];
        _roadElements.Remove(go);
        _roadElements.Add(go);
    }
}
