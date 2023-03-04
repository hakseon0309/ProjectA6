using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MySceneManager : MonoBehaviour
{
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

    public CanvasGroup FadeImg;

    public void ChangeScene(float fadeDuration)
    {
        FadeImg.DOFade(1f, fadeDuration)
        .OnStart( () =>
        {
            // 아래 레이어 레이캐스트 블락
            FadeImg.blocksRaycasts = true;
        })
        .OnComplete( () =>
        {
            // 로딩화면 띄우거나, 씬 로드 하거나..
        });
    }

    public void SceneFadeIn(float fadeDuration)
    {
        FadeImg.alpha = 1f;
        FadeImg.DOFade(0f, fadeDuration);
    }

    public void SceneFadeOut(float fadeDuration)
    {
        FadeImg.alpha = 0f;
        FadeImg.DOFade(1f, fadeDuration);
    }

}
