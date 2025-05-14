using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIFadeIn : MonoBehaviour
{
    [SerializeField] private float duration = 1f;

    private CanvasGroup canvasGroup;
    private float timer = 0f;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f; // Начинаем с прозрачности
    }

    void OnEnable()
    {
        timer = 0f;
        StartCoroutine(FadeIn());
    }

    private System.Collections.IEnumerator FadeIn()
    {
        while (timer < duration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(timer / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f; // гарантируем финальную непрозрачность
    }
}
