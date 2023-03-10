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

public class BattleManager : MonoBehaviour
{
    // 싱글톤
    // private static BattleManager instance = null;
    // private void Awake()
    // {
    //     if (instance == null)
    //     {
    //         instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }
    // public static BattleManager Instance
    // {
    //     get
    //     {
    //         if (instance == null)
    //         {
    //             return null;
    //         }
    //         return instance;
    //     }
    // }





    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {

    }





    public IEnumerator BattleStart()
    {
        GameManager.Instance.isLoading = true;
        OptionSetUp();
        CardManager.Instance.DeckSetUp();

        for (int i = 0; i < GameManager.Instance.startCardCount; i++)
        {
            yield return GameManager.Instance.delayStartGame;
            CardManager.Instance.DrawRandomCard();
        }
        
        StartCoroutine(Co_StartTurn());
    }

    private void OptionSetUp()
    {
        if (GameManager.Instance.fastMode == true)
        {
            GameManager.Instance.delayStartGame = new WaitForSeconds(0.05f);
            GameManager.Instance.delayStartTurn = new WaitForSeconds(0.07f);
        }

        // switch (eTurnMode)
        // {
        //     case ETurnMode.Player:
        //         isPlayerTurn = true;
        //         break;
        //     case ETurnMode.Enemy:
        //         isPlayerTurn = false;
        //         break;
        //     case ETurnMode.Random:
        //         isPlayerTurn = Random.Range(0, 2) == 0;
        //         break;
        // }
    }



    private IEnumerator Co_StartTurn()
    {
        GameManager.Instance.isLoading = true;
        if (GameManager.Instance.isPlayerTurn)
            GameManager.Instance.Notification("Player Turn");

        yield return GameManager.Instance.delayStartTurn;
        CardManager.Instance.DrawRandomCard();
        yield return GameManager.Instance.delayStartTurn;
        GameManager.Instance.isLoading = false;
    }

    public void EndTurn()
    {
        GameManager.Instance.isPlayerTurn = !GameManager.Instance.isPlayerTurn;
    }
}
