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
}