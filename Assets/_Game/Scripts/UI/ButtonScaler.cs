using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float scale = 0.85f;
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(scale, 0.25f).SetUpdate(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.05f, 0.15f).SetUpdate(true))
            .Append(transform.DOScale(0.95f, 0.1f).SetUpdate(true))
            .Append(transform.DOScale(1f, 0.1f).SetUpdate(true));
    }
}
