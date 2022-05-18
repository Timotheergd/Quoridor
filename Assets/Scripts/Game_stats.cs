using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_stats : MonoBehaviour
{
    public int turn;
    
    public float last_move_time;
    public float time_between_turn;

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

    [SerializeField]
    private Material francoisMaterial;
    [SerializeField]
    private Material francoisMaterial_table;
    [SerializeField]
    private Material player1_francois_material;
    [SerializeField]
    private Material player1Cube_francois_material;
    [SerializeField]
    private Material player1Bar_francois_material;
    [SerializeField]
    private Material player2_francois_material;
    [SerializeField]
    private Material player2Cube_francois_material;
    [SerializeField]
    private Material player2Bar_francois_material;
    [SerializeField]
    private Material player3_francois_material;
    [SerializeField]
    private Material player3Cube_francois;
    [SerializeField]
    private Material player3Bar_francois;
    [SerializeField]
    private Material player4_francois;
    [SerializeField]
    private Material player4Cube_francois;
    [SerializeField]
    private Material player4Bar_francois;
    

    public bool jump_choosing;
    public bool is_placing_wall;

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

    public bool is_player_moving;

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

            if (MainManager.Instance.is_modeFrancois)
            {

                GameObject player3Bar = GameObject.Find("Player3Cube/ColorIndicator");
                GameObject player4Bar = GameObject.Find("Player4Cube/ColorIndicator");
                
                player3.GetComponent<MeshRenderer>().material = player3_francois_material;
                player4.GetComponent<MeshRenderer>().material = player4_francois;
                player3Cube.GetComponent<MeshRenderer>().material = player3Cube_francois;
                player4Cube.GetComponent<MeshRenderer>().material = player4Cube_francois;
                player3Bar.GetComponent<MeshRenderer>().material = player3Bar_francois;
                player4Bar.GetComponent<MeshRenderer>().material = player4Bar_francois;
            }
        }

        if (MainManager.Instance.is_modeFrancois)
        {
            GameObject[] francoisObjects = GameObject.FindGameObjectsWithTag("Francois");
            foreach(GameObject francois in francoisObjects)
            {
                francois.GetComponent<MeshRenderer>().material = francoisMaterial;
            }
            
            GameObject player1Cube = GameObject.Find("Player1Cube");
            GameObject player1Bar = GameObject.Find("Player1Cube/ColorIndicator");
            GameObject player2Bar = GameObject.Find("Player2Cube/ColorIndicator");

            player1.GetComponent<MeshRenderer>().material = player1_francois_material;
            player1Cube.GetComponent<MeshRenderer>().material = player1Cube_francois_material;
            player1Bar.GetComponent<MeshRenderer>().material = player1Bar_francois_material;
            player2.GetComponent<MeshRenderer>().material = player2_francois_material;
            player2Cube.GetComponent<MeshRenderer>().material = player2Cube_francois_material;
            player2Bar.GetComponent<MeshRenderer>().material = player2Bar_francois_material;

            GameObject table = GameObject.Find("Table");
            table.GetComponent<MeshRenderer>().material = francoisMaterial_table;
            GameObject baseboard = GameObject.Find("Board/Cube");
            baseboard.GetComponent<MeshRenderer>().material = francoisMaterial_table;

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

        GameObject.Find("/Player" + (get_current_player_num().ToString())).GetComponent<Player_mouvement>().check_win(); // check if the player win

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

    public int get_current_player_num()
    {
        int player_num = 0;
        if (MainManager.Instance.number_of_player == 4)
        {
            player_num = (turn%4)+1;
            if (player_num == 2) player_num = 3;
            else if (player_num == 3) player_num = 2;
        }
        else if (MainManager.Instance.number_of_player == 2)
        {
            player_num = (turn%2)+1;
        }
        return player_num;
    }

    public GameObject get_current_player_object()
    {
        string player_name = "/Player" + get_current_player_num().ToString();
        GameObject player = GameObject.Find(player_name);
        return player;
    }

    public void Set_jump_choosing(bool value)
    {
        jump_choosing = value;
    }
}
