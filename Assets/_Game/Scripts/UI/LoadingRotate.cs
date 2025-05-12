using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingRotate : MonoBehaviour
{
    private Tween rotateTween;

    void OnEnable()
    {
        rotateTween = transform
            .DOLocalRotate(new Vector3(0, 0, -360), 3f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart); 
    }

    void OnDisable()
    {
        if (rotateTween != null && rotateTween.IsActive())
            rotateTween.Kill();
    }
}
