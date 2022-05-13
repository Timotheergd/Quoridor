using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_mouvement : MonoBehaviour
{
    public float move_range;

    public GameObject player;
    public Vector2Int player_pos;
    public GameObject game;
    private int player_num;
    public GameObject shadow;
    public Material playerShadowTexture;

    private float time_between_turn;

    void Start()
    {
        move_range = 2.1f;
        player_num = player.GetComponent<Player>().player_num;
        time_between_turn = game.GetComponent<Game_stats>().time_between_turn;
    }

    void Update()
    {        
        if (game.GetComponent<Game_stats>().jump_choosing == false && (Time.time-game.GetComponent<Game_stats>().last_move_time) > time_between_turn)
        {
            // if it is not his turn, return
            if (MainManager.Instance.number_of_player == 2 && (game.GetComponent<Game_stats>().turn%2)+1 != player_num)
            {
                return;
            }
            if (MainManager.Instance.number_of_player == 4)
            {
                if ((player_num == 1 || player_num == 4) && (game.GetComponent<Game_stats>().turn%4)+1 != player_num){return;}
                if ( player_num == 2                     && (game.GetComponent<Game_stats>().turn%4)+1 != 3){return;}
                if ( player_num == 3                     && (game.GetComponent<Game_stats>().turn%4)+1 != 2){return;}
            }

            // Move the player relatively to him because the camera may have turn

            if (game.GetComponent<Game_stats>().is_Left_KeyPressed) // move left
            {          
                if (MainManager.Instance.autoRotateCam == true)
                {
                    if (player_num == 1) { move_left(); }
                    else if (player_num == 2) { move_right(); }
                    else if (player_num == 3) { move_up(); }
                    else if (player_num == 4) { move_down(); }
                }
                else { move_left(); }
            }
            else if (game.GetComponent<Game_stats>().is_Right_KeyPressed) // move right
            {
                if (MainManager.Instance.autoRotateCam == true)
                {
                    if (player_num == 1) { move_right(); }
                    else if (player_num == 2) { move_left(); }
                    else if (player_num == 3) { move_down(); }
                    else if (player_num == 4) { move_up(); }
                }
                else { move_right(); }
            } 
            else if (game.GetComponent<Game_stats>().is_Up_KeyPressed) // move up
            {
                if (MainManager.Instance.autoRotateCam == true)
                {
                    if (player_num == 1) { move_up(); }
                    else if (player_num == 2) { move_down(); }
                    else if (player_num == 3) { move_right(); }
                    else if (player_num == 4) { move_left(); }
                }
                else { move_up(); }
            } 
            else if (game.GetComponent<Game_stats>().is_Down_KeyPressed) //move down
            {
                if (MainManager.Instance.autoRotateCam == true)
                {
                    if (player_num == 1) { move_down(); }
                    else if (player_num == 2) { move_up(); }
                    else if (player_num == 3) { move_left(); }
                    else if (player_num == 4) { move_right(); }
                }
                else { move_down(); }
            }  
        }
    }

    private void move_left()
    {
        // Move the shadow to the player before he move
        shadow.transform.position = this.transform.position;
        shadow.GetComponent<MeshRenderer>().material = playerShadowTexture;

        int numberOfBlocsToMove = GameObject.Find("/Board").GetComponent<Game_grid>().move_player_left(player_pos, player_num); // get the number of blocs to move (may be 2 if the player jump over an other or 0 if he is blocked)
        if (numberOfBlocsToMove == 0) {return;} // the player didn't move
        player_pos[0] -= 2*numberOfBlocsToMove; // set the new position of the player in the grid
        this.transform.Translate(new Vector3 (move_range*numberOfBlocsToMove, 0f, 0f)); // move the player
        game.GetComponent<Game_stats>().end_turn();
    }

    private void move_right()
    {
        // Move the shadow to the player before he move
        shadow.transform.position = this.transform.position;
        shadow.GetComponent<MeshRenderer>().material = playerShadowTexture;

        int numberOfBlocsToMove = GameObject.Find("/Board").GetComponent<Game_grid>().move_player_right(player_pos, player_num);
        if (numberOfBlocsToMove == 0) {return;}
        player_pos[0] += 2*numberOfBlocsToMove;
        this.transform.Translate(new Vector3 (-move_range*numberOfBlocsToMove, 0f, 0f));
        game.GetComponent<Game_stats>().end_turn();
    }

    private void move_up()
    {
        // Move the shadow to the player before he move
        shadow.transform.position = this.transform.position;
        shadow.GetComponent<MeshRenderer>().material = playerShadowTexture;

        int numberOfBlocsToMove = GameObject.Find("/Board").GetComponent<Game_grid>().move_player_up(player_pos, player_num);
        if (numberOfBlocsToMove == 0) {return;}
        player_pos[1] -= 2*numberOfBlocsToMove;
        this.transform.Translate(new Vector3 (0f, 0f, -move_range*numberOfBlocsToMove));
        game.GetComponent<Game_stats>().end_turn();
    }

    private void move_down()
    {
        // Move the shadow to the player before he move
        shadow.transform.position = this.transform.position;
        shadow.GetComponent<MeshRenderer>().material = playerShadowTexture;

        int numberOfBlocsToMove = GameObject.Find("/Board").GetComponent<Game_grid>().move_player_down(player_pos, player_num);
        if (numberOfBlocsToMove == 0) {return;}
        player_pos[1] += 2*numberOfBlocsToMove;
        this.transform.Translate(new Vector3 (0f, 0f, move_range*numberOfBlocsToMove));
        game.GetComponent<Game_stats>().end_turn();
    }

    public void check_win()
    {
        //check if the player is on the other side of the map. If he is, he win
        if (player_num == 1)
        {
            if (player_pos[1] == 0)
            {
                GameObject.Find("/Canvas").GetComponent<WinMenu>().WinPause("Player Red");
            }
        }
        else if (player_num == 2)
        {
            if (player_pos[1] == 16)
            {
                GameObject.Find("/Canvas").GetComponent<WinMenu>().WinPause("Player Blue");
            }
        }
        else if (player_num == 3)
        {
            if (player_pos[0] == 16)
            {
                GameObject.Find("/Canvas").GetComponent<WinMenu>().WinPause("Player Yellow/Orange");
            }
        }
        else if (player_num == 4)
        {
            if (player_pos[0] == 0)
            {
                GameObject.Find("/Canvas").GetComponent<WinMenu>().WinPause("Player Green");
            }
        }
    }
}
