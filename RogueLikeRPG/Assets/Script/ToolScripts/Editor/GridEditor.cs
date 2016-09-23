using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    Grid myGrid;
    private GridWindow window;
    private List<GameObject> myTiles;
    private int tileToPaint;
    public void OnEnable()
    {
        Object[] prefabs;
        myTiles = new List<GameObject>();
        prefabs = Resources.LoadAll("Tiles");

        foreach (var prefab in prefabs)
        {
            myTiles.Add((GameObject)prefab);
        }
        myGrid = (Grid)target;
        SceneView.onSceneGUIDelegate += GridUpdate;
        tileToPaint =0;
    }

    public void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= GridUpdate;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        myGrid.myParent = (GameObject)EditorGUILayout.ObjectField("Tile parent", myGrid.myParent, typeof(Object), true);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(" Grid Width ");
        myGrid.width = EditorGUILayout.FloatField(myGrid.width, GUILayout.Width(50));
        GUILayout.Label(" Grid Height ");
        myGrid.height = EditorGUILayout.FloatField(myGrid.height, GUILayout.Width(50));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        GUILayout.Label(" Tile to paint ");
        tileToPaint = EditorGUILayout.IntField(tileToPaint, GUILayout.Width(50));
        GUILayout.EndHorizontal();
        foreach (var gameObject in myTiles)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.TextField(gameObject.name, gameObject.name, new GUIStyle());
            GUILayout.EndHorizontal();
            //EditorGUI.DrawPreviewTexture(rec, AssetPreview.GetAssetPreview(gameObject));
        }


        if (GUILayout.Button("Open Grid Window", GUILayout.Width(255)))
        {
            GridWindow window = (GridWindow)EditorWindow.GetWindow(typeof(GridWindow));
            window.Init();
        }
        SceneView.RepaintAll();
    }
    void GridUpdate(SceneView sceneview)
    {
        Event e = Event.current;

        Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        if (e.isKey && e.character == 'd')
        {
            tileToPaint++;
            if (tileToPaint >= myTiles.Count)
            {
                tileToPaint = 0;
            }
        }
        if (e.isKey && e.character == 'a')
        {
            GameObject obj;

            obj = GameObject.Instantiate(myTiles[tileToPaint]);
                RaycastHit rayHit = new RaycastHit();

                Physics.Raycast(r, out rayHit, 1000);

                //Vector3 alignedPosition = new Vector3(
                //    ((rayHit.point.x - (rayHit.point.x % myGrid.width)) / myGrid.width) * myGrid.width + myGrid.width * 0.5f,
                //    rayHit.point.y,
                //    ((rayHit.point.z - (rayHit.point.z % myGrid.height)) / myGrid.height) * myGrid.height - myGrid.height * 0.5f);

                Vector3 newPosition = new Vector3(rayHit.point.x - (rayHit.point.x % myGrid.width) + myGrid.width * 0.5f, rayHit.point.y, rayHit.point.z - (rayHit.point.z % myGrid.height) + myGrid.height * 0.5f);


                obj.transform.position = newPosition;
            obj.transform.parent = myGrid.myParent.transform;
        }
    }
}