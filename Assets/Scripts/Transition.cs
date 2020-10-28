using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Transition : MonoBehaviour
{
    [SerializeField] private Image _image;
    private CanvasGroup _canvasGroup;
    [SerializeField] private bool _hasEntrance = true;
    [SerializeField] private bool _hasExit = true;

    [SerializeField] private float _alphaDelay = 1f;

    public UnityEvent OnFinishedEntrance;
    public UnityEvent OnFinishedExit;

    void Start()
    {
        _canvasGroup = _image.GetComponent<CanvasGroup>();

        if (_hasEntrance)
        {
            RunEntrance();
        }
    }

    public void RunEntrance()
    {
        RunTransition(1f, 0f, LeanTweenType.easeInCirc, _alphaDelay, OnFinishedEntrance);
    }

    public void RunExit()
    {
        RunTransition(0f, 1f, LeanTweenType.easeOutCirc, _alphaDelay, OnFinishedExit);
    }

    public void RunExit(UnityAction callback)
    {
        OnFinishedExit.AddListener(callback);
        RunTransition(0f, 1f, LeanTweenType.easeOutCirc, _alphaDelay, OnFinishedExit);
    }

    private void RunTransition(float startAlpha, float endAlpha, LeanTweenType easeMode, float time, UnityEvent callbackEvent)
    {
        _canvasGroup.alpha = startAlpha;

        var seq = LeanTween.sequence();
        seq.append(
            LeanTween.alphaCanvas(_canvasGroup, endAlpha, time).setEase(easeMode).setIgnoreTimeScale(true)
        );
        seq.append(() =>
        {
            callbackEvent.Invoke();
        }
        );
    }
}