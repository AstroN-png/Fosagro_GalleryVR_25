using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swipe : MonoBehaviour
{
    [SerializeField] VideoController videoController;
    public Color[] colors;
    public GameObject scrollbar;
    private float scroll_pos = 0;
    float[] pos;
    private float time;

    public float baseScale = 306f;         // Базовый масштаб элементов
    public float scaleStep = 0.2f;         // Шаг изменения масштаба
    public float scrollSpeed = 5f;         // Скорость анимации прокрутки
    public float zOffsetStep = 10f;        // Смещение по оси Z
    public float zLerpSpeed = 0.1f;        // Скорость интерполяции по Z
    public float alphaStep = 0.05f;        // Шаг изменения прозрачности

    private float targetScrollPos = 0;

    void OnEnable()
    {
        targetScrollPos = 0f; // Устанавливаем цель прокрутки в начало
        if (videoController != null)
        {
            videoController.PlayVideo(0);
        }
    }
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            targetScrollPos = scroll_pos;
        }
        else
        {
            scroll_pos = Mathf.Lerp(scroll_pos, targetScrollPos, Time.deltaTime * scrollSpeed);
            scrollbar.GetComponent<Scrollbar>().value = scroll_pos;
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2f) && scroll_pos > pos[i] - (distance / 2f))
            {
                Debug.LogWarning("Current Selected Level " + i);

                // Центральный элемент
                Transform main = transform.GetChild(i);
                main.localScale = Vector2.Lerp(main.localScale, new Vector2(baseScale, baseScale), 0.1f);
                main.GetComponent<Canvas>().overrideSorting = true;
                main.GetComponent<Canvas>().sortingOrder = 10;
                main.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(main.GetComponent<CanvasGroup>().alpha, 1f, 0.15f);

                // Плавное возвращение по Z = 0
                Vector3 currentMainPos = main.localPosition;
                Vector3 desiredMainPos = new Vector3(currentMainPos.x, currentMainPos.y, 0f);
                main.localPosition = Vector3.Lerp(currentMainPos, desiredMainPos, zLerpSpeed);

                for (int j = 0; j < pos.Length; j++)
                {
                    if (j == i) continue;

                    Transform child = transform.GetChild(j);
                    int offset = Mathf.Abs(j - i);

                    float targetScale = baseScale - (scaleStep * offset);
                    float targetAlpha = Mathf.Max(1f - (alphaStep * offset), 0f);
                    int sortingOrder = offset <= 1 ? 9 : 8;

                    // Масштаб, прозрачность, порядок отрисовки
                    child.localScale = Vector2.Lerp(child.localScale, new Vector2(targetScale, targetScale), 0.1f);
                    child.GetComponent<Canvas>().overrideSorting = true;
                    child.GetComponent<Canvas>().sortingOrder = sortingOrder;
                    float currentAlpha = child.GetComponent<CanvasGroup>().alpha;
                    child.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(currentAlpha, targetAlpha, 0.1f);

                    // Плавное смещение по Z
                    int relativeIndex = j - i;
                    float zOffset = relativeIndex * zOffsetStep;
                    Vector3 currentPos = child.localPosition;
                    Vector3 desiredPos = new Vector3(currentPos.x, currentPos.y, zOffset);
                    child.localPosition = Vector3.Lerp(currentPos, desiredPos, zLerpSpeed);
                }
            }
        }
    }

    public void ScrollRight()
    {
        scrollSpeed = 5f;
        float distance = 1f / (transform.childCount - 1f);

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2f) && scroll_pos > pos[i] - (distance / 2f))
            {
                int nextIndex = Mathf.Min(i + 1, pos.Length - 1);
                targetScrollPos = pos[nextIndex];
                time = 0;
                if (videoController != null)
                {
                    videoController.PlayVideo(nextIndex);
                }
                break;
            }
        }
        

    }

    public void ScrollLeft()
    {
        scrollSpeed = 5f;
        float distance = 1f / (transform.childCount - 1f);

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2f) && scroll_pos > pos[i] - (distance / 2f))
            {
                int prevIndex = Mathf.Max(i - 1, 0);
                targetScrollPos = pos[prevIndex];
                time = 0;
                if (videoController != null)
                {
                    videoController.PlayVideo(prevIndex);
                }
                break;
            }
        }
    }
}



