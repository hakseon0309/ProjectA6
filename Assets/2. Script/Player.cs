using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Player : MonoBehaviour
{
    public int hp;
    public int mp;
    public int str;
    public int atk;

    public GameObject playerCard;
    public List<GameObject> playerDeck = new List<GameObject>();
}