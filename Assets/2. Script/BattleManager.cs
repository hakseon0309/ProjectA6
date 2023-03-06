using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public TMP_Text myTurn;
    public TMP_Text enemyTurn;

    public float seqIntervalReady;
    public float seqIntervalBetween;

    public List<GameObject> drawedCards;
    public List<GameObject> usedCards;
    public List<GameObject> extinctedCards;

    public Transform pos_Spawn;
    public Transform pos_Player;
    public Transform pos_Enemy;
    public Transform pos_Hand;

    private void Awake()
    {
        ReadyToBattle();
    }

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            DrawCards(Player.Instance.drawCount);
        }
        if (Input.GetButtonDown("Jump"))
        {
            EndTurn();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            MoveCards();
        }
    }

    public void Shuffle<T>(List<T> list)
    {
        for ( int i = 0; i < list.Count; i++ )
        {
            int k = Random.Range(0, list.Count);
            T value = list[i];  
            list[i] = list[k];  
            list[k] = value;  
        }
    }
    public void DrawCards(int count)
    {
        // 현재 플레이어덱의 카드 갯수가 뽑아야할 카드 갯수보다 같거나 많으면
        if (Player.Instance.playerDeck.Count >= count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject card = Player.Instance.playerDeck[0];
                drawedCards.Add(card);
                Player.Instance.playerDeck.RemoveAt(0);
            }
            ///// 두트윈
        }

        // 현재 플레이어덱의 카드 갯수가 뽑아야할 카드 갯수보다 적으면
        else if (Player.Instance.playerDeck.Count < count)
        {
            // 후에 덱 순환 후 추가 드로우할 갯수 저장
            int diff = count - Player.Instance.playerDeck.Count;

            // 일단 남아있는 플레이어덱 카드 갯수만큼 드로우
            int tempPlayerDeck = Player.Instance.playerDeck.Count;
            for (int i = 0; i < tempPlayerDeck; i++)
            {
                GameObject card = Player.Instance.playerDeck[0];
                drawedCards.Add(card);
                Player.Instance.playerDeck.RemoveAt(0);
            }
            ///// 두트윈

            // 사용한 카드들을 플레이어덱으로 이동
            int tempUsedCards = usedCards.Count;
            for (int i = 0; i < tempUsedCards; i++)
            {
                GameObject card = usedCards[0];
                Player.Instance.playerDeck.Add(card);
                usedCards.RemoveAt(0);
            }
            // Player.Instance.playerDeck을 셔플
            Shuffle(Player.Instance.playerDeck);

            // 덱 순환 완료 후 부족했던만큼 추가 드로우
            for (int i = 0; i < diff; i++)
            {
                if (Player.Instance.playerDeck.Count == 0)
                {
                    Debug.Log("카드 없음");
                    break;
                }
                GameObject card = Player.Instance.playerDeck[0];
                drawedCards.Add(card);
                Player.Instance.playerDeck.RemoveAt(0);
            }
            ///// 두트윈
        }
    }

    public void MoveCards()
    {
        for ( int i = 0; i < drawedCards.Count; i++ )
        {
            GameObject card = Instantiate(drawedCards[i], pos_Spawn.position, Quaternion.identity);
            card.transform.SetParent(pos_Hand, false);
        }
    }

    public void EndTurn()
    {
        int temp = drawedCards.Count;
        for (int i = 0; i < temp; i++)
        {
            GameObject card = drawedCards[0];
            usedCards.Add(card);
            drawedCards.RemoveAt(0);
        }
    }
    public void ReadyToBattle()
    {
        Sequence seq = DOTween.Sequence();
        seq.OnStart( () =>
        {
            MySceneManager.Instance.SceneFadeIn(1.5f);
        });
        seq.AppendInterval(seqIntervalReady);
        seq.AppendCallback( () =>
        {
            MySceneManager.Instance.BackgroundShadowStart(1.5f);
            TxtMyTurnStart(1.5f);
        });
        seq.AppendInterval(seqIntervalBetween);
        seq.AppendCallback( () =>
        {
            MySceneManager.Instance.BackgroundShadowEnd(1.5f);
            TxtMyTurnEnd(1.5f);
        });
    }
    public void TxtMyTurnStart(float fadeDuration)
    {
        myTurn.gameObject.SetActive(true);
        myTurn.alpha = 0f;
        myTurn.DOFade(1f,fadeDuration);
    }
    public void TxtMyTurnEnd(float fadeDuration)
    {
        myTurn.DOFade(0f,fadeDuration)
        .OnComplete( () => {
            myTurn.gameObject.SetActive(false);
        });
    }
}
