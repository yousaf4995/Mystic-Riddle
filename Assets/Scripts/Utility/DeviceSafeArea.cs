using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceSafeArea : MonoBehaviour
{

    [SerializeField] private RectTransform Panel;
    Rect LastSafeArea = new Rect(0, 0, 0, 0);

    void Awake()
    {
        Panel ??= GetComponent<RectTransform>();
        Refresh();
    }

    void Update()
    {
        Refresh();
    }

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        Rect safeArea = GetSafeArea();

        if (safeArea != LastSafeArea)
            ApplySafeArea(safeArea);
    }

    Rect GetSafeArea()
    {
        return Screen.safeArea;
    }

    void ApplySafeArea(Rect r)
    {
        LastSafeArea = r;

        // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
        Vector2 anchorMin = r.position;
        Vector2 anchorMax = r.position + r.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        Panel.anchorMin = anchorMin;
        Panel.anchorMax = anchorMax;

        //Debug.LogFormat("New safe area is assigned to {0}: x={1}, y={2}, w={3}, h={4} on full extents w={5}, h={6}",
        //    name, r.x, r.y, r.width, r.height, Screen.width, Screen.height);
    }
}
