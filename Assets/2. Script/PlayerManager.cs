using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    // 싱글톤
    private static PlayerManager instance = null;
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
    public static PlayerManager Instance
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





    public int hp;
    public int mp;
    public int power;
    public int drawCount;

    public GameObject playerCharacterCard;

    public List<GameObject> playerDeck;
}