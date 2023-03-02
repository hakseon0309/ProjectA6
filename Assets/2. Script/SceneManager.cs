using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneManager : MonoBehaviour
{
    public Image fadeController;
    public float duration;

    public void SceneFadeOut()
    {
        fadeController.DOFade(1f, duration);
    }

    public void SceneFadeIn()
    {
        fadeController.DOFade(0f, duration);
    }
}
