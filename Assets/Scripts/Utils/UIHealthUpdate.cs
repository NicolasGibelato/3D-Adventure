using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIHealthUpdate : MonoBehaviour
{
    public Image uiImage;
    public TextMeshProUGUI textMeshPro;
    [Header("Animation")]
    public bool checkPointActivedUI = true;
    public float duration = 1f;
    public Ease ease = Ease.OutBack;

    private Tween _currentTween;

    private void OnValidate()
    {
        if (uiImage == null) uiImage = GetComponent<Image>();
    }

    public void UpdadeValue(float f)
    {
        uiImage.fillAmount = f;
    }

    public void UpdateValue(float max, float current)
    {
        if (_currentTween != null) _currentTween.Kill();
        _currentTween = uiImage.DOFillAmount(1 - (current / max), duration).SetEase(ease);
    }


}