using UnityEngine;

public class CanvasGroupFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f;

    private Coroutine currentRoutine;

    private void Start()
    {
        canvasGroup.alpha = 0f;
    }

    // Включает затемнение
    public void FadeOut()
    {
        StartFade(0f);
    }

    // Включает осветление
    public void FadeIn()
    {
        StartFade(1f);
    }

    private void StartFade(float targetAlpha)
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
        currentRoutine = StartCoroutine(FadeCanvasGroup(targetAlpha));
    }

    private System.Collections.IEnumerator FadeCanvasGroup(float targetAlpha)
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
        canvasGroup.interactable = targetAlpha > 0.9f;
        canvasGroup.blocksRaycasts = targetAlpha > 0.9f;
    }
}


