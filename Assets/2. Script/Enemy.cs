using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Enemy : MonoBehaviour
{
    public int hp;
    public int mp;
    public int str;
    public int atk;

    public GameObject enemyCard;
    public List<GameObject> enemyDeck = new List<GameObject>();
}