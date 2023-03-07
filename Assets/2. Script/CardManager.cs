using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CardManager : MonoBehaviour
{
    public Card card;

    public Vector3 originScale;
    public Transform tr;

    public TMP_Text tmp_Hp;
    public TMP_Text tmp_Mp;

    private void Update()
    {
        tmp_Hp.text = card.hp.ToString();
        tmp_Mp.text = card.mp.ToString();
    }

    private void Start()
    {
        originScale = tr.localScale;
    }

    public void TempExpand()
    {
        Vector3 overScale = originScale * 1.2f;
        tr.DOScale(overScale, 0.3f);
    }

    public void SetOrigin()
    {
        tr.DOScale(originScale, 0.3f);
    }
}
