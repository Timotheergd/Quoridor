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

    [SerializeField]
    private GameObject playableMoveIndicator;

    private GameObject playableMoveIndicatorLeft;
    private GameObject playableMoveIndicatorRight;
    private GameObject playableMoveIndicatorUp;
    private GameObject playableMoveIndicatorDown;
    private GameObject playableMoveIndicatorUpLeft;
    private GameObject playableMoveIndicatorDownLeft;
    private GameObject playableMoveIndicatorUpRight;
    private GameObject playableMoveIndicatorDownRight;
    private GameObject playableMoveIndicatorLeftJump;
    private GameObject playableMoveIndicatorRightJump;
    private GameObject playableMoveIndicatorUpJump;
    private GameObject playableMoveIndicatorDownJump;

    private Vector2Int Left_VectorinGrid;
    private Vector2Int Right_VectorinGrid;
    private Vector2Int Up_VectorinGrid;
    private Vector2Int Down_VectorinGrid;
    private Vector2Int UpLeft_VectorinGrid;
    private Vector2Int DownLeft_VectorinGrid;
    private Vector2Int UpRight_VectorinGrid;
    private Vector2Int DownRight_VectorinGrid;
    private Vector2Int LeftJump_VectorinGrid;
    private Vector2Int RightJump_VectorinGrid;
    private Vector2Int UpJump_VectorinGrid;
    private Vector2Int DownJump_VectorinGrid;

    private float time_between_turn;

    void Start()
    {
        move_range = 2.1f;
        player_num = player.GetComponent<Player>().player_num;
        time_between_turn = game.GetComponent<Game_stats>().time_between_turn;

        Left_VectorinGrid      = new Vector2Int(-2, 0);
        Right_VectorinGrid     = new Vector2Int(2, 0);
        Up_VectorinGrid        = new Vector2Int(0, -2);
        Down_VectorinGrid      = new Vector2Int(0, 2);
        UpLeft_VectorinGrid    = new Vector2Int(-2, -2);
        DownLeft_VectorinGrid  = new Vector2Int(-2, 2);
        UpRight_VectorinGrid   = new Vector2Int(2, -2);
        DownRight_VectorinGrid = new Vector2Int(2, 2);
        LeftJump_VectorinGrid  = new Vector2Int(-4, 0);
        RightJump_VectorinGrid = new Vector2Int(4, 0);
        UpJump_VectorinGrid    = new Vector2Int(0, -4);
        DownJump_VectorinGrid  = new Vector2Int(0, 4);
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
        // get_playable_moves();
    }

    /*
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

    */
    private void move_left()
    {
        // Move the shadow to the player before he move
        shadow.transform.position = this.transform.position;
        shadow.GetComponent<MeshRenderer>().material = playerShadowTexture;

        Vector2Int numberOfBlocsToMove = GameObject.Find("/Board").GetComponent<Game_grid>().move_player_left(player_pos, player_num)[0]; // get the number of blocs to move (may be 2 if the player jump over an other or 0 if he is blocked)
        if (numberOfBlocsToMove.x == 0) {return;} // the player didn't move
        player_pos[0] -= 2*numberOfBlocsToMove.x; // set the new position of the player in the grid
        player_pos[1] -= 2*numberOfBlocsToMove.y;
        this.transform.Translate(new Vector3 (move_range*numberOfBlocsToMove.x, 0f, move_range*numberOfBlocsToMove.y)); // move the player
        game.GetComponent<Game_stats>().end_turn();
    }

    private void move_right()
    {
        // Move the shadow to the player before he move
        shadow.transform.position = this.transform.position;
        shadow.GetComponent<MeshRenderer>().material = playerShadowTexture;

        Vector2Int numberOfBlocsToMove = GameObject.Find("/Board").GetComponent<Game_grid>().move_player_right(player_pos, player_num)[0];
        if (numberOfBlocsToMove.x == 0) {return;}
        player_pos[0] += 2*numberOfBlocsToMove.x;
        player_pos[0] += 2*numberOfBlocsToMove.y;
        this.transform.Translate(new Vector3 (-move_range*numberOfBlocsToMove.x, 0f, -move_range*numberOfBlocsToMove.y));
        game.GetComponent<Game_stats>().end_turn();
    }

    private void move_up()
    {
        // Move the shadow to the player before he move
        shadow.transform.position = this.transform.position;
        shadow.GetComponent<MeshRenderer>().material = playerShadowTexture;

        Vector2Int numberOfBlocsToMove = GameObject.Find("/Board").GetComponent<Game_grid>().move_player_up(player_pos, player_num)[0];
        if (numberOfBlocsToMove.x == 0) {return;}
        player_pos[1] -= 2*numberOfBlocsToMove.x;
        player_pos[0] -= 2*numberOfBlocsToMove.y;
        this.transform.Translate(new Vector3 (-move_range*numberOfBlocsToMove.y, 0f, -move_range*numberOfBlocsToMove.x));
        game.GetComponent<Game_stats>().end_turn();
    }

    private void move_down()
    {
        // Move the shadow to the player before he move
        shadow.transform.position = this.transform.position;
        shadow.GetComponent<MeshRenderer>().material = playerShadowTexture;

        Vector2Int numberOfBlocsToMove = GameObject.Find("/Board").GetComponent<Game_grid>().move_player_down(player_pos, player_num)[0];
        if (numberOfBlocsToMove.x == 0) {return;}
        player_pos[1] += 2*numberOfBlocsToMove.x;
        player_pos[0] += 2*numberOfBlocsToMove.y;
        this.transform.Translate(new Vector3 (move_range*numberOfBlocsToMove.y, 0f, move_range*numberOfBlocsToMove.x));
        game.GetComponent<Game_stats>().end_turn();
    }

    public void check_win()
    {
        //check if the player is on the other side of the map. If he is, he win
        Debug.Log("begin check " + player_num.ToString());
        if (player_num == 1)
        {
            if (player_pos[1] == 0)
            {
                GameObject.Find("/Canvas").GetComponent<WinMenu>().WinPause(MainManager.Instance.is_modeFrancois ? "<color=red>François</color>" : "Player <color=red>Red</color>");
            }
        }
        else if (player_num == 2)
        {
            if (player_pos[1] == 16)
            {
                GameObject.Find("/Canvas").GetComponent<WinMenu>().WinPause(MainManager.Instance.is_modeFrancois ? "<color=blue>François</color>" : "Player <color=blue>Blue</color>");
            }
        }
        else if (player_num == 3)
        {
            if (player_pos[0] == 16)
            {
                GameObject.Find("/Canvas").GetComponent<WinMenu>().WinPause(MainManager.Instance.is_modeFrancois ? "<color=orange>François</color>" : "Player <color=orange>Orange</color>");
            }
        }
        else if (player_num == 4)
        {
            if (player_pos[0] == 0)
            {
                GameObject.Find("/Canvas").GetComponent<WinMenu>().WinPause(MainManager.Instance.is_modeFrancois ? "<color=green>François</color>" : "Player <color=green>Green</color>");
            }
        }
    }

    public void get_playable_moves()
    {
        List<Vector2Int> playableMoves = new List<Vector2Int>();

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
        
        foreach(Vector2Int playableMove in GameObject.Find("/Board").GetComponent<Game_grid>().move_player_up(player_pos, player_num, true))
        {
            if (playableMoves.Contains(playableMove) == false && !(playableMove == Vector2Int.zero))
            {
                playableMoves.Add(new Vector2Int(-playableMove.y*2, -playableMove.x*2));
            }
        }
        foreach(Vector2Int playableMove in GameObject.Find("/Board").GetComponent<Game_grid>().move_player_down(player_pos, player_num, true))
        {
            if (playableMoves.Contains(playableMove) == false && !(playableMove == Vector2Int.zero))
            {
                playableMoves.Add(new Vector2Int(playableMove.y*2, playableMove.x*2));
            }
        }
        foreach(Vector2Int playableMove in GameObject.Find("/Board").GetComponent<Game_grid>().move_player_left(player_pos, player_num, true))
        {
            if (playableMoves.Contains(playableMove) == false && !(playableMove == Vector2Int.zero))
            {
                playableMoves.Add(new Vector2Int(-playableMove.x*2, -playableMove.y*2));
            }
        }
        // string s="playrmouvementbr\n";
        // foreach(Vector2Int v in playableMoves)
        // {
        //     s+= v.x.ToString() + " " + v.y.ToString() + "\n";
        // }
        // Debug.Log(s);
        foreach(Vector2Int playableMove in GameObject.Find("/Board").GetComponent<Game_grid>().move_player_right(player_pos, player_num, true))
        {
            if (playableMoves.Contains(playableMove) == false && !(playableMove == Vector2Int.zero))
            {
                playableMoves.Add(new Vector2Int(playableMove.x*2, playableMove.y*2));
            }
        }
        // s="playrmouvement\n";
        // foreach(Vector2Int v in playableMoves)
        // {
        //     s+= v.x.ToString() + " " + v.y.ToString() + "\n";
        // }
        // Debug.Log(s);
        create_playableMovesIndicators(playableMoves);
        
    }

    private void create_playableMovesIndicators(List<Vector2Int> playableMoves)
    {
        foreach(Vector2Int playableMovePosition in playableMoves)
        {
            if (playableMovePosition == Left_VectorinGrid) // left
            {   
                // Debug.Log(playableMovePosition);
                if (playableMoveIndicatorLeft != null){ Destroy(playableMoveIndicatorLeft);}
                else
                {
                    // create a clickable object who will move the player
                    playableMoveIndicatorLeft = Instantiate(playableMoveIndicator);
                    playableMoveIndicatorLeft.name = "playableMoveIndicatorLeft";
                }
                playableMoveIndicatorLeft.transform.position = GameObject.Find("/Board").GetComponent<Game_grid>().Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+playableMovePosition.x,player_pos[1]+playableMovePosition.y));

            }
            else if (playableMovePosition == Right_VectorinGrid) // right
            {   
                if (playableMoveIndicatorRight != null){ Destroy(playableMoveIndicatorRight);}
                else
                {
                    // create a clickable object who will move the player
                    playableMoveIndicatorRight = Instantiate(playableMoveIndicator);
                    playableMoveIndicatorRight.name = "playableMoveIndicatorRight";
                }
                playableMoveIndicatorRight.transform.position = GameObject.Find("/Board").GetComponent<Game_grid>().Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+playableMovePosition.x,player_pos[1]+playableMovePosition.y));

            }
            else if (playableMovePosition == Up_VectorinGrid) // up
            {   
                if (playableMoveIndicatorUp != null){ Destroy(playableMoveIndicatorUp);}
                else
                {
                    // create a clickable object who will move the player
                    playableMoveIndicatorUp = Instantiate(playableMoveIndicator);
                    playableMoveIndicatorUp.name = "playableMoveIndicatorUp";
                }
                playableMoveIndicatorUp.transform.position = GameObject.Find("/Board").GetComponent<Game_grid>().Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+playableMovePosition.x,player_pos[1]+playableMovePosition.y));

            }
            else if (playableMovePosition == Down_VectorinGrid) // down
            {   
                if (playableMoveIndicatorDown != null){ Destroy(playableMoveIndicatorDown);}
                else
                {
                    // create a clickable object who will move the player
                    playableMoveIndicatorDown = Instantiate(playableMoveIndicator);
                    playableMoveIndicatorDown.name = "playableMoveIndicatorDown";
                }
                playableMoveIndicatorDown.transform.position = GameObject.Find("/Board").GetComponent<Game_grid>().Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+playableMovePosition.x,player_pos[1]+playableMovePosition.y));

            }
            else if (playableMovePosition == UpLeft_VectorinGrid) // up left
            {   
                if (playableMoveIndicatorUpLeft != null){ Destroy(playableMoveIndicatorUpLeft);}
                else
                {
                    // create a clickable object who will move the player
                    playableMoveIndicatorUpLeft = Instantiate(playableMoveIndicator);
                    playableMoveIndicatorUpLeft.name = "playableMoveIndicatorUpLeft";
                }
                playableMoveIndicatorUpLeft.transform.position = GameObject.Find("/Board").GetComponent<Game_grid>().Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+playableMovePosition.x,player_pos[1]+playableMovePosition.y));

            }
            else if (playableMovePosition == DownLeft_VectorinGrid) // up right
            {   
                if (playableMoveIndicatorUpRight != null){ Destroy(playableMoveIndicatorUpRight);}
                else
                {
                    // create a clickable object who will move the player
                    playableMoveIndicatorUpRight = Instantiate(playableMoveIndicator);
                    playableMoveIndicatorUpRight.name = "playableMoveIndicatorUpRight";
                }
                playableMoveIndicatorUpRight.transform.position = GameObject.Find("/Board").GetComponent<Game_grid>().Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+playableMovePosition.x,player_pos[1]+playableMovePosition.y));

            }
            else if (playableMovePosition == UpRight_VectorinGrid) // down left
            {   
                if (playableMoveIndicatorDownLeft != null){ Destroy(playableMoveIndicatorDownLeft);}
                else
                {
                    // create a clickable object who will move the player
                    playableMoveIndicatorDownLeft = Instantiate(playableMoveIndicator);
                    playableMoveIndicatorDownLeft.name = "playableMoveIndicatorDownLeft";
                }
                playableMoveIndicatorDownLeft.transform.position = GameObject.Find("/Board").GetComponent<Game_grid>().Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+playableMovePosition.x,player_pos[1]+playableMovePosition.y));

            }
            else if (playableMovePosition == DownRight_VectorinGrid) // down right
            {   
                if (playableMoveIndicatorDownRight != null){ Destroy(playableMoveIndicatorDownRight);}
                else
                {
                    // create a clickable object who will move the player
                    playableMoveIndicatorDownRight = Instantiate(playableMoveIndicator);
                    playableMoveIndicatorDownRight.name = "playableMoveIndicatorDownRight";
                }
                playableMoveIndicatorDownRight.transform.position = GameObject.Find("/Board").GetComponent<Game_grid>().Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+playableMovePosition.x,player_pos[1]+playableMovePosition.y));

            } 
            else if (playableMovePosition == LeftJump_VectorinGrid) // left jump
            {   
                if (playableMoveIndicatorLeftJump != null){ Destroy(playableMoveIndicatorLeftJump);}
                else
                {
                    // create a clickable object who will move the player
                    playableMoveIndicatorLeftJump = Instantiate(playableMoveIndicator);
                    playableMoveIndicatorLeftJump.name = "playableMoveIndicatorLeftJump";
                }
                playableMoveIndicatorLeftJump.transform.position = GameObject.Find("/Board").GetComponent<Game_grid>().Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+playableMovePosition.x,player_pos[1]+playableMovePosition.y));

            }
            else if (playableMovePosition == RightJump_VectorinGrid) // right jump
            {   
                if (playableMoveIndicatorRightJump != null){ Destroy(playableMoveIndicatorRightJump);}
                else
                {
                    // create a clickable object who will move the player
                    playableMoveIndicatorRightJump = Instantiate(playableMoveIndicator);
                    playableMoveIndicatorRightJump.name = "playableMoveIndicatorRightJump";
                }
                playableMoveIndicatorRightJump.transform.position = GameObject.Find("/Board").GetComponent<Game_grid>().Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+playableMovePosition.x,player_pos[1]+playableMovePosition.y));

            }
            else if (playableMovePosition == UpJump_VectorinGrid) // up jump
            {   
                if (playableMoveIndicatorUpJump != null){ Destroy(playableMoveIndicatorUpJump);}
                else
                {
                    // create a clickable object who will move the player
                    playableMoveIndicatorUpJump = Instantiate(playableMoveIndicator);
                    playableMoveIndicatorUpJump.name = "playableMoveIndicatorUpJump";
                }
                playableMoveIndicatorUpJump.transform.position = GameObject.Find("/Board").GetComponent<Game_grid>().Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+playableMovePosition.x,player_pos[1]+playableMovePosition.y));

            }
            else if (playableMovePosition == DownJump_VectorinGrid) // down jump
            {   
                if (playableMoveIndicatorDownJump != null){ Destroy(playableMoveIndicatorDownJump);}
                else
                {
                    // create a clickable object who will move the player
                    playableMoveIndicatorDownJump = Instantiate(playableMoveIndicator);
                    playableMoveIndicatorDownJump.name = "playableMoveIndicatorDownJump";
                }
                playableMoveIndicatorDownJump.transform.position = GameObject.Find("/Board").GetComponent<Game_grid>().Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+playableMovePosition.x,player_pos[1]+playableMovePosition.y));

            }
        }
    }
}