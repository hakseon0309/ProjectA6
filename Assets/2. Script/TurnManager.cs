using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;
using DG.Tweening;

public class TurnManager : MonoBehaviour
{
    // 싱글톤
    private static TurnManager instance = null;
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
    public static TurnManager Instance
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



    [Header("Developer Mode")]
    [SerializeField] [Tooltip("Set Starting Turn Mode")] ETurnMode eTurnMode;
    [SerializeField] [Tooltip("Set Start Card Count")] int startCardCount;
    [SerializeField] [Tooltip("Fast Mode")] bool fastMode;
    


    [Header("Property")]
    public bool isLoading;  // True - 터치 방지
    public bool isPlayerTurn;
    private enum ETurnMode {Player, Enemy, Random}
    [SerializeField] WaitForSeconds delayStartGame = new WaitForSeconds(0.5f);
    [SerializeField] WaitForSeconds delayStartTurn = new WaitForSeconds(0.7f);



    [Header("Transform Position")]
    public Transform cardsToDraw;
    public Transform addedCards;
    public Transform discardedCards;
    public Transform removedCards;



    public IEnumerator Co_StartGame()
    {
        // FastMode 체크, Starting Turn 체크
        GameSetup();
        // PlayerDeck 프리팹을 모두 CardsToDraw 로 복제
        DeckSetUp();

        isLoading = true;

        for (int i = 0; i < startCardCount; i++)
        {
            yield return delayStartGame;
            DrawRandomCard();
        }
        StartCoroutine(Co_StartTurn());
    }

    private void GameSetup()
    {
        if (fastMode)
        {
            delayStartGame = new WaitForSeconds(0.05f);
            delayStartTurn = new WaitForSeconds(0.07f);
        }

        switch (eTurnMode)
        {
            case ETurnMode.Player:
                isPlayerTurn = true;
                break;
            case ETurnMode.Enemy:
                isPlayerTurn = false;
                break;
            case ETurnMode.Random:
                isPlayerTurn = Random.Range(0, 2) == 0;
                break;
        }
    }

    public void DeckSetUp()
    {
        int playerDeckCount = Player.Instance.playerDeck.Count;
        // int orderInLayer = 0;
        for (int i = 0; i < playerDeckCount; i++)
        {
            var card = Instantiate(Player.Instance.playerDeck[i], cardsToDraw.transform);
            card.transform.localScale = Vector3.zero;
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

    private IEnumerator Co_StartTurn()
    {
        isLoading = true;
        if (isPlayerTurn)
            GameManager.Instance.Notification("Player Turn");

        yield return delayStartTurn;
        DrawRandomCard();
        yield return delayStartTurn;
        isLoading = false;
    }

    public void EndTurn()
    {
        isPlayerTurn = !isPlayerTurn;
    }

    // Drawing Card System
    public void DrawTopCard()
    {
        Transform fromTr = cardsToDraw;
        Transform toTr = addedCards;

        if (fromTr.childCount != 0)
        {
            GameObject card = fromTr.GetChild(fromTr.childCount-1).gameObject;
            card.transform.parent = toTr.transform;
            MoveCard(card, toTr);
            Debug.Log(MethodBase.GetCurrentMethod().Name);
        }
        else
        {
            Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
        }
    }
    public void DrawBottomCard()
    {
        Transform fromTr = cardsToDraw;
        Transform toTr = addedCards;

        if (fromTr.childCount != 0)
        {

            GameObject card = fromTr.GetChild(0).gameObject;
            card.transform.parent = toTr.transform;
            MoveCard(card, toTr);
            Debug.Log(MethodBase.GetCurrentMethod().Name);
        }
        else
        {
            Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
        }
    }
    public void DrawRandomCard()
    {
        Transform fromTr = cardsToDraw;
        Transform toTr = addedCards;

        if (fromTr.childCount != 0)
        {
            int randomIndex = Random.Range(0, fromTr.childCount);
            GameObject card = fromTr.GetChild(randomIndex).gameObject;
            card.transform.parent = toTr.transform;
            MoveCard(card, toTr);
            Debug.Log(MethodBase.GetCurrentMethod().Name);
        }
        else
        {
            Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
        }
    }

    public void DiscardTopCard()
    {
        Transform fromTr = addedCards;
        Transform toTr = discardedCards;

        if (fromTr.childCount != 0)
        {
            GameObject card = fromTr.GetChild(fromTr.childCount-1).gameObject;
            card.transform.parent = toTr.transform;
            MoveCard(card, toTr, true);
            Debug.Log(MethodBase.GetCurrentMethod().Name);
        }
        else
        {
            Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
        }
    }
    public void DiscardBottomCard()
    {
        Transform fromTr = addedCards;
        Transform toTr = discardedCards;

        if (fromTr.childCount != 0)
        {
            GameObject card = fromTr.GetChild(0).gameObject;
            card.transform.parent = toTr.transform;
            MoveCard(card, toTr, true);
            Debug.Log(MethodBase.GetCurrentMethod().Name);
        }
        else
        {
            Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
        }
    }
    public void DiscardRandomCard()
    {
        Transform fromTr = addedCards;
        Transform toTr = discardedCards;

        if (fromTr.childCount != 0)
        {
            int randomIndex = Random.Range(0, fromTr.childCount);
            GameObject card = fromTr.GetChild(randomIndex).gameObject;
            card.transform.parent = toTr.transform;
            MoveCard(card, toTr, true);
            Debug.Log(MethodBase.GetCurrentMethod().Name);
        }
        else
        {
            Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
        }
    }
    
    public void RemoveTopCard()
    {
        Transform fromTr = addedCards;
        Transform toTr = removedCards;

        if (fromTr.childCount != 0)
        {
            GameObject card = fromTr.GetChild(fromTr.childCount-1).gameObject;
            card.transform.parent = toTr.transform;
            MoveCard(card, toTr, true);
            Debug.Log(MethodBase.GetCurrentMethod().Name);
        }
        else
        {
            Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
        }
    }
    public void RemoveBottomCard()
    {
        Transform fromTr = addedCards;
        Transform toTr = removedCards;       

        if (addedCards.childCount != 0)
        {
            GameObject card = addedCards.GetChild(0).gameObject;
            card.transform.parent = removedCards.transform;
            MoveCard(card, removedCards, true);
            Debug.Log(MethodBase.GetCurrentMethod().Name);
        }
        else
        {
            Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
        }
    }
    public void RemoveRandomCard()
    {
        Transform fromTr = addedCards;
        Transform toTr = removedCards;

        if (fromTr.childCount != 0)
        {
            int randomIndex = Random.Range(0, fromTr.childCount);
            GameObject card = fromTr.GetChild(randomIndex).gameObject;
            card.transform.parent = toTr.transform;
            MoveCard(card, toTr, true);
            Debug.Log(MethodBase.GetCurrentMethod().Name);
        }
        else
        {
            Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
        }
    }

    public void MoveCard(GameObject obj, Transform to)
    {
        obj.transform.DOMove(to.position, 1f);
        obj.transform.DOScale(1f, 1f);
    }
    public void MoveCard(GameObject obj, Transform to, bool disappear)
    {
        var temp = 1f;
        if (disappear == true)
        {
            temp = 0f;
        }
        obj.transform.DOMove(to.position, 1f);
        obj.transform.DOScale(temp, 1f);
    }

    public void DeckCycle()
    {
        if ( discardedCards.childCount != 0 )
        {
            int discardedCardsCount = discardedCards.childCount;
            for ( int i = 0; i < discardedCardsCount; i++ )
            {
                GameObject card = discardedCards.GetChild(0).gameObject;
                card.transform.parent = cardsToDraw.transform;
                card.transform.position = cardsToDraw.position;
            }
            Debug.Log("Cycle !");
        }
        Debug.Log("Can't" + MethodBase.GetCurrentMethod().Name);
        
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


}
