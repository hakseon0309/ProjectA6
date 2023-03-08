using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        StartGame();
    }

    private void Update()
    {

    }

    public void StartGame()
    {
        StartCoroutine(TurnManager.Instance.Co_StartGame());
    }



    // Cheat Mode
    [ContextMenu("DrawTopCard")]
    private void DrawTopCard() => TurnManager.Instance.DrawTopCard();
    [ContextMenu("DrawBottomCard")]
    private void DrawBottomCard() => TurnManager.Instance.DrawBottomCard();
    [ContextMenu("DrawRandomCard")]
    private void DrawRandomCard() => TurnManager.Instance.DrawRandomCard();

    [ContextMenu("DiscardTopCard")]
    private void DiscardTopCard() => TurnManager.Instance.DiscardTopCard();
    [ContextMenu("DiscardBottomCard")]
    private void DiscardBottomCard() => TurnManager.Instance.DiscardBottomCard();
    [ContextMenu("DiscardRandomCard")]
    private void DiscardRandomCard() => TurnManager.Instance.DiscardRandomCard();

    [ContextMenu("RemoveTopCard")]
    private void RemoveTopCard() => TurnManager.Instance.RemoveTopCard();
    [ContextMenu("RemoveBottomCard")]
    private void RemoveBottomCard() => TurnManager.Instance.RemoveBottomCard();
    [ContextMenu("RemoveRandomCard")]
    private void RemoveRandomCard() => TurnManager.Instance.RemoveRandomCard();

    // [ContextMenu("MoveCard")]
    // private void MoveCard() => TurnManager.Instance.MoveCard();
    [ContextMenu("DeckCycle")]
    private void DeckCycle() => TurnManager.Instance.DeckCycle();
    // [ContextMenu("Shuffle")]
    // private void Shuffle() => TurnManager.Instance.Shuffle();

    [ContextMenu("EndTurn")]
    private void EndTurn() => TurnManager.Instance.EndTurn();

    [SerializeField] NotificationPanel notificationPanel;
    public void Notification(string message) => notificationPanel.Show(message);
}