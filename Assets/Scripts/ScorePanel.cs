using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private Transform _showPos;
    [SerializeField] private Transform _hidePos;
    [SerializeField] private TMP_Text _scoreValue;
    [SerializeField] private TMP_Text _bestScoreValue;
    [SerializeField] private float _moveDelay;

    public void ShowPanel()
    {
        LeanTween.move(gameObject, _showPos.position, _moveDelay).setEase(LeanTweenType.easeOutCirc);
    }

    public void HidePanel()
    {
        LeanTween.move(gameObject, _hidePos.position, _moveDelay).setEase(LeanTweenType.easeInCirc);
    }

    public void UpdatePanel(int score, int bestScore)
    {
        _scoreValue.text = score.ToString();
        _bestScoreValue.text = bestScore.ToString();
    }
}
