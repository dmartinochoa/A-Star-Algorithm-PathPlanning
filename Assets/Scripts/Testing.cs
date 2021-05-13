
using System.Collections.Generic;
using UnityEngine;


public class Testing : MonoBehaviour
{

    [SerializeField] private PathfindingVisual pathfindingVisual;
    private Pathfinding pathfinding;
    public GameObject player;

    public float speed = 1f;

    private void Start()
    {
        player = GameObject.Find("Player"); ;
        pathfinding = new Pathfinding(10, 10);
        pathfindingVisual.SetGrid(pathfinding.GetGrid());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("First pos: " + player.transform.position);
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

            int[] playerXY = GetPlayerPosicion(player.transform);
            List<PathNode> path = pathfinding.FindPath(playerXY[0], playerXY[1], x, y);

            if (path != null)
            {
                for (int i = 0; i < path.Count-1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 50f);
                    Vector3 newPos = new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f;
                    Debug.Log("Current pos: " + player.transform.position + " --- Moving to pos:" + newPos);
                    Vector3 movePos = Vector3.MoveTowards(player.transform.position, newPos, speed * Time.deltaTime);
                    player.transform.position = newPos;
                }
                Debug.Log("Last pos: " + player.transform.position);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
            
        }
    }

    public static int[] GetPlayerPosicion(Transform transform)
    {
       Vector3 idkwhatimdoing = transform.position;
       int x = Mathf.FloorToInt((idkwhatimdoing - Vector3.one * 5f).x / 10f); 
       int y = Mathf.FloorToInt((idkwhatimdoing - Vector3.one * 5f).y / 10f);
       int[] xy = {x, y};
       return xy;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
