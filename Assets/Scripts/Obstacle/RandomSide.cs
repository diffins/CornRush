using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSide : MonoBehaviour
{
    private void OnEnable()
    {
        int r = Random.Range(0, 2);

        if(r == 0)
        {
            transform.localScale = new Vector3(1, 1, -1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
