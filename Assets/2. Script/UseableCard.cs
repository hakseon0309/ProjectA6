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

public class UseableCard : MonoBehaviour
{
    public PRS originPRS;



    private void Awake()
    {

    }
    private void Start()
    {

    } 
    private void Update()
    {

    }



    ///// 터치 시스템

    // 터치할 때 호출, 재호출을 위해선 다른 곳 터치 한번 하고 터치해야함
    // 터치시 확대
    private void OnMouseEnter()
    {
        // Debug.Log("OnMouseEnter " + gameObject.name);
        TempExpand();
    }

    // 터치했다가 다른 곳을 터치할 때 한번만 호출
    private void OnMouseExit()
    {
        // Debug.Log("OnMouseExit " + gameObject.name);
        SetOrigin();
    }



    // 터치할 때마다 호출
    private void OnMouseDown()
    {
        // Debug.Log("OnMouseDown " + gameObject.name);

    }

    // 터치 후, 뗄 때 호출
    private void OnMouseUp()
    {
        // Debug.Log("OnMouseUp " + gameObject.name);
    }



    // 터치 후, 다른 곳을 터치할 때까지 계속 호출
    private void OnMouseOver()
    {
        // Debug.Log("OnMouseOver " + gameObject.name);
        
    }

    // 터치 홀드, 홀드 떼기 전까지 OnMouseOver랑 같이 계속 호출됨
    private void OnMouseDrag()
    {
        // Debug.Log("OnMouseDrag " + gameObject.name);

    }





    ///// 모듈 함수
    
    public void TempExpand()
    {
        // Vector3 overScale = originScale * 1.2f;
        // transform.DOScale(overScale, 0.3f);
    }

    public void SetOrigin()
    {
        // transform.DOScale(originScale, 0.3f);
    }

    public void MoveTransform(PRS prs, bool useDotween, float dotweenSec = 0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotweenSec);
            transform.DORotateQuaternion(prs.rot, dotweenSec);
            transform.DOScale(prs.scale, dotweenSec);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
}
