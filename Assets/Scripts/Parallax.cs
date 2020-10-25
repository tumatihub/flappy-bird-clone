using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private SpriteRenderer _firstImage;
    [SerializeField] private SpriteRenderer _secondImage;
    private float _maxDistance;

    void Start()
    {
        _maxDistance = _secondImage.transform.position.x;
    }

    void Update()
    {
        _firstImage.transform.position = new Vector2(_firstImage.transform.position.x - _speed * Time.deltaTime, _firstImage.transform.position.y);
        _secondImage.transform.position = new Vector2(_secondImage.transform.position.x - _speed * Time.deltaTime, _secondImage.transform.position.y);

        if (_firstImage.transform.position.x <= -_maxDistance)
        {
            _firstImage.transform.position = new Vector2(_maxDistance, _firstImage.transform.position.y);
            _secondImage.transform.position = new Vector2(0f, _secondImage.transform.position.y);
        }

        if (_secondImage.transform.position.x <= -_maxDistance)
        {
            _secondImage.transform.position = new Vector2(_maxDistance, _secondImage.transform.position.y);
            _firstImage.transform.position = new Vector2(0f, _firstImage.transform.position.y);
        }
    }
}
