using UnityEngine;
using UnityEngine.UI;

public class CustomGridLayoutGroup : GridLayoutGroup
{
    private Vector2 originalCellSize;

    protected override void OnEnable()
    {
        base.OnEnable();
        originalCellSize = cellSize; // Store the original size
    }

    public override void SetLayoutHorizontal()
    {
        SetCellsAlongAxis(0);
    }

    public override void SetLayoutVertical()
    {
        SetCellsAlongAxis(1);
    }

    private void SetCellsAlongAxis(int axis)
    {
        var rectChildrenCount = rectChildren.Count;
        if (axis == 0)
        {
            for (int i = 0; i < rectChildrenCount; i++)
            {
                RectTransform rect = rectChildren[i];
                m_Tracker.Add(this, rect,
                    DrivenTransformProperties.Anchors |
                    DrivenTransformProperties.AnchoredPosition |
                    DrivenTransformProperties.SizeDelta);

                rect.anchorMin = Vector2.up;
                rect.anchorMax = Vector2.up;
            }
            return;
        }

        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        int total = rectChildrenCount;

        int cellCountX = 1;
        int cellCountY = 1;

        if (m_Constraint == Constraint.FixedColumnCount)
        {
            cellCountX = m_ConstraintCount;
            cellCountY = Mathf.CeilToInt((float)total / cellCountX);
        }
        else if (m_Constraint == Constraint.FixedRowCount)
        {
            cellCountY = m_ConstraintCount;
            cellCountX = Mathf.CeilToInt((float)total / cellCountY);
        }
        else
        {
            cellCountX = Mathf.Max(1, Mathf.FloorToInt((width - padding.horizontal + spacing.x + 0.001f) / (originalCellSize.x + spacing.x)));
            cellCountY = Mathf.CeilToInt((float)total / cellCountX);
        }

        // Calculate required total width and height with original cell size
        float requiredWidth = cellCountX * originalCellSize.x + (cellCountX - 1) * spacing.x + padding.horizontal;
        float requiredHeight = cellCountY * originalCellSize.y + (cellCountY - 1) * spacing.y + padding.vertical;

        // Calculate scaling factor to fit within the available space
        float scaleX = width / requiredWidth;
        float scaleY = height / requiredHeight;
        float scale = Mathf.Min(scaleX, scaleY, 1f); // Never scale up

        // Apply scaled cell size
        cellSize = originalCellSize * scale;

        int cornerX = (int)startCorner % 2;
        int cornerY = (int)startCorner / 2;

        float rowHeight = cellSize.y;
        float totalHeight = cellCountY * rowHeight + (cellCountY - 1) * spacing.y;
        float startY = GetStartOffset(1, totalHeight);

        for (int y = 0; y < cellCountY; y++)
        {
            int rowStart = y * cellCountX;
            int rowEnd = Mathf.Min(total, rowStart + cellCountX);
            int itemsInRow = rowEnd - rowStart;

            if (itemsInRow <= 0) continue;

            float contentWidth = itemsInRow * cellSize.x + (itemsInRow - 1) * spacing.x;
            float availableWidth = width - padding.horizontal;

            float extraSpacingX = (availableWidth - contentWidth) / (itemsInRow + 1);

            float rowY = startY + y * (cellSize.y + spacing.y);

            for (int i = 0; i < itemsInRow; i++)
            {
                int itemIndex = rowStart + i;
                int posX = cornerX == 1 ? (itemsInRow - 1 - i) : i;
                int posY = cornerY == 1 ? (cellCountY - 1 - y) : y;

                float x = padding.left + extraSpacingX * (posX + 1) + spacing.x * posX + cellSize.x * posX;
                float yPos = rowY;

                SetChildAlongAxis(rectChildren[itemIndex], 0, x, cellSize.x);
                SetChildAlongAxis(rectChildren[itemIndex], 1, yPos, cellSize.y);
            }
        }
    }
}
