using UnityEngine;
using UnityEngine.UI;

public class CardGridLayout : LayoutGroup
{
    public int gridRows;
    public int gridColumns;
    public Vector2 cellSize;
    public Vector2 cellSpacing;
    [Range(5,100)]
    public float paddingFactor;


    [SerializeField] float parentWidth;
    [SerializeField] float parentHeight;
    public override float minWidth => base.minWidth;

    public override float preferredWidth => base.preferredWidth;

    public override float flexibleWidth => base.flexibleWidth;

    public override float minHeight => base.minHeight;

    public override float preferredHeight => base.preferredHeight;

    public override float flexibleHeight => base.flexibleHeight;

    public override int layoutPriority => base.layoutPriority;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (gridRows == 0 || gridColumns == 0)
        {
            gridRows = 4;
            gridColumns = 5;
        }

        parentWidth = rectTransform.rect.width;
        parentHeight = rectTransform.rect.height;

      //  paddingFactor = Mathf.Clamp(paddingFactor, 0, Mathf.Min(parentWidth, parentHeight) / 2);

        float cellHeight = (parentHeight - (2 * paddingFactor) - cellSpacing.y * (gridRows - 1)) / gridRows;
        float cellWidth = cellHeight;

        if (cellWidth * gridColumns + cellSpacing.x * (gridColumns - 1) > parentWidth)
        {
            cellWidth = (parentWidth - (2 * paddingFactor) - cellSpacing.x * (gridColumns - 1)) / gridColumns;
            cellHeight = cellWidth;
        }

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        padding.left = Mathf.FloorToInt((parentWidth - gridColumns * cellWidth - cellSpacing.x * (gridColumns - 1)) / 2);
        padding.top = Mathf.FloorToInt((parentHeight - gridRows * cellHeight - cellSpacing.y * (gridRows - 1)) / 2);

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / gridColumns;
            columnCount = i % gridColumns;

            var item = rectChildren[i];

            var xPos = padding.left + (cellSize.x + cellSpacing.x) * columnCount;
            var yPos = padding.top + (cellSize.y + cellSpacing.y) * rowCount;

            //var xPos2 = padding.left + (cellSize.x * columnCount) + (cellSpacing.x * (columnCount - 1));
            //var yPos2 = padding.top + (cellSize.y * rowCount) + (cellSpacing.y * (rowCount - 1));

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }



    public override void CalculateLayoutInputVertical()
    {
        OnTransformChildrenChanged();
    }


    public override bool IsActive()
    {
        return base.IsActive();
    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {

    }


    protected override void OnBeforeTransformParentChanged()
    {
        base.OnBeforeTransformParentChanged();
    }

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
    }

    protected override void OnTransformChildrenChanged()
    {
        base.OnTransformChildrenChanged();
    }

    protected override void OnTransformParentChanged()
    {
        base.OnTransformParentChanged();
    }

}
