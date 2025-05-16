using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GridFader : MonoBehaviour
{
    [Header("Tile Settings")]
    public GameObject tilePrefab;
    public int rows = 10;
    public int columns = 10;
    public float spacing = 0.1f;

    [Header("Animation Settings")]
    public float totalAnimDuration = 2f;      // Общее время анимации на все плитки
    public Vector3 hiddenOffset = new Vector3(0, 0, 5f);  // Плитки начинают с глубины +5 по Z
    public Vector3 targetLocalScale = Vector3.one;        // Конечный масштаб плитки

    private Transform[,] tiles;
    private bool isGridGenerated = false;
    private Coroutine currentRoutine;
    private bool isFading = false;
    private bool? pendingFade = null;

    private void GenerateGrid()
    {
        if (isGridGenerated) return;

        tiles = new Transform[rows, columns];
        Vector3 start = new Vector3(-(columns - 1) / 2f * spacing, 0f, -(rows - 1) / 2f * spacing);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3 pos = start + new Vector3(x * spacing, 0f, y * spacing);
                GameObject tile = Instantiate(tilePrefab, transform);
                tile.transform.localPosition = pos + hiddenOffset;
                tile.transform.localRotation = Quaternion.identity;
                tile.transform.localScale = Vector3.zero;
                tile.SetActive(false);
                tiles[y, x] = tile.transform;
            }
        }
        isGridGenerated = true;
    }

    public void FadeIn()
    {
        TryStartFade(true);
    }

    public void FadeOut()
    {
        TryStartFade(false);
    }

    private void TryStartFade(bool fadeIn)
    {
        if (isFading)
        {
            pendingFade = fadeIn;
            return;
        }

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        GenerateGrid();
        currentRoutine = StartCoroutine(FadeTiles(fadeIn));
    }

    private IEnumerator FadeTiles(bool fadeIn)
    {
        isFading = true;
        pendingFade = null;

        List<Vector2Int> positions = new List<Vector2Int>();
        for (int y = 0; y < rows; y++)
            for (int x = 0; x < columns; x++)
                positions.Add(new Vector2Int(x, y));

        // Случайный порядок появления для живости эффекта
        for (int i = positions.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (positions[i], positions[j]) = (positions[j], positions[i]);
        }

        float delay = totalAnimDuration / positions.Count;

        foreach (var pos in positions)
        {
            var tile = tiles[pos.y, pos.x];

            if (fadeIn && !tile.gameObject.activeSelf)
                tile.gameObject.SetActive(true);

            Vector3 startPos = fadeIn ? tile.localPosition : tile.localPosition;
            Vector3 hiddenPos = tile.localPosition + (fadeIn ? Vector3.zero : hiddenOffset);
            Vector3 shownPos = tile.localPosition + (fadeIn ? hiddenOffset : Vector3.zero);

            StartCoroutine(AnimateTile(tile, fadeIn, delay, hiddenPos, shownPos, targetLocalScale));

            yield return new WaitForSeconds(delay);
        }

        isFading = false;

        if (pendingFade != null)
        {
            bool nextFade = pendingFade.Value;
            pendingFade = null;
            TryStartFade(nextFade);
        }
    }

    private IEnumerator AnimateTile(Transform tile, bool fadeIn, float duration, Vector3 fromPos, Vector3 toPos, Vector3 targetScale)
    {
        float elapsed = 0f;
        Vector3 fromScale = fadeIn ? Vector3.zero : targetScale;
        Vector3 toScale = fadeIn ? targetScale : Vector3.zero;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);

            tile.localPosition = Vector3.Lerp(fromPos, toPos, t);
            tile.localScale = Vector3.Lerp(fromScale, toScale, t);

            yield return null;
        }

        tile.localPosition = toPos;
        tile.localScale = toScale;

        if (!fadeIn)
            tile.gameObject.SetActive(false);
    }
}





