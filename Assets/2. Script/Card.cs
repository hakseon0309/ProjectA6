using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public int hp;
    public int mp;

    // 터치할 때 호출, 재호출을 위해선 다른 곳 터치 한번 하고 터치해야함
    private void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter " + gameObject.name);
    }

    // 터치했다가 다른 곳을 터치할 때 한번만 호출
    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit " + gameObject.name);
    }



    // 터치할 때마다 호출
    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown " + gameObject.name);

    }

    // 터치 후, 뗄 때 호출
    private void OnMouseUp()
    {
        Debug.Log("OnMouseUp " + gameObject.name);
    }



    // 터치 후, 다른 곳을 터치할 때까지 계속 호출
    private void OnMouseOver()
    {
        Debug.Log("OnMouseOver " + gameObject.name);
        
    }

    // 터치 홀드, 홀드 떼기 전까지 OnMouseOver랑 같이 계속 호출됨
    private void OnMouseDrag()
    {
        Debug.Log("OnMouseDrag " + gameObject.name);
    }
}
