using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Pathfinding pathfinding;
    private void Start()
    {
        pathfinding = new Pathfinding(10, 10);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                { 
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}

    /*    Grid grid;
        int value;
        private void Start()
        {
            grid = new Grid(4, 4, 10f, new Vector3(20, 0));
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                grid.SetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition), 10);
            }

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log(grid.GetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            }
        }*/
