using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;
    public Transform pos_Player;
    public Transform pos_Enemy;

    public Image FadePanel;
    public float fadeDuration = 1f;
    public float waitTime = 0.5f;
    
    public TextMeshProUGUI txt_MyTurn;

    void Start()
    {
        GameObject playerCard = Instantiate(player.playerCard, pos_Player);
        GameObject enemyCard = Instantiate(enemy.enemyCard, pos_Enemy);


    }

    IEnumerator SceneFadeOut(float sec)
    {
        FadePanel.gameObject.SetActive(true);
        FadePanel.color = new Color(0f, 0f, 0f, 0f);
        FadePanel.DOFade(1f, sec);
        yield return new WaitForSeconds(sec);
        FadePanel.gameObject.SetActive(false);
    }

    IEnumerator FadeInText()
    {
        txt_MyTurn.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        txt_MyTurn.DOFade(1f, 0.2f).SetEase(Ease.OutQuad);
        txt_MyTurn.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), 0.5f, 2, 0.5f); // 바운스 효과 추가
        yield return new WaitForSeconds(1.5f);
        txt_MyTurn.DOFade(0f, 0.3f).SetEase(Ease.OutQuad); // 0.2초 동안 페이드 인 효과 추가
        yield return new WaitForSeconds(1f);
        txt_MyTurn.gameObject.SetActive(false);
    }
}