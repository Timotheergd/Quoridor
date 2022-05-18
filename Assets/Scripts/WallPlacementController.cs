using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject placeableObjectPrefab;
    public GameObject game;

    [SerializeField]
    private Material existingWallMaterial;
    [SerializeField]
    private Material wellPlacedWallMaterial;
    [SerializeField]
    private Material wrongPlacedWallMaterial;
    
    public Vector3 hitPoint;

    private GameObject currentPlaceableObject;

    private Vector3 currentPlaceableObject_position;
    private Vector3 currentPlaceableObject_last_position;
    private Quaternion currentPlaceableObject_rotation;
    private Quaternion currentPlaceableObject_last_rotation;

    public Vector2Int currentPlaceableObject_grid_position;

    private float mouseWheelRotation;

    [SerializeField]
    private float time_between_wall_move;
    private float last_time_wall_move;

    private bool move;

    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {    
                MoveCurrentObjectToMouse();
                RotateFromMouseWheel();
                ReleaseIfClicked();            
        }
    }

    private void HandleNewObjectHotkey()
    {
        if (game.GetComponent<Game_stats>().is_handleWall_KeyPressed)
        {
            // if the player need to choose where to jump, the jumping indicators are destroyed so he can plaace a wall
            if (GameObject.Find("/Game").GetComponent<Game_stats>().jump_choosing)
            {
                // remove the inicators
                GameObject[] playableMoves;
                playableMoves = GameObject.FindGameObjectsWithTag("playableMove");
                foreach (GameObject playableMove in playableMoves)
                {
                    Destroy(playableMove);
                }
                GameObject.Find("/Game").GetComponent<Game_stats>().jump_choosing = false;
            }

            GameObject player = GameObject.Find("/Game").GetComponent<Game_stats>().get_current_player_object();

            //test if there are wall left to the player
            if (!player.GetComponent<Player>().as_wall()) return;

            if (currentPlaceableObject != null)
            {
                Destroy(currentPlaceableObject);
                //Set the text of the button to Wall
                GameObject.Find("/AndroidUI").GetComponent<AndroidUiManager>().DisplayWallrelatedButton(false);
            }
            else
            {
                // Create a new wall
                currentPlaceableObject = Instantiate(placeableObjectPrefab);
                // get the position of the wall 
                currentPlaceableObject_grid_position = Wall_Worldpos_To_Gridpos((float)(roundTo2point1(hitPoint.x)+1.05f), (float)(roundTo2point1(hitPoint.z)+1.05f));
                currentPlaceableObject_grid_position = new Vector2Int(Mathf.Min(Mathf.Max(currentPlaceableObject_grid_position.x, 1), 15), Mathf.Min(Mathf.Max(currentPlaceableObject_grid_position.y, 1), 15));
                
                if (GameObject.Find("/Board").GetComponent<Game_grid>().poseable(currentPlaceableObject_grid_position, is_rotated()))
                {
                    //set color
                    currentPlaceableObject.GetComponent<MeshRenderer>().material = wellPlacedWallMaterial;
                }
                else
                {
                    //set color
                    currentPlaceableObject.GetComponent<MeshRenderer>().material = wrongPlacedWallMaterial;
                }
                currentPlaceableObject.transform.localScale *= 1.01f; // Upscale to make it apear over the existing walls
                GameObject.Find("/Game").GetComponent<Game_stats>().is_placing_wall = true;

                //Set the text of the button to cancel
                GameObject.Find("/AndroidUI").GetComponent<AndroidUiManager>().DisplayWallrelatedButton(true);
            }
        }
    }

    private void MoveCurrentObjectToMouse()
    {
        if (!GameObject.Find("/Game").GetComponent<Game_stats>().jump_choosing && GameObject.Find("/Game").GetComponent<Game_stats>().is_placing_wall == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                // hitPoint.x = hitInfo.point.x;
                // hitPoint.y = hitInfo.point.y;
                // Debug.Log("Cam raycast in wall placement controler");
            }
            else
            {
                if (Time.time-last_time_wall_move > time_between_wall_move)
                {
                    Vector2 to_move = new Vector2(0, 0);
                    move = false;

                    // check if a key was pressed
                    if (game.GetComponent<Game_stats>().is_Wall_Left_KeyPressed)
                    {
                        to_move.x += 2.1f;
                        move = true;
                    }
                    else if (game.GetComponent<Game_stats>().is_Wall_Right_KeyPressed)
                    {
                        to_move.x += -2.1f;
                        move = true;
                    }
                    else if (game.GetComponent<Game_stats>().is_Wall_Down_KeyPressed)
                    {
                        to_move.y += 2.1f;
                        move = true;
                    }
                    else if (game.GetComponent<Game_stats>().is_Wall_Up_KeyPressed)
                    {
                        to_move.y += -2.1f;
                        move = true;
                    }

                    if (move == true) // if a key was pressed
                    {
                        last_time_wall_move = Time.time;
                        if (MainManager.Instance.autoRotateCam == true)
                        {
                            // get the player num
                            int player_num = 0;
                            if (MainManager.Instance.number_of_player == 4)
                            {
                                player_num = (GameObject.Find("/Game").GetComponent<Game_stats>().turn%4)+1;
                                if (player_num == 2) player_num = 3;
                                else if (player_num == 3) player_num = 2;
                            }
                            else if (MainManager.Instance.number_of_player == 2)
                            {
                                player_num = (GameObject.Find("/Game").GetComponent<Game_stats>().turn%2)+1;
                            } 

                            // Change relative to the player
                            if (player_num == 1)
                            {
                                //Great that's the default
                            }
                            else if (player_num == 2)
                            {
                                to_move.x = -to_move.x;
                                to_move.y = -to_move.y;
                            }
                            else if (player_num == 3)
                            {
                                float x = to_move.x;
                                to_move.x = to_move.y;
                                to_move.y = -x;
                            }
                            else if (player_num == 4)
                            {
                                float x = to_move.x;
                                to_move.x = -to_move.y;
                                to_move.y = x;
                            }
                        }
                        move = false;

                        // Set the position of the wall in the world with min max to stay in the grid
                        hitPoint.x += to_move.x;
                        hitPoint.x = Mathf.Min(Mathf.Max(hitPoint.x, -14f), 1.1f);
                        hitPoint.z += to_move.y;
                        hitPoint.z = Mathf.Min(Mathf.Max(hitPoint.z, -7.4f), 7.4f);
                    }
                }
                
                // convert the position of the wall in the world with min max to stay in the grid
                currentPlaceableObject_grid_position = Wall_Worldpos_To_Gridpos((float)(roundTo2point1(hitPoint.x)+1.05f), (float)(roundTo2point1(hitPoint.z)+1.05f));
                currentPlaceableObject_grid_position = new Vector2Int(Mathf.Min(Mathf.Max(currentPlaceableObject_grid_position.x, 1), 15), Mathf.Min(Mathf.Max(currentPlaceableObject_grid_position.y, 1), 15));

                currentPlaceableObject.transform.position = Wall_Gridpos_To_Worldpos(currentPlaceableObject_grid_position); // move the wall
                currentPlaceableObject.transform.Rotate(Vector3.up, 0, 0); // force the x rotation so the wall doesn't turn

                if (currentPlaceableObject.transform.position != currentPlaceableObject_last_position || currentPlaceableObject.transform.rotation != currentPlaceableObject_last_rotation) // check only if the wall moved
                {
                    if (GameObject.Find("/Board").GetComponent<Game_grid>().poseable(currentPlaceableObject_grid_position, is_rotated()))
                    {
                        //set color
                        currentPlaceableObject.GetComponent<MeshRenderer>().material = wellPlacedWallMaterial;
                    }
                    else
                    {
                        //set color
                        currentPlaceableObject.GetComponent<MeshRenderer>().material = wrongPlacedWallMaterial;
                    }
                    currentPlaceableObject_last_position = currentPlaceableObject.transform.position;
                    currentPlaceableObject_last_rotation = currentPlaceableObject.transform.rotation;
                }
            }
        }
        else
        {
            GameObject.Find("/Game").GetComponent<Game_stats>().is_placing_wall = false;
            if (currentPlaceableObject != null)
            {
                Destroy(currentPlaceableObject);
                //Set the text of the button to Wall
                GameObject.Find("/AndroidUI").GetComponent<AndroidUiManager>().DisplayWallrelatedButton(false);
            }
        }
    }

    private void RotateFromMouseWheel()
    {
        // if (Input.mouseScrollDelta.y != 0f)
        if (game.GetComponent<Game_stats>().is_rotateWall_KeyPressed)
        {
            currentPlaceableObject.transform.rotation *= Quaternion.Euler(0, 90, 0);
        }
    }

    private void ReleaseIfClicked()
    {
        // if (Input.GetMouseButtonDown(0))
        if (game.GetComponent<Game_stats>().is_releaseWall_KeyPressed)
        {
            if (GameObject.Find("/Board").GetComponent<Game_grid>().poseable(currentPlaceableObject_grid_position, is_rotated()))
            {
                GameObject.Find("/Board").GetComponent<Game_grid>().add_wall(currentPlaceableObject_grid_position, is_rotated()); // add the wall in the grid
                currentPlaceableObject.transform.localScale *= 100f/101f; // resize it to his ooriginal size
                currentPlaceableObject.GetComponent<MeshRenderer>().material = existingWallMaterial; // chage th texture
                currentPlaceableObject = null;

                GameObject.Find("/AndroidUI").GetComponent<AndroidUiManager>().DisplayWallrelatedButton(false); //Set the text of the button to wall
                game.GetComponent<Game_stats>().end_turn();
            }
        }
    }

    public float roundTo2point1(double pos)
    {
        int neg = 1; // Store if the number is negative
        if (pos < 0)
        { 
            pos = -pos;
            neg = -1;
        }

        float roundedpos;
        int r = ((int)(pos*10))%21;
        if (r < 11)
        {
            roundedpos = ((int)(pos*10)) - r;
        }
        else
        {
            roundedpos = ((int)(pos*10)) + (21-r);
        }
        return (float)(neg*roundedpos/10);
    }

    public Vector2Int Wall_Worldpos_To_Gridpos(float worldposx, float worldposz)
    {
        int x = (int)(((((worldposx * -1) +1.1) /2.1) *2) +1);
        int y = (int)(((worldposz+7.3) /2.1) *2);
        return new Vector2Int(x, y);
    }

    public Vector3 Wall_Gridpos_To_Worldpos(Vector2Int gridpos)
    {
        float x = (float)(((((gridpos.x-1) /2) *2.1) -1.05) *-1);
        float y = (float)((((gridpos.y-1) /2) *2.1) -7.35);
        return new Vector3(x, 1.1f, y);
    }

    public bool is_rotated()
    {
        // rotated if it is horizonal, if it is on the x
        float rotation = currentPlaceableObject.transform.eulerAngles.y;
        if (rotation%180 == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

