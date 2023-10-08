using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(GridLayoutGroup))]
public class DynamicGrid : MonoBehaviour
{
    [SerializeField] private int rows, columns;

    private void OnRectTransformDimensionsChange()
    {
        var gridLayoutGroup = GetComponent<GridLayoutGroup>();
        var size = GetComponent<RectTransform>().rect.size;
        size.x -= gridLayoutGroup.padding.horizontal;
        size.y -= gridLayoutGroup.padding.vertical;
        gridLayoutGroup.cellSize = new Vector2(size.x / columns, size.y / rows);
    }
}
