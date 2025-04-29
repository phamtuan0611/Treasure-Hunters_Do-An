using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreParentScale : MonoBehaviour
{
    private Vector3 initialLocalScale;
    private Vector3 initialOffsetFromParent;
    private Vector3 initialParentScale;

    private Transform parent;
    private CanvasGroup canvasGroup;

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float displayTime = 3f;

    private Coroutine fadeCoroutine;

    void Start()
    {
        parent = transform.parent;

        if (parent != null)
        {
            initialLocalScale = transform.localScale;
            initialOffsetFromParent = transform.position - parent.position;
            initialParentScale = parent.lossyScale;
        }

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        ShowAndHide(); // tự động hiện lên khi bắt đầu
    }

    void LateUpdate()
    {
        if (parent == null) return;

        // Vị trí: luôn giữ khoảng cách ban đầu với parent
        transform.position = parent.position + initialOffsetFromParent;

        // Scale: giữ nguyên scale ban đầu (bỏ ảnh hưởng của việc lật - scale X âm)
        Vector3 currentParentScale = parent.lossyScale;
        transform.localScale = new Vector3(
            initialLocalScale.x * (initialParentScale.x / currentParentScale.x),
            initialLocalScale.y * (initialParentScale.y / currentParentScale.y),
            initialLocalScale.z * (initialParentScale.z / currentParentScale.z)
        );
    }

    public void ShowAndHide()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut()
    {
        yield return FadeTo(1f);
        yield return new WaitForSeconds(displayTime);
        yield return FadeTo(0f);
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        canvasGroup.interactable = targetAlpha > 0f;
        canvasGroup.blocksRaycasts = targetAlpha > 0f;
    }
}
