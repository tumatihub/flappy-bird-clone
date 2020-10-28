using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private List<Environment> _environments = new List<Environment>();
    [SerializeField] private SpriteRenderer _bottomGroundFirstImage;
    [SerializeField] private SpriteRenderer _bottomGroundSecondImage;
    [SerializeField] private SpriteRenderer _topGroundFirstImage;
    [SerializeField] private SpriteRenderer _topGroundSecondImage;
    [SerializeField] private Spawner _spawner;

    void Start()
    {
        Environment env = _environments[Random.Range(0, _environments.Count)];
        _bottomGroundFirstImage.sprite = env.Ground;
        _bottomGroundSecondImage.sprite = env.Ground;
        _topGroundFirstImage.sprite = env.Ground;
        _topGroundSecondImage.sprite = env.Ground;

        _spawner.SetObstaclePrefab(env.ObstaclePrefab);
    }

}
