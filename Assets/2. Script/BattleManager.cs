using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

// 전투 시스템을 관리하는 클래스
public class BattleManager : MonoBehaviour
{
    // 페이드 인/아웃 등 트랜지션에서 초 간격을 설정할 변수
    public float seqIntervalReady;            // '씬 시작 -> 페이드 인' 후에 처음 기다릴 시간
    public float seqIntervalBetween;          // 두트윈 시퀀스 사이 간격
    
    // 텍스트 변수
    public TMP_Text myTurn;                   // My Turn 텍스트
    public TMP_Text enemyTurn;                // Enemy Turn 텍스트

    public Transform cardsToDraw;
    public Transform addedCards;
    public Transform discardedCards;
    public Transform removedCards;

    // 테스트로 추가할 카드
    public GameObject card03;
    public GameObject card04;
    public GameObject card05;
    public GameObject card06;

    // 현재 상태
    public bool canControl = false;           // 조작 가능한 상태인지 확인
    public bool playerTurn = false;           // 플레이어턴 상태인지 확인

    private void Awake()
    {
        // // 씬 시작 후 기본 화면 진행
        // ReadyToBattle();
        SetUp();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    // sec 간격으로 카드를 생성하고 손으로 가져옴
    // public IEnumerator MoveToHandCards(float sec)
    // {
    //     int temp = drawedCards.Count;
    //     for ( int i = temp - 1; i >= 0; i-- )
    //     {
    //         GameObject card = Instantiate(drawedCards[i], pos_Spawn.position, Quaternion.identity);
    //         list_HandedCards.Add(drawedCards[i]);
    //         drawedCards.Remove(drawedCards[i]);
    //         card.transform.parent = handedCards.transform;
    //         card.transform.DOMove(pos_Hand.position, 1f);
    //         yield return new WaitForSeconds(sec);
    //     }
    // }

    // public void MoveToUsedCards()
    // {
    //     foreach (Transform child in handedCards)
    //     {
    //         if ( child.name.Contains("Card") )
    //         {
    //             usedCards.Add(child.gameObject);
    //             child.gameObject.transform.DOMove(pos_Used.position, 1f)
    //             .OnComplete ( () => {Destroy(child.gameObject);});
    //         }
    //     }


    //     int temp = drawedCards.Count;
    //     for (int i = 0; i < temp; i++)
    //     {
    //         GameObject card = drawedCards[0];
    //         usedCards.Add(card);
    //         drawedCards.RemoveAt(0);
    //     }
    // }

    // PlayerDeck의 모든 카드를 cardsToDraw의 자식으로 생성
    // 동시에 크기와 알파값을 0으로



    ///////////////////////// 오브젝트 풀링 타입 카드 순환 시스템

    public void SetUp()
    {
        int playerDeckCount = Player.Instance.playerDeck.Count;
        // int orderInLayer = 0;
        for ( int i = 0; i < playerDeckCount; i++ )
        {
            var card = Instantiate( Player.Instance.playerDeck[0], cardsToDraw.transform );
            card.transform.localScale = Vector3.zero;
            Color color = card.GetComponentInChildren<SpriteRenderer>().color;
            color.a = 0f;
            card.transform.parent = cardsToDraw.transform;

            // // 카드들의 Sorting
            // Renderer renderer = card.GetComponentInChildren<Renderer>();
            // if (renderer != null)
            // {
            //     renderer.sortingOrder = orderInLayer;
            // }
            // orderInLayer += 10;
        }
    }

    public void DrawingCardSystem( int count )
    {
        // 조건 분기 1: 뽑을 카드 더미가 뽑아야할 카드보다 같거나 많음
        if ( cardsToDraw.childCount >= count )
        {
            DrawRandomCards( count );
        }
        // 조건 분기 2: 뽑을 카드 더미보다 뽑아야할 카드가 많음
        else
        {
            // diff만큼 후에 추가로 뽑을거임
            int diff = count - cardsToDraw.childCount;
            int temp = cardsToDraw.childCount;
            // 일단 있는 카드 다 뽑음
            DrawRandomCards( temp );
            // 사용한 카드 다 가져옴
            CycleDeck();
            // 나머지 뽑자
            DrawRandomCards( diff );
        }
    }

    public void DrawRandomCards( int count )
    {
        for ( int i = 0; i < count; i++ )
        {
            if ( cardsToDraw.childCount == 0 )
            {
                Debug.Log("CardsToDraw에 뽑을 카드가 없음");
                break;
            }
            int ctd = cardsToDraw.childCount;
            int randomIndex = Random.Range(0, ctd);
            var card = cardsToDraw.GetChild( randomIndex ).gameObject;
            card.transform.parent = addedCards.transform;
            card.transform.DOMove( addedCards.position, 1f );
            card.transform.DOScale( 1f, 1f );
            card.GetComponentInChildren<SpriteRenderer>().DOFade( 1f, 1f );
        }
        Debug.Log("DrawR !");
    }

    // public void DrawNoRandomCards( int count )
    // {
    //     int temp = cardsToDraw.childCount;
    //     for ( int i = temp - 1; i > count; i-- )
    //     {
    //         var card = cardsToDraw.GetChild( i ).gameObject;
    //         card.transform.parent = addedCards.transform;
    //         card.transform.DOMove( addedCards.position, 1f );
    //         card.transform.DOScale( 1f, 1f );
    //         card.GetComponentInChildren<SpriteRenderer>().DOFade( 1f, 1f );
    //     }
    // }

    public void DiscardCardsAll()
    {
        int addedCardsCount = addedCards.childCount;
        for ( int i = 0; i < addedCardsCount; i++ )
        {
            var card = addedCards.GetChild( 0 ).gameObject;
            card.transform.parent = discardedCards.transform;
            card.transform.DOMove( discardedCards.position, 1f );
            card.transform.DOScale( 0f, 1f );
            card.GetComponentInChildren<SpriteRenderer>().DOFade( 0f, 1f );
        }
        Debug.Log("Discard !");
    }

    public void RemovedCardsAll()
    {
        int addedCardsCount = addedCards.childCount;
        for ( int i = 0; i < addedCardsCount; i++ )
        {
            var card = addedCards.GetChild( 0 ).gameObject;
            card.transform.parent = removedCards.transform;
            card.transform.DOMove( removedCards.position, 1f );
            card.transform.DOScale( 0f, 1f );
            card.GetComponentInChildren<SpriteRenderer>().DOFade( 0f, 1f );
        }
        Debug.Log("Remove !");
    }

    public void CycleDeck()
    {
        if ( discardedCards.childCount != 0 )
        {
            int discardedCardsCount = discardedCards.childCount;
            for ( int i = 0; i < discardedCardsCount; i++ )
            {
                var card = discardedCards.GetChild( 0 ).gameObject;
                card.transform.parent = cardsToDraw.transform;
                card.transform.position = cardsToDraw.position;
            }
            Debug.Log("Cycle !");
        }
        Debug.Log("카드가 더 없어서 못섞어 !");
        
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
    

    // 테스트용 버튼에 들어갈 함수들
    public void TestAddCardToCardsToDraw()
    {
        var card = Instantiate( card03, cardsToDraw.transform );
        card.transform.parent = cardsToDraw.transform;
    }
    public void TestAddCardToDiscardedCards()
    {
        var card = Instantiate( card03, discardedCards.transform );
        card.transform.parent = discardedCards.transform;
    }
    public void TestAddCardToRemovedCards()
    {
        var card = Instantiate( card03, removedCards.transform );
        card.transform.parent = removedCards.transform;
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
