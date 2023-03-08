using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;
using DG.Tweening;

public class NotificationPanel : MonoBehaviour
{
    [Header("Notification Panel")]
    public Image background;
    public TMP_Text text;

    private void Start()
    {
        ScaleZero();
    }

    public void Show(string message)
    {
        text.text = message;
        Sequence sequence = DOTween.Sequence()
        .Append(transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutQuad))
        .AppendInterval(0.9f)
        .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad));
    }

    [ContextMenu("ScaleOne")]
    private void ScaleOne()
    {
        transform.localScale = Vector3.one;
    }

    [ContextMenu("ScaleZero")]
    public void ScaleZero()
    {
        transform.localScale = Vector3.zero;
    }
}
