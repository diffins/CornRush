using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeContainer : MonoBehaviour
{
    public List<CornController> Controllers { get; private set; } = new List<CornController>();
    private string _cornTag = "Corn";

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == _cornTag)
        {
            Controllers.Add(other.gameObject.GetComponent<CornController>());
        }
    }

    public void CleanList()
    {
        Controllers.Clear();
    }
}
