using UnityEngine;
using UnityEngine.UI;

public class CardGridLayout : LayoutGroup
{
	public int gridRows;
	public int gridColumns;
	public Vector2 cellSize;
	public Vector2 cellSpacing;
	public int paddingFactor;

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

		float parentWidth = rectTransform.rect.width;
		float parentHeight = rectTransform.rect.height;

		float cellHeight = (parentHeight - 2 * paddingFactor - (gridRows - 1) * cellSpacing.y) / gridRows;
		float cellWidth = cellHeight;

		cellSize.x = cellWidth;
		cellSize.y = cellHeight;

		padding.left = Mathf.FloorToInt((parentWidth - gridColumns * cellHeight) / 2);
		padding.top = Mathf.FloorToInt((parentHeight - gridRows * cellWidth) / 2);
		padding.bottom = padding.top;

		int columnCount = 0;
		int rowCount = 0;

		for (int i = 0; i < rectChildren.Count; i++)
		{
			rowCount = i / gridColumns;
			columnCount = i % gridColumns;

			var item = rectChildren[i];

			var xPos = padding.left + (cellSize.x * columnCount) + (cellSpacing.x * (columnCount - 1));
			var yPos = padding.top + (cellSize.y * rowCount) + (cellSpacing.y * (rowCount - 1));

			SetChildAlongAxis(item, 0, xPos, cellSize.x);
			SetChildAlongAxis(item, 1, yPos, cellSize.y);
		}
		
	}

    public override void CalculateLayoutInputVertical()
    {
       
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
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

    public override string ToString()
    {
        return base.ToString();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnBeforeTransformParentChanged()
    {
        base.OnBeforeTransformParentChanged();
    }

    protected override void OnCanvasGroupChanged()
    {
        base.OnCanvasGroupChanged();
    }

    protected override void OnCanvasHierarchyChanged()
    {
        base.OnCanvasHierarchyChanged();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void OnDidApplyAnimationProperties()
    {
        base.OnDidApplyAnimationProperties();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
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

    protected override void OnValidate()
    {
        base.OnValidate();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    protected override void Start()
    {
        base.Start();
    }
}
