using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private GameObject _finish;

    private List<GameObject> _obstaclesDisable = new List<GameObject>();
    private List<GameObject> _obstaclesActive = new List<GameObject>();

    private bool _spawnFinish = false;

    private float _rangeBetweenObstacles;
    private float _minRange = 30f;
    private float _maxRange = 100f;
    
    private float _distanceToSpawn = 170f;
    private float _nextSpawnPoint;

    private void Start()
    {
        _rangeBetweenObstacles = Mathf.Lerp(_maxRange, _minRange, GameManager.Instance.Settings.ObstacleDensity);
        _nextSpawnPoint = - 30f;

        foreach (Transform obstacle in transform)
        {
            _obstaclesDisable.Add(obstacle.gameObject);
        }
    }

    private void Update()
    {
        if(!_spawnFinish && Time.timeSinceLevelLoad > GameManager.Instance.Settings.LevelDuration)
        {
            _spawnFinish = true;
        }

        if(Mathf.Abs(_nextSpawnPoint - _cameraTransform.position.x) < _distanceToSpawn)
        {
            if(_spawnFinish)
            {
                SpawnFinish();
            }
            else
            {
                SpawnObstacle();
            }
        }
    }

    private void SpawnFinish()
    {
        _finish.SetActive(true);
        _finish.transform.position = new Vector3(_nextSpawnPoint, 0, 0);
        this.enabled = false;
    }

    private void SpawnObstacle()
    {
        if (_obstaclesDisable.Count == 0)
            return;

        int random = Random.Range(0, _obstaclesDisable.Count);

        GameObject go = _obstaclesDisable[random];

        go.SetActive(true);
        _obstaclesDisable.Remove(go);
        _obstaclesActive.Add(go);

        go.transform.position = new Vector3(_nextSpawnPoint, 0, 0);
        _nextSpawnPoint -= _rangeBetweenObstacles;
    }

    public void PutElementInPool(GameObject go)
    {
        _obstaclesActive.Remove(go);
        _obstaclesDisable.Add(go);
        go.SetActive(false);
    }
}
