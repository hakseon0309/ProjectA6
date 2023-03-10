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





    // CardManager

    [Header("Transform Position")]
    public Transform cardsToDraw;
    public Transform addedCards;
    public Transform discardedCards;
    public Transform removedCards;
    public Transform addedCardsLeft;
    public Transform addedCardsRight;

    // SetUp
    public void DeckSetUp()
    {
        int playerDeckCount = PlayerManager.Instance.playerDeck.Count;
        for (int i = 0; i < playerDeckCount; i++)
        {
            var card = Instantiate(PlayerManager.Instance.playerDeck[i], cardsToDraw.transform);
            card.transform.localScale = Vector3.zero;
            card.transform.parent = cardsToDraw.transform;
        }
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

    // Drawing Card System

    public void DrawCard(string method)
    {
        Transform fromTr = cardsToDraw;
        Transform toTr = addedCards;
        switch (method)
        {
            case "0":
                if (fromTr.childCount != 0)
                {
                    GameObject card = fromTr.GetChild(fromTr.childCount-1).gameObject;
                    card.transform.parent = toTr.transform;
                    MoveCard(card, toTr, false);
                    Debug.Log(MethodBase.GetCurrentMethod().Name);
                }
                else
                {
                    Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
                }
            break;
            

        }
    }

    public void DrawTopCard()
    {
        Transform fromTr = cardsToDraw;
        Transform toTr = addedCards;

        if (fromTr.childCount != 0)
        {
            GameObject card = fromTr.GetChild(fromTr.childCount-1).gameObject;
            card.transform.parent = toTr.transform;
            MoveCard(card, toTr, false);
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
            MoveCard(card, toTr, false);
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
            MoveCard(card, toTr, false);
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



    // Animation & Alignment

    [Header("DoTween")]
    public float doMoveSec;
    public float doScaleSec;

    public void MoveCard(GameObject obj, Transform to, bool disappear)
    {
        var temp = 1f;
        if (disappear == true)
        {
            temp = 0f;
        }
        obj.transform.DOMove(to.position, doMoveSec).SetEase(Ease.Linear);
        obj.transform.DOScale(temp, doScaleSec).SetEase(Ease.Linear);
    }
    public void MoveTransform(PRS prs, bool useDotween, float dotweenSec = 0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotweenSec);
            transform.DORotateQuaternion(prs.rot, dotweenSec);
            transform.DOScale(prs.scale, dotweenSec);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
    public void SetSortingLayer()
    {
        if (addedCards.childCount != 0)
        {
            for (int i = 0; i < addedCards.childCount; i++)
            {
                var sort = addedCards.GetChild(i).GetComponent<SortingGroup>();
                sort.sortingOrder = i;
            }            
        }
        Debug.Log("Can't " + MethodBase.GetCurrentMethod().Name);
    }
    public void CardAlignment()
    {
        List<PRS> originPRSs = new List<PRS>();

        originPRSs = RoundAlignment(addedCardsLeft, addedCardsRight, addedCards.childCount, 0.5f, Vector3.one * 1.5f);
        
        var addedCardsCount = addedCards.childCount;
        for (int i = 0; i < addedCardsCount; i++)
        {
            var card = addedCards.GetChild(i).gameObject.GetComponent<UseableCard>();
            card.originPRS = originPRSs[i];
            card.MoveTransform(card.originPRS, true, 1f);
        }
    }
    private List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        switch (objCount)
        {
            case 1: objLerps = new float[] {0.5f}; break;
            case 2: objLerps = new float[] {0.27f, 0.73f}; break;
            case 3: objLerps = new float[] {0.1f, 0.5f, 0,9f}; break;
            default :
                float interval = 1f / (objCount - 1);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }

        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Quaternion.identity;
            if (objCount >= 4)
            {
                float curve = Mathf.Sqrt(MathF.Pow(height, 2) - MathF.Pow(objLerps[i] - 0.5f, 2));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }
            results.Add(new PRS(targetPos, targetRot, scale));
        }
        return results;
    }
}
