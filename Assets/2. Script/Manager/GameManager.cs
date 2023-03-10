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

public class GameManager : MonoBehaviour
{
    // 싱글톤
    private static GameManager instance = null;
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
    public static GameManager Instance
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





    private void Start()
    {

    }

    private void Update()
    {

    }

    public void StartGame()
    {

    }





    [Header("Developer Mode")]
    [SerializeField] [Tooltip("첫번째 턴을 시작할 캐릭터")] ETurnMode eTurnMode;
    [SerializeField] [Tooltip("시작 드로우 카드 수")] public int startCardCount;
    [SerializeField] [Tooltip("패스트 모드 On/Off")] public bool fastMode;

    [Header("Property")]
    public bool isLoading;  // True - 터치 방지
    public bool isPlayerTurn;
    private enum ETurnMode {Player, Enemy, Random}
    [SerializeField] public WaitForSeconds delayStartGame = new WaitForSeconds(0.5f);
    [SerializeField] public WaitForSeconds delayStartTurn = new WaitForSeconds(0.7f);

    [Header("DoTween")]
    public float doMoveSec;
    public float doScaleSec;

    // Cheat Mode
    [ContextMenu("DrawTopCard")]
    private void DrawTopCard() => CardManager.Instance.DrawTopCard();
    [ContextMenu("DrawBottomCard")]
    private void DrawBottomCard() => CardManager.Instance.DrawBottomCard();
    [ContextMenu("DrawRandomCard")]
    private void DrawRandomCard() => CardManager.Instance.DrawRandomCard();

    [ContextMenu("DiscardTopCard")]
    private void DiscardTopCard() => CardManager.Instance.DiscardTopCard();
    [ContextMenu("DiscardBottomCard")]
    private void DiscardBottomCard() => CardManager.Instance.DiscardBottomCard();
    [ContextMenu("DiscardRandomCard")]
    private void DiscardRandomCard() => CardManager.Instance.DiscardRandomCard();

    [ContextMenu("RemoveTopCard")]
    private void RemoveTopCard() => CardManager.Instance.RemoveTopCard();
    [ContextMenu("RemoveBottomCard")]
    private void RemoveBottomCard() => CardManager.Instance.RemoveBottomCard();
    [ContextMenu("RemoveRandomCard")]
    private void RemoveRandomCard() => CardManager.Instance.RemoveRandomCard();

    // [ContextMenu("MoveCard")]
    // private void MoveCard() => BattleManager.Instance.MoveCard();
    [ContextMenu("DeckCycle")]
    private void DeckCycle() => CardManager.Instance.DeckCycle();
    // [ContextMenu("Shuffle")]
    // private void Shuffle() => BattleManager.Instance.Shuffle();

    // [ContextMenu("EndTurn")]
    // private void EndTurn() => BattleManager.Instance.EndTurn();
    [ContextMenu("SetSortingLayer")]
    private void SetSortingLayer() => CardManager.Instance.SetSortingLayer();

    // [ContextMenu("AlignmentCards")]
    // private void AlignmentCards() => BattleManager.Instance.AlignmentCards();
    [ContextMenu("CardAlignment")]
    private void CardAlignment() => CardManager.Instance.CardAlignment();



    [SerializeField] NotificationPanel notificationPanel;
    public void Notification(string message) => notificationPanel.Show(message);
}