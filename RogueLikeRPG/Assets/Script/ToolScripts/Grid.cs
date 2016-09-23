using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{

    public float width = 32f;
    public float height = 32f;
    public CustomList myListOfTiles;
    public GameObject myParent;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Vector3 pos = Camera.current.transform.position;
        //for (float z = pos.y - 800.0f; z < pos.y + 800.0f; z += height)
        //{
        //    Gizmos.DrawLine(new Vector3(-1000000.0f, 0f, Mathf.Floor(z / height) * height),
        //                    new Vector3(1000000.0f, 0.0f, Mathf.Floor(z / height) * height));
        //}

        //for (float x = pos.x - 1200.0f; x < pos.x + 1200.0f; x += width)
        //{
        //    Gizmos.DrawLine(new Vector3(Mathf.Floor(x / width) * width, -1000000.0f, 0.0f),
        //                    new Vector3(Mathf.Floor(x / width) * width, 1000000.0f, 0.0f));
        //}
        for (int x = -1000; x < 1000; x++)
        {
            Gizmos.DrawLine(new Vector3(x * width, 0, -1000 * height), new Vector3(x * width, 0, 1000 * height));
        }
        for (int z = -1000; z < 1000; z++)
        {
            Gizmos.DrawLine(new Vector3(-1000 * width, 0, z * height), new Vector3(1000 * width, 0, z * height));
        }
    }
}
