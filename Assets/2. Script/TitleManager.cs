using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public SceneManager sceneManager;

    public Image background;
    public float ToScale;
    public float durationScale;

    public TMP_Text start;
    public float durationAlpha;

    public bool canStart = false;

    private void Start()
    {
        sceneManager.SceneFadeIn();
        background.transform.DOScale(ToScale, durationScale);
        start.alpha = 0f;
        start.DOFade(1f, 1f).SetDelay(3f)
        .OnComplete( () => Sequence_Start() );
        StartCoroutine(Btn_Start());
    }

    private void Update()
    {
        if ( canStart == true && Input.GetMouseButtonDown(0))
        {
            sceneManager.SceneFadeOut();

        }
    }

    private void Sequence_Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(start.DOColor(new Color(1, 1, 1, 0.5f), durationAlpha));
        sequence.Append(start.DOColor(new Color(1, 1, 1, 1), durationAlpha));
        sequence.SetLoops(-1);
    }

    IEnumerator Btn_Start()
    {
        yield return new WaitForSeconds(3f);
        canStart = true;

    }

    IEnumerator ToSelectScene()
    {
        sceneManager.SceneFadeOut();
        yield return new WaitForSeconds(1f);
    }
}