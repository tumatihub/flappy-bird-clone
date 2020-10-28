using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public enum GameState { PLAY, PAUSE, WAITING_FIRST_INPUT }

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreDisplay;
    [SerializeField] private Transform _startPlanePos;
    public Transform PlanePos;
    [SerializeField] private PlayerController _planePrefab;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Image _tapIcon;
    [SerializeField] private ScorePanel _scorePanel;
    private int _score;
    private int _bestScore;
    public GameState State;
    private Coroutine _coroutine;

    public UnityAction<int> OnScore;

    void Start()
    {
        State = GameState.PAUSE;
        LoadScore();
        StartNewGame();
    }

    private void LoadScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            _bestScore = PlayerPrefs.GetInt("BestScore");
        }
        else
        {
            _bestScore = 0;
            PlayerPrefs.SetInt("BestScore", _bestScore);
        }
    }

    public void Restart()
    {
        _scorePanel.HidePanel();
        StartNewGame();
    }

    public void Score()
    {
        _score += 1;
        OnScore?.Invoke(_score);
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
        _bestScore = Mathf.Max(_score, _bestScore);
        SaveScore();
        _scorePanel.UpdatePanel(_score, _bestScore);
        _spawner.StopSpawner();
        _coroutine = StartCoroutine(WaitForLastObstacle());

    }

    private void SaveScore()
    {
        var currentBestScore = PlayerPrefs.GetInt("BestScore");
        bool isSignedIn = GameJolt.API.GameJoltAPI.Instance.HasSignedInUser;
        
        if (_bestScore > currentBestScore && isSignedIn)
        {
            GameJolt.API.Scores.Add(_bestScore, $"{_bestScore}");
        }
        PlayerPrefs.SetInt("BestScore", _bestScore);
    }

    IEnumerator WaitForLastObstacle()
    {
        while (FindObjectOfType<Obstacle>() != null)
        {
            yield return new WaitForSeconds(1f);
        }
        State = GameState.PAUSE;
        _scorePanel.ShowPanel();
    }

    public void Play()
    {
        State = GameState.PLAY;
        _spawner.StartSpawner();
        _tapIcon.gameObject.SetActive(false);
    }

    [ContextMenu("Clear Save")]
    private void ClearSave()
    {
        PlayerPrefs.DeleteAll();
    }

}
