using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MySceneManager : MonoBehaviour
{
    // 싱글톤
    private static MySceneManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static MySceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }



    public CanvasGroup fadeImg;
    public float shadowFade;

    public void SceneFadeIn(float fadeDuration)
    {
        fadeImg.gameObject.SetActive(true);
        fadeImg.alpha = 1f;
        fadeImg.DOFade(0f, fadeDuration)
        .OnComplete( () => {
            fadeImg.gameObject.SetActive(false);
        });
    }
    public void SceneFadeOut(float fadeDuration)
    {
        fadeImg.gameObject.SetActive(true);
        fadeImg.alpha = 0f;
        fadeImg.DOFade(1f, fadeDuration);
    }
    public void BackgroundShadowStart(float fadeDuration)
    {
        fadeImg.gameObject.SetActive(true);
        fadeImg.alpha = 0f;
        fadeImg.DOFade (shadowFade, fadeDuration);
    }
    public void BackgroundShadowEnd(float fadeDuration)
    {
        fadeImg.DOFade (0f, fadeDuration)
        .OnComplete( () => {
            fadeImg.gameObject.SetActive(false);
        });
    }    
}
