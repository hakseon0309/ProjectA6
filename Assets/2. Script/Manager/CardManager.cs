using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;
using TMPro;
using DG.Tweening;

public class CardManager : MonoBehaviour
{
    // 싱글톤
    private static CardManager instance = null;
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
    public static CardManager Instance
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





    [Header("Transform Position")]
    public Transform deck;
    public Transform hand;
    public Transform discard;
    public Transform extinct;

    // 1장의 카드를 드로우 합니다.
    // 매개변수 way 0 = 가장 위에 있는 카드를 드로우
    // 매개변수 way 1 = 가장 아래에 있는 카드를 드로우
    // 매개변수 way 2 = 랜덤한 카드를 드로우
    public void Card_Draw(int way)
    {

        Transform fromTr = deck;
        int count = fromTr.childCount;
        Transform toTr = hand;
        GameObject card = null;
        int randomIndex = Random.Range(0, count);
        List<Vector3> cardsPos = new List<Vector3>();

        if (count != 0)
        {
            switch (way)
            {
                case 0: {card = fromTr.GetChild(count-1).gameObject;} 
                        {card.transform.parent = toTr.transform;} break;
                case 1: {card = fromTr.GetChild(0).gameObject;} 
                        {card.transform.parent = toTr.transform;} break;                
                case 2: {card = fromTr.GetChild(randomIndex).gameObject;}
                        {card.transform.parent = toTr.transform;} break;
            }
            Card_Sorting();
            cardsPos = Card_Alignment();
            for (int i = 0; i < count; i++)
            {
                // Card_Move(card, cardsPos[i]);
            }
        }
        else
        {
            Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
        }
    }

    public void Deck_SetUp()
    {
        int playerDeckCount = PlayerManager.Instance.playerDeck.Count;
        for (int i = 0; i < playerDeckCount; i++)
        {
            var card = Instantiate(PlayerManager.Instance.playerDeck[i], deck.transform);
            card.transform.localScale = Vector3.zero;
            card.transform.parent = deck.transform;
        }
    }

    public void DeckCycle()
    {
        if ( discard.childCount != 0 )
        {
            int discardCount = discard.childCount;
            for ( int i = 0; i < discardCount; i++ )
            {
                GameObject card = discard.GetChild(0).gameObject;
                card.transform.parent = deck.transform;
                card.transform.position = deck.position;
            }
            Debug.Log("Cycle !");
        }
        Debug.Log("Can't" + MethodBase.GetCurrentMethod().Name);
        
    }
    public void Shuffle()
    {
        int count = deck.childCount;
        List<Transform> childList = new List<Transform>(count);

        for (int i = 0; i < count; i++) {childList.Add(deck.GetChild(i));}
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(i, count);
            Transform temp = childList[i];
            childList[i] = childList[randomIndex];
            childList[randomIndex] = temp;
        }
        for (int i = 0; i < count; i++)
        {
            childList[i].SetSiblingIndex(i);
        }
    }


    
    // Only Top Draw
    public IEnumerator DrawPerTurn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            DrawCard(0);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void DiscardCard(int way)
    {
        Transform fromTr = hand;
        Transform toTr = discard;

        GameObject card = null;
        int randomIndex = Random.Range(0, count);

        if (count != 0)
        {
            switch (way)
            {
                case 0: {card = fromTr.GetChild(count-1).gameObject;} 
                        {card.transform.parent = toTr.transform;} break;
                case 1: {card = fromTr.GetChild(0).gameObject;} 
                        {card.transform.parent = toTr.transform;} break;                
                case 2: {card = fromTr.GetChild(randomIndex).gameObject;}
                        {card.transform.parent = toTr.transform;} break;
            }
            // CardAlignment();
        }
        else
        {
            Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
        }
    }
    public void ExtinctCard(int way)
    {
        Transform fromTr = hand;
        Transform toTr = extinct;

        GameObject card = null;
        int randomIndex = Random.Range(0, count);

        if (count != 0)
        {
            switch (way)
            {
                case 0: {card = fromTr.GetChild(count-1).gameObject;} 
                        {card.transform.parent = toTr.transform;} break;
                case 1: {card = fromTr.GetChild(0).gameObject;} 
                        {card.transform.parent = toTr.transform;} break;                
                case 2: {card = fromTr.GetChild(randomIndex).gameObject;}
                        {card.transform.parent = toTr.transform;} break;
            }
            // CardAlignment();
        }
        else
        {
            Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
        }
    }

    // Animation & Alignment
    [Header("DoTween")]
    public float doMoveSec;
    public float doScaleSec;



    public void Card_Move(GameObject card, Vector3 pos, bool useDotween, float dotweenTime)
    {
        if (useDotween)
        {
            card.transform.DOMove(pos, dotweenTime);
        }
        else
        {
            card.transform.position = pos;
        }
    }

    public void Card_Sorting()
    {
        if (hand.childCount != 0)
        {
            for (int i = 0; i < hand.childCount; i++)
            {
                var sort = hand.GetChild(i).GetComponent<SortingGroup>();
                sort.sortingOrder = i;
            }            
        }
        else
        {
            Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
        }   
    }

    public List<Vector3> Card_Alignment()
    {
        List<Vector3> list = new List<Vector3>();
        var count = hand.childCount;
        if (count != 0)
        {
            switch (count)
            {
                case 1:
                    list.Add(Vector3.zero);
                    break;
                case 2:
                    list.Add(new Vector3(-0.5f, 0f, 0f));
                    list.Add(new Vector3(0.5f, 0f, 0f));
                    break;
                case 3:
                    list.Add(new Vector3(-1f, 0f, 0f));
                    list.Add(new Vector3(0f, 0f, 0f));
                    list.Add(new Vector3(1f, 0f, 0f));
                    break;
                case 4:
                    list.Add(new Vector3(-1.5f, 0f, 0f));
                    list.Add(new Vector3(-0.5f, 0f, 0f));
                    list.Add(new Vector3(0.5f, 0f, 0f));
                    list.Add(new Vector3(1.5f, 0f, 0f));
                    break;
                case 5:
                    list.Add(new Vector3(-2f, 0f, 0f));
                    list.Add(new Vector3(-1f, 0f, 0f));
                    list.Add(new Vector3(0f, 0f, 0f));
                    list.Add(new Vector3(1f, 0f, 0f));
                    list.Add(new Vector3(2f, 0f, 0f));
                    break;
                case 6:
                    list.Add(new Vector3(-2.5f, 0f, 0f));
                    list.Add(new Vector3(-1.5f, 0f, 0f));
                    list.Add(new Vector3(-0.5f, 0f, 0f));
                    list.Add(new Vector3(0.5f, 0f, 0f));
                    list.Add(new Vector3(1.5f, 0f, 0f));
                    list.Add(new Vector3(2.5f, 0f, 0f));
                    break;
                case 7:
                    list.Add(new Vector3(-3f, 0f, 0f));
                    list.Add(new Vector3(-2f, 0f, 0f));
                    list.Add(new Vector3(-1f, 0f, 0f));
                    list.Add(new Vector3(0f, 0f, 0f));
                    list.Add(new Vector3(1f, 0f, 0f));
                    list.Add(new Vector3(2f, 0f, 0f));
                    list.Add(new Vector3(3f, 0f, 0f));
                    break;
                case 8:
                    list.Add(new Vector3(-3.5f, 0f, 0f));
                    list.Add(new Vector3(-2.5f, 0f, 0f));
                    list.Add(new Vector3(-1.5f, 0f, 0f));
                    list.Add(new Vector3(-0.5f, 0f, 0f));
                    list.Add(new Vector3(0.5f, 0f, 0f));
                    list.Add(new Vector3(1.5f, 0f, 0f));
                    list.Add(new Vector3(2.5f, 0f, 0f));
                    list.Add(new Vector3(3.5f, 0f, 0f));
                    break;
                case 9:
                    list.Add(new Vector3(-4f, 0f, 0f));
                    list.Add(new Vector3(-3f, 0f, 0f));
                    list.Add(new Vector3(-2f, 0f, 0f));
                    list.Add(new Vector3(-1f, 0f, 0f));
                    list.Add(new Vector3(0f, 0f, 0f));
                    list.Add(new Vector3(1f, 0f, 0f));
                    list.Add(new Vector3(2f, 0f, 0f));
                    list.Add(new Vector3(3f, 0f, 0f));
                    list.Add(new Vector3(4f, 0f, 0f));
                    break;
                case 10:
                    list.Add(new Vector3(-4.5f, 0f, 0f));
                    list.Add(new Vector3(-3.5f, 0f, 0f));
                    list.Add(new Vector3(-2.5f, 0f, 0f));
                    list.Add(new Vector3(-1.5f, 0f, 0f));
                    list.Add(new Vector3(-0.5f, 0f, 0f));
                    list.Add(new Vector3(0.5f, 0f, 0f));
                    list.Add(new Vector3(1.5f, 0f, 0f));
                    list.Add(new Vector3(2.5f, 0f, 0f));
                    list.Add(new Vector3(3.5f, 0f, 0f));
                    list.Add(new Vector3(4.5f, 0f, 0f));
                    break;
            }
        }
        return list;
    }
}
