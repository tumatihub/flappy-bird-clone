using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementPanel : MonoBehaviour
{
    [SerializeField] private Image _thumb;
    [SerializeField] private TMP_Text _msg;
    [SerializeField] private Transform _showPos;
    [SerializeField] private Transform _hidePos;
    [SerializeField] private float _showDuration = 2f;
    [SerializeField] private float _moveDelay = .5f;
    public void UpdatePanel(Sprite thumb, string msg)
    {
        _thumb.sprite = thumb;
        _msg.text = msg;
    }

    public void ShowAchievement()
    {
        StartCoroutine(ShowAchievementCoroutine());
    }

    IEnumerator ShowAchievementCoroutine()
    {
        Show();
        yield return new WaitForSeconds(_showDuration);
        Hide();
    }

    private void Show()
    {
        LeanTween.move(gameObject, _showPos.position, _moveDelay).setEase(LeanTweenType.easeOutCirc);
    }

    private void Hide()
    {
        LeanTween.move(gameObject, _hidePos.position, _moveDelay).setEase(LeanTweenType.easeInCirc);
    }
}
