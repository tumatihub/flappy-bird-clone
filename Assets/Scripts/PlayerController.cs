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
    [SerializeField] private float _timeToMoveToPosition = 3f;
    private GameManager _gameManager;

    private bool _isFalling;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
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
        });
    }

    private void Tap()
    {
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

        _damageParticles.Play();
        _isFalling = true;
        Destroy(gameObject, 2f);
        _gameManager.EndGame();
    }
}
