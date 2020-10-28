using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private ParticleSystem _burstParticles;
    [SerializeField] private ParticleSystem _damageParticles;
    [SerializeField] private ParticleSystem _startSmokeParticles;
    [SerializeField] private ParticleSystem _rocksParticles;
    [SerializeField] private float _timeToMoveToPosition = 3f;
    private GameManager _gameManager;
    private AudioSource _audioSource;
    [SerializeField] AudioClip _rocksCollisionClip;


    private bool _isFalling;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        MoveToPosition();
    }


    // Update is called once per frame
    void Update()
    {
        if (_isFalling) return;

        switch (_gameManager.State)
        {
            case GameState.PLAY:
                if (Input.GetButtonDown("Tap"))
                {
                    Tap();
                }
                break;
            case GameState.WAITING_FIRST_INPUT:
                if (Input.GetButtonDown("Tap"))
                {
                    _rigidbody2D.gravityScale = 1;
                    Tap();
                    _gameManager.Play();
                }
                break;
            default:
                break;
        }

    }
    private void MoveToPosition()
    {
        var seq = LeanTween.sequence();
        seq.append(
            LeanTween.move(gameObject, _gameManager.PlanePos.position, _timeToMoveToPosition).setEase(LeanTweenType.easeOutCirc)
        );
        seq.append(() => {
            _startSmokeParticles.Stop();
            ChangePitch(.8f, .3f);
        });
    }

    private void ChangePitch(float toValue, float time)
    {
        var currentValue = _audioSource.pitch;
        LeanTween.value(gameObject, UpdatePitch, currentValue, toValue, time);
    }

    private void VaryPitch(float minValue, float maxValue, float time)
    {
        var currentValue = _audioSource.pitch;
        var seq = LeanTween.sequence();
        seq.append(
            LeanTween.value(gameObject, UpdatePitch, currentValue, maxValue, time/2)  
        );
        seq.append(
            LeanTween.value(gameObject, UpdatePitch, maxValue, minValue, time/2)
        );
    }

    private void UpdatePitch(float value)
    {
        _audioSource.pitch = value;
    }

    private void Tap()
    {
        VaryPitch(.8f, 1f, .2f);
        _rigidbody2D.velocity = new Vector2(0f, _verticalSpeed);
        _burstParticles.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            
            Die();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ObstacleArea") && !_isFalling)
        {
            _gameManager.Score();
        }
    }

    private void Die()
    {
        if (_isFalling) return;

        _audioSource.PlayOneShot(_rocksCollisionClip);
        ChangePitch(.7f, .5f);
        Instantiate(_rocksParticles, transform.position, Quaternion.identity);
        _damageParticles.Play();
        _isFalling = true;
        Destroy(gameObject, 2f);
        _gameManager.EndGame();
    }
}
