using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementDisplay : MonoBehaviour
{
    [SerializeField] private Achievement _achievement;
    [SerializeField] private Image _thumb;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _minPoints;

    [SerializeField] private float _hideAlpha = .25f;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        UpdateDisplay();
    }

    void Start()
    {
        if (PlayerPrefs.HasKey(_achievement.PrefKey) && PlayerPrefs.GetInt(_achievement.PrefKey) == 1)
        {
            _canvasGroup.alpha = 1;
        }
        else
        {
            _canvasGroup.alpha = _hideAlpha;
        }
    }


    private void UpdateDisplay()
    {
        _thumb.sprite = _achievement.Thumb;
        _description.text = _achievement.Msg;
        _minPoints.text = $"{_achievement.MinScore}+";
    }
}
