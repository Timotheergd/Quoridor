using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_stats : MonoBehaviour
{
    public int turn;
    
    public float last_move_time;
    public float time_between_turn;

    public bool jump_choosing;
    public bool is_placing_wall;

    [SerializeField]
    public GameObject Cam;

    [SerializeField]
    private KeyCode newObjectHotkey = KeyCode.M;
    [SerializeField]
    private KeyCode releaseWAll_Key = KeyCode.P;

    [SerializeField]
    private Material player1Material;
    [SerializeField]
    private Material player2Material;

    public bool is_Left_KeyPressed;
    public bool is_Down_KeyPressed;
    public bool is_Right_KeyPressed;
    public bool is_Up_KeyPressed;

    public bool is_handleWall_KeyPressed;
    public bool is_releaseWall_KeyPressed;
    public bool is_rotateWall_KeyPressed;
    
    public bool is_Wall_Left_KeyPressed;
    public bool is_Wall_Down_KeyPressed;
    public bool is_Wall_Right_KeyPressed;
    public bool is_Wall_Up_KeyPressed;

    void Start()
    {
        // Get the players and the cube 
        GameObject player1 = GameObject.Find("Player1");
        player1.GetComponent<Player>().player_num = 1;

        GameObject player2 = GameObject.Find("Player2");
        player2.GetComponent<Player>().player_num = 2;
        GameObject player2Cube = GameObject.Find("Player2Cube");
        
        GameObject player3 = GameObject.Find("Player3");
        GameObject player3Cube = GameObject.Find("Player3Cube");

        GameObject player4 = GameObject.Find("Player4");
        GameObject player4Cube = GameObject.Find("Player4Cube");

        if (MainManager.Instance.number_of_player == 2)
        {
            // destroy the objects not needed for the game
            Destroy(player3);
            Destroy(player4);
            Destroy(player3Cube);
            Destroy(player4Cube);

            if (MainManager.Instance.autoRotateCam == false)
            {
                Destroy(player2Cube);
            }
        }
        else
        {
            player3.GetComponent<Player>().player_num = 3;
            player4.GetComponent<Player>().player_num = 4;
        }

        turn = 0;
        last_move_time = 0f;
    }

    void LateUpdate()
    {
        // reset and check the pressed keys 
        resetKeys();

        if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.Q)) is_Left_KeyPressed = true;
        if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) is_Down_KeyPressed = true;
        if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) is_Right_KeyPressed = true;
        if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.Z)) is_Up_KeyPressed = true;

        if (Input.GetKey (newObjectHotkey)) is_handleWall_KeyPressed = true;
        if (Input.GetKey (releaseWAll_Key)) is_releaseWall_KeyPressed = true;
        if (Input.mouseScrollDelta.y != 0f) is_rotateWall_KeyPressed = true;
    }

    public void end_turn()
    {
        is_placing_wall = false;

        GameObject.Find("/Player" + ((turn%2)+1).ToString()).GetComponent<Player_mouvement>().check_win(); // check if the player win

        GameObject.Find("/AudioManager").GetComponent<AudioManager>().Play("NextTurn"); // play a sound

        turn++; // increment the number of turn
        last_move_time = Time.time;

        if (MainManager.Instance.autoRotateCam == true)
        {
            // rotate the  camera if needed
            Cam.GetComponent<CamController>().turnCam();
        }

        if (MainManager.Instance.number_of_player == 2 && MainManager.Instance.autoRotateCam == false)
        {
            // Display the number of wall and change th color of the cube if there are 2 players and no rotation of the cam so the players can see who's turn it is and how many wall they got left
            int player_num = (turn%2)+1;       
            
            if (player_num == 1)
            {
                GameObject.Find("/Player1Cube").GetComponent<MeshRenderer>().material = player1Material;
            }
            else if (player_num == 2)
            {
                GameObject.Find("/Player1Cube").GetComponent<MeshRenderer>().material = player2Material;
            }
            GameObject.Find("Player1Cube").GetComponent<WallLeftDisplay>().DisplayWallLeft();
        }
    }

    public void resetKeys()
    {
        is_Left_KeyPressed = false;
        is_Down_KeyPressed = false;
        is_Right_KeyPressed = false;
        is_Up_KeyPressed = false;
        is_handleWall_KeyPressed = false;
        is_releaseWall_KeyPressed = false;
        is_rotateWall_KeyPressed = false;
        is_Wall_Left_KeyPressed = false;
        is_Wall_Down_KeyPressed = false;
        is_Wall_Right_KeyPressed = false;
        is_Wall_Up_KeyPressed = false;
    }
}
