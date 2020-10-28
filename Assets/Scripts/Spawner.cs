using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private float _startDelay = 3f;
    [SerializeField] private float _minDelay = 1f;
    [SerializeField] private float _delayDecrement = 0.5f;
    [SerializeField] private float _timeToSpeedUp = 10f;
    [SerializeField] private Obstacle _obstacle;
    [SerializeField] private float _yMin;
    [SerializeField] private float _yMax;

    private Coroutine _coroutine;
    private float _counter;


    private void Update()
    {
        if (_delay <= _minDelay) return;

        if (_counter <= 0)
        {
            _counter = _timeToSpeedUp;
            _delay = Mathf.Max(_delay - _delayDecrement, _minDelay);
        }
        else
        {
            _counter -= Time.deltaTime;
        }
    }

    private void Spawn()
    {
        Vector2 pos = new Vector2(transform.position.x, UnityEngine.Random.Range(_yMin, _yMax));
        Instantiate(_obstacle, pos, Quaternion.identity);
    }

    public void SetObstaclePrefab(Obstacle obstacle)
    {
        _obstacle = obstacle;
    }

    public void StartSpawner()
    {
        _delay = _startDelay;
        _counter = _timeToSpeedUp;
        _coroutine = StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(_delay);
        }
    }

    public void StopSpawner()
    {
        StopCoroutine(_coroutine);
    }
}
