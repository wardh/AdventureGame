using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GridWindow : EditorWindow
{
    Grid grid;
    public void Init()
    {
        //AssetPreview.GetAssetPreview(tower);
        grid = (Grid)FindObjectOfType(typeof(Grid));
    }
}