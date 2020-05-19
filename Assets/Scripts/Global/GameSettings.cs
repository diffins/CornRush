using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [Tooltip("Время нагрева зерна")]
    [SerializeField] private float _timeToBlowUp;
    public float TimeToBlowUp
    {
        get { return _timeToBlowUp; }
        protected set { }
    }

    [Tooltip("Длительность уровня (в секундах)")]
    [SerializeField] private float _levelDuration;
    public float LevelDuration
    {
        get { return _levelDuration; }
        protected set { }
    }

    [Tooltip("Плотность препятствий")]
    [Range(0, 1)]
    [SerializeField] private float _obstacleDensity;
    public float ObstacleDensity
    {
        get { return _obstacleDensity; }
        protected set { }
    }

    [Tooltip("Мощность взрыва")]
    [Range(0, 1)]
    [SerializeField] private float _blowPower;
    public float BlowPower
    {
        get { return _blowPower; }
        protected set { }
    }

    [Tooltip("Сила прыжка")]
    [Range(0, 1)]
    [SerializeField] private float _jumpPower;
    public float JumpPower
    {
        get { return _jumpPower; }
        protected set { }
    }

    [Tooltip("Сила смещения в сторону")]
    [Range(0, 1)]
    [SerializeField] private float _offsetPower;
    public float OffsetPower
    {
        get { return _offsetPower; }
        protected set { }
    }

    [Tooltip("Скорость движения зёрен вперед")]
    [Range(0, 1)]
    [SerializeField] private float _cornForwardSpeed;
    public float CornForwardSpeed
    {
        get { return _cornForwardSpeed; }
        protected set { }
    }

    [Tooltip("Максимальная ширина до которой будет увеличиваться зона влияния свайпа")]
    [Range(0, 1)]
    [SerializeField] private float _swipeAreaMaxWidth;
    public float SwipeAreaMaxWidth
    {
        get { return _swipeAreaMaxWidth; }
        protected set { }
    }

    [Tooltip("Время в секундах для полного увеличения зоны свайпа")]
    [SerializeField] private float _timeToMaxSwipeAreaWidth;
    public float TimeToMaxSwipeAreaWidth
    {
        get { return _timeToMaxSwipeAreaWidth; }
        protected set { }
    }

    //[Tooltip("Время в секундах после которого нажатие считается долгим")]
    //[SerializeField] private float _pressTime;
    //public float PressTime
    //{
    //    get { return _pressTime; }
    //    protected set { }
    //}


    [Tooltip("Нагреваются ли зёрна от боковых стенок")]
    [SerializeField] private bool _isWallsAreHot;
    public bool IsWallsAreHot
    {
        get { return _isWallsAreHot; }
        protected set { }
    }
}
