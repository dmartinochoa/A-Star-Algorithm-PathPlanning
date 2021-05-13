using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField] private PathfindingVisual pathfindingVisual;
    private Pathfinding pathfinding;
    public GameObject player;

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
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y); //gets the (x,y) values of the node you clicked on
            int[] playerXY = GetPlayerPosicion(player.transform); //gets the (x,y) values of the player GameObject
            List<PathNode> path = pathfinding.FindPath(playerXY[0], playerXY[1], x, y);
            if (path != null)
            {
                StartCoroutine(MovePlayer(path));
            }
            else
            {
                Debug.Log("No available path?");
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        }
    }
    //MovePlayer() coroutine needed to wait for MoveTowards() to finish before looping again in the for
    IEnumerator MovePlayer(List<PathNode> path)
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 50f);
            Vector3 newPos = new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f;
            Debug.Log("Current pos: " + player.transform.position + " --- Moving to pos:" + newPos);
            Vector3 movePos = Vector3.MoveTowards(player.transform.position, newPos, 1f * Time.deltaTime);
            yield return new WaitForSeconds(0.2f);
            player.transform.position = newPos;
        }
        Debug.Log("Last pos: " + player.transform.position);
    }

    //-------- Helper functions that shouldnt be here --------
    public static int[] GetPlayerPosicion(Transform transform)
    {
        //Mathf.FloorToInt((worldPosition - originPosition).x / cellSize); ---> 5f and 10f hardcoded values
        Vector3 pos = transform.position;
        int x = Mathf.FloorToInt((pos - Vector3.one * 5f).x / 10f);
        int y = Mathf.FloorToInt((pos - Vector3.one * 5f).y / 10f);
        int[] xy = { x, y };
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
