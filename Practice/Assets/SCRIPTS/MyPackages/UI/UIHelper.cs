using UnityEngine;
using UnityEngine.UI;

public static class UIHelper
{
    public static void SetAnchorPreset(RectTransform rt, RectAnchorPreset preset)
    {
        switch (preset)
        {
            case RectAnchorPreset.TopLeft:
                rt.anchorMin = new Vector2(0f, 1f);
                rt.anchorMax = new Vector2(0f, 1f);
                rt.pivot = new Vector2(0f, 1f);
                break;

            case RectAnchorPreset.TopCenter:
                rt.anchorMin = new Vector2(0.5f, 1f);
                rt.anchorMax = new Vector2(0.5f, 1f);
                rt.pivot = new Vector2(0.5f, 1f);
                break;

            case RectAnchorPreset.TopRight:
                rt.anchorMin = new Vector2(1f, 1f);
                rt.anchorMax = new Vector2(1f, 1f);
                rt.pivot = new Vector2(1f, 1f);
                break;

            case RectAnchorPreset.MiddleCenter:
                rt.anchorMin = new Vector2(0.5f, 0.5f);
                rt.anchorMax = new Vector2(0.5f, 0.5f);
                rt.pivot = new Vector2(0.5f, 0.5f);
                break;

            case RectAnchorPreset.BottomStretch:
                rt.anchorMin = new Vector2(0f, 0f);
                rt.anchorMax = new Vector2(1f, 0f);
                rt.pivot = new Vector2(0.5f, 0f);
                break;

            case RectAnchorPreset.StretchAll:
                rt.anchorMin = new Vector2(0f, 0f);
                rt.anchorMax = new Vector2(1f, 1f);
                rt.pivot = new Vector2(0.5f, 0.5f);
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;
                break;
        }
    }
    public static void ScrollToChildIndex(
       ScrollRect scrollRect,
       int targetIndex,
       GridLayoutGroup gridLayout)
    {
        RectTransform content = scrollRect.content;
        int columnCount = Mathf.FloorToInt((content.rect.width + gridLayout.spacing.x) /
                                           (gridLayout.cellSize.x + gridLayout.spacing.x));
        if (columnCount <= 0) columnCount = 1;

        int row = targetIndex / columnCount;
        float totalHeight = content.rect.height;
        float cellHeight = gridLayout.cellSize.y + gridLayout.spacing.y;
        float targetY = row * cellHeight;

        // Tính scrollPosition theo tỷ lệ (pivot mặc định là top-left)
        float normalizedY = 1f - Mathf.Clamp01(targetY / (totalHeight - scrollRect.viewport.rect.height));

        scrollRect.verticalNormalizedPosition = normalizedY;
    }
    public static int GetCountPerRow(RectTransform content, GridLayoutGroup grid, float scale)
    {
        float scaledCellWidth = grid.cellSize.x * scale;
        float spacingX = grid.spacing.x;
        float totalWidth = content.rect.width;

        int countPerRow = Mathf.FloorToInt((totalWidth + spacingX) / (scaledCellWidth + spacingX));
        return Mathf.Max(1, countPerRow);
    }
    public static int GetCountPerRow(float containerWidth, float cellWidth, float spacing, float scale)
    {
        float scaledCellWidth = cellWidth * scale;
        float totalUnitWidth = scaledCellWidth + spacing;

        int count = Mathf.FloorToInt((containerWidth + spacing) / totalUnitWidth);
        return Mathf.Max(1, count);
    }
    public static string FormatTime(float timeInSeconds)
    {
            int totalCentiseconds = Mathf.FloorToInt(timeInSeconds * 100);

            int hours = totalCentiseconds / (3600 * 100);
            int minutes = (totalCentiseconds / (60 * 100)) % 60;
            int seconds = (totalCentiseconds / 100) % 60;
            int centiseconds = totalCentiseconds % 100;

            return string.Format("{0:00}:{1:00}:{2:00}",minutes, seconds, centiseconds);
    }
}
public enum RectAnchorPreset
{
    TopLeft,
    TopCenter,
    TopRight,
    MiddleCenter,
    BottomStretch,
    StretchAll,
    Custom
}
