using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragging : MonoBehaviour
{
    private float dist;
    private bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;
    private Vector2Int wall_gridpos=new Vector2Int();
 
    void Update()
    {        
        Vector3 v3;
 
        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }
 
        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;
 
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
 
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Wall")
                {
                    toDrag = hit.transform;
                    dist = Mathf.Sqrt(Mathf.Pow(hit.transform.position.x - Camera.main.transform.position.x, 2) + Mathf.Pow(hit.transform.position.y - Camera.main.transform.position.y, 2) + Mathf.Pow(hit.transform.position.z - Camera.main.transform.position.z, 2));
                    v3 = new Vector3(pos.x, pos.y, dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    offset = toDrag.position - v3;
                    dragging = true;
                }
            }
        }
 
        if (dragging && touch.phase == TouchPhase.Moved)
        {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);

            wall_gridpos = GameObject.Find("/WallPlacementCollider").GetComponent<WallPlacementController>().Wall_Worldpos_To_Gridpos((float)(GameObject.Find("/WallPlacementCollider").GetComponent<WallPlacementController>().roundTo2point1(v3.x)+1.05f), (float)(GameObject.Find("/WallPlacementCollider").GetComponent<WallPlacementController>().roundTo2point1(v3.z)+1.05f));
            wall_gridpos = new Vector2Int(Mathf.Min(Mathf.Max(wall_gridpos.x, 1), 15), Mathf.Min(Mathf.Max(wall_gridpos.y, 1), 15));
            GameObject.Find("/WallPlacementCollider").GetComponent<WallPlacementController>().currentPlaceableObject_grid_position = wall_gridpos;
            GameObject.Find("/WallPlacementCollider").GetComponent<WallPlacementController>().hitPoint = v3;
            toDrag.position = GameObject.Find("/WallPlacementCollider").GetComponent<WallPlacementController>().Wall_Gridpos_To_Worldpos(wall_gridpos); // move the wall
        }
 
        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
           dragging = false;
        }
    }
}
