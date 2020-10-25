using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum GameState { PLAY, PAUSE, WAITING_FIRST_INPUT }

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreDisplay;
    [SerializeField] private Transform _startPlanePos;
    public Transform PlanePos;
    [SerializeField] private PlayerController _planePrefab;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Image _tapIcon;
    private int _score;
    public GameState State;
    private Coroutine _coroutine;

    void Start()
    {
        State = GameState.PAUSE;
        StartNewGame();
    }

    private void Update()
    {
        if (State == GameState.PAUSE && Input.GetKeyDown(KeyCode.R))
        {
            StartNewGame();
        }    
    }

    public void Score()
    {
        _score += 1;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        _scoreDisplay.text = _score.ToString();
    }

    public void StartNewGame()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        State = GameState.WAITING_FIRST_INPUT;
        _score = 0;
        UpdateDisplay();
        Instantiate(_planePrefab, _startPlanePos.position, Quaternion.identity);
        _tapIcon.gameObject.SetActive(true);
    }

    public void EndGame()
    {
        _spawner.StopSpawner();
        _coroutine = StartCoroutine(WaitForLastObstacle());

    }
    IEnumerator WaitForLastObstacle()
    {
        Debug.Log("Start Coroutine");
        while (FindObjectOfType<Obstacle>() != null)
        {
            yield return new WaitForSeconds(1f);
        }
        State = GameState.PAUSE;
        Debug.Log("End Coroutine");
    }

    public void Play()
    {
        State = GameState.PLAY;
        _spawner.StartSpawner();
        _tapIcon.gameObject.SetActive(false);
    }
}
