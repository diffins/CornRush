using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandle : MonoBehaviour
{
    [SerializeField] private GameObject _swipeArea;
    [SerializeField] private SwipeContainer _swipeContainer;
    [SerializeField] private CornPool _cornPool;

    private float _pressTime;
    private bool _isHolding;

    private bool _isDelay = true;
    private float _delayTime;
    private float _delay = 0.1f;

    private float _swipeAreaWidthScaler = 16.0f;


    private void FixedUpdate()
    { 
        if(Input.touchCount == 0)
            _isDelay = true;

        if (_isDelay)
            _delayTime += Time.fixedDeltaTime;

        if (Input.touchCount > 0 && _delayTime > _delay)
        {
            Touch touch = Input.GetTouch(0);
            _pressTime += Time.fixedDeltaTime;

            if (!_isHolding)
            {
                _isHolding = true;
                Vector3 p = touch.position;
                p.z = 20;
                Vector3 pos = Camera.main.ScreenToWorldPoint(p);

                _swipeArea.SetActive(true);
                Vector3 areaPos = new Vector3(pos.x - 30f, 0.1f, pos.z);
                _swipeArea.transform.position = areaPos;
            }
            else if(_isHolding)
            {
                if (Mathf.Abs(touch.deltaPosition.x) > 8.0f)
                {
                    foreach (CornController corn in _swipeContainer.Controllers)
                    {
                        corn.MoveOffset(touch.deltaPosition.x);
                    }
                    ResetSwipe();
                    return;
                }
                float _widthToTime = _pressTime / GameManager.Instance.Settings.TimeToMaxSwipeAreaWidth;

                float scaleX = Mathf.Lerp(0, _swipeAreaWidthScaler * GameManager.Instance.Settings.SwipeAreaMaxWidth, _widthToTime);
                Vector3 swipeScale = new Vector3(scaleX, _swipeArea.transform.localScale.y, _swipeArea.transform.localScale.z);
                _swipeArea.transform.localScale = swipeScale;
            }
        }
        else if(Input.touchCount == 0)
        {
            if(_pressTime != 0 && _delayTime > _delay)
            {
                foreach (GameObject corn in _cornPool.Corns)
                {
                    corn.GetComponent<CornController>().Jump();
                }
                ResetSwipe();
            }
        }
    }

    private void ResetSwipe()
    {
        _delayTime = 0;
        _isDelay = false;
        _swipeArea.SetActive(false);
        _swipeContainer.CleanList();
        _isHolding = false;
        _pressTime = 0;
        Vector3 swipeScale = new Vector3(0.4f, _swipeArea.transform.localScale.y, _swipeArea.transform.localScale.z);
        _swipeArea.transform.localScale = swipeScale;
    }
}
