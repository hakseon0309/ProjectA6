using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    // 싱글톤 패턴~
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
    // ~싱글톤 패턴

    // 아래 함수들은 다른 클래스에서 자유롭게 호출 가능
    // Add() 라는 함수를 호출하는 예시 - GameManager.Instance.Add();
}