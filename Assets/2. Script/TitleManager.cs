using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public Image titleBackground;
    public Image selectBackground;
    public TMP_Text touchToStart;
    private bool canStart;

    private void Start()
    {
        touchToStart.alpha = 0f;

        Color color = selectBackground.GetComponent<Image>().color;
        color.a = 0f; 

        titleBackground.transform.DOScale(1.6f,60f);
        MySceneManager.Instance.SceneFadeIn(3f);
        touchToStart.DOFade(1f,1f).SetDelay(3f)
        .OnComplete ( () =>
        {
            canStart = true;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(touchToStart.DOFade(0.5f,0.5f))
            .Append(touchToStart.DOFade(1f,0.5f))
            .SetLoops(-1);
        });        
    }

    public void OnClickTouchToStart()
    {
        if (canStart == true)
        {
        canStart = false;
        SceneManager.LoadScene("02Select");
        }
    }
}