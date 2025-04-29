using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IgnoreParentScale : MonoBehaviour
{
    private Vector3 initialLocalScale;
    private Vector3 initialOffsetFromParent;
    private Vector3 initialParentScale;

    private Transform parent;
    private CanvasGroup canvasGroup;

    [Header("UI Components")]
    [SerializeField] private TMP_Text chatText;

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 0.5f;

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
    }

    void LateUpdate()
    {
        if (parent == null) return;

        transform.position = parent.position + initialOffsetFromParent;

        Vector3 currentParentScale = parent.lossyScale;
        transform.localScale = new Vector3(
            initialLocalScale.x * (initialParentScale.x / currentParentScale.x),
            initialLocalScale.y * (initialParentScale.y / currentParentScale.y),
            initialLocalScale.z * (initialParentScale.z / currentParentScale.z)
        );
    }

    public void ShowMessage(string message)
    {
        chatText.text = message;
        Show();
    }

    public void Show()
    {
        if (!gameObject.activeInHierarchy) return;
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeTo(1f));
    }

    public void Hide()
    {
        if (!gameObject.activeInHierarchy) return;
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeTo(0f));
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
