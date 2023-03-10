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

    public float fadeSec = 3f;

    public TMP_Text touchToStart;
    public bool canStart;

    public Sequence sq_Title;
    public Sequence sq_TitleSelect;

    private void Start()
    {
        // 처음에 안보여야 할 것들
        touchToStart.alpha = 0f;

        selectBackground.color = new Color(selectBackground.color.r, selectBackground.color.g, selectBackground.color.b, 0f);
        selectBackground.gameObject.SetActive(false);

        // 타이틀 화면 점점 커지게
        titleBackground.transform.DOScale(1.6f,60f);
        // 씬이 시작하자마자 페이드 인
        MySceneManager.Instance.SceneFadeIn(fadeSec);

        // 페이드 인 기다렸다가 Touch To Start 페이드 인
        touchToStart.DOFade(1f,1f).SetDelay(fadeSec)
        .OnComplete ( () =>
        {
            // 이제 터치하면 메인 화면으로 이동 가능
            canStart = true;
            // Touch To Start 가 깜빡거리게
            sq_Title = DOTween.Sequence();
            sq_Title.Append(touchToStart.DOFade(0.5f,0.5f))
                    .Append(touchToStart.DOFade(1f,0.5f))
                    .SetLoops(-1);
        });             
    }

    public void OnClickTouchToStart()
    {
        if (canStart == true)
        {
            canStart = false;
            sq_Title.Kill();
            touchToStart.gameObject.SetActive(false);
            selectBackground.gameObject.SetActive(true);
            sq_TitleSelect = DOTween.Sequence();
            sq_TitleSelect.Append(selectBackground.DOFade(0.4f,1f));
        }
    }
}