using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornGroundChecker : MonoBehaviour
{
    public bool IsOnGround { get; private set; } = false;
    private string _groundTagName = "Floor";
    private GameObject _lastGround;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == _groundTagName)
        {
            IsOnGround = true;
            _lastGround = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == _groundTagName)
        {
            if(_lastGround == collision.gameObject)
                IsOnGround = false;
        }
    }

}
