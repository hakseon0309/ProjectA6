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



    private void Start()
    {
        BattleSetUp();
        StartCoroutine(PlayerTurn());
    }

    private void Update()
    {

    }



    public NotificationPanel notifi;
    public void BattleSetUp()
    {
        GameManager.Instance.isLoading = true;

        // 플레이어와 에너미의 캐릭터 카드를 배치

        // CardManager.Instance.DeckSetUp();

        CardManager.Instance.Shuffle();
    }

    public IEnumerator PlayerTurn()
    {
        GameManager.Instance.isLoading = true;
        GameManager.Instance.isPlayerTurn = true;
        yield return new WaitForSeconds(0.5f);
        notifi.Show("Player Turn");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(CardManager.Instance.DrawPerTurn(GameManager.Instance.drawCountPerTurn));
        GameManager.Instance.isLoading = false;
    }


}
