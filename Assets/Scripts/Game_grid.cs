using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_grid : MonoBehaviour
{

    public int[,] grid;
    private int wall_num;

    [SerializeField]
    private GameObject playableMoveIndicator;

    private GameObject playableMoveIndicatorLeft;
    private GameObject playableMoveIndicatorRight;

    void Start()
    {
        grid = new int[17,17];
        wall_num = 5;

        // Place the 4 player
        grid[8,16] = 1;
        grid[8,0] = 2;
        if (MainManager.Instance.number_of_player == 4)
        {
            grid[0,8] = 3;
            grid[0,16] = 4;
        }
    }

    public int move_player_left(Vector2Int player_pos, int player_num)
    {
        /*
            Move the player in the grid and ask where to jump if it is in diagonal

            :input: - position of the player (Vector2Int)
                    - number of the player in the grid (int)


            :output: - number of blocs to move in the direction (int) (0, 1 or 2)
        */

        if (player_pos[0] < 2){return 0;} // if player will go out of the grid retur false and exit
        if (grid[player_pos[0]-1,player_pos[1]] == wall_num){return 0;} // if there is a wall, return false and exit

        if (grid[player_pos[0]-2,player_pos[1]] >= 1 && grid[player_pos[0]-2,player_pos[1]] <= 4) // if the player need to jump over an other player to move
        {
            // if (player_pos[0] < 4){return 0;} // if the player is too close to the border to jump
            if (player_pos[0] < 4 || grid[player_pos[0]-3,player_pos[1]] == wall_num ||  (grid[player_pos[0]-4,player_pos[1]] >= 1 && grid[player_pos[0]-4,player_pos[1]] <= 4)) // if there is a wall or a player behind the player to jump by
            {
                //check up and down (relative right and left)
                bool can_jump_diaonal = false;
                if (grid[player_pos[0]-2,player_pos[1]-1] == 0 && grid[player_pos[0]-2,player_pos[1]-2] == 0)
                {
                    // can go up / right
                    if (playableMoveIndicatorRight != null)
                    {
                        Destroy(playableMoveIndicatorRight);
                    }
                    else
                    {
                        // create a clickable object who will move the player
                        playableMoveIndicatorRight = Instantiate(playableMoveIndicator);
                        playableMoveIndicatorRight.name = "playableMoveIndicatorRight";
                    }
                    playableMoveIndicatorRight.transform.position = Gridpos_To_Worldpos(new Vector2Int(player_pos[0]-2,player_pos[1]-2));
                    can_jump_diaonal = true;

                }
                if (grid[player_pos[0]-2,player_pos[1]+1] == 0 && grid[player_pos[0]-2,player_pos[1]+2] == 0)
                {
                    // can go down / left
                    if (playableMoveIndicatorLeft != null)
                    {
                        Destroy(playableMoveIndicatorLeft);
                    }
                    else
                    {
                        // create a clickable object who will move the player
                        playableMoveIndicatorLeft = Instantiate(playableMoveIndicator);
                        playableMoveIndicatorLeft.name = "playableMoveIndicatorLeft";
                    }
                    playableMoveIndicatorLeft.transform.position = Gridpos_To_Worldpos(new Vector2Int(player_pos[0]-2,player_pos[1]+2));
                    can_jump_diaonal = true;
                }
                if (can_jump_diaonal) GameObject.Find("/Game").GetComponent<Game_stats>().jump_choosing = true;
                return 0;
            }
            
            else if (grid[player_pos[0]-4,player_pos[1]] == 0) // else if there is no wall and nothing behind the player
            {
                //move of 2 blocs
                grid[player_pos[0],player_pos[1]] = 0;
                grid[player_pos[0]-4,player_pos[1]] = player_num;
                return 2;
            }

            return 1;
        }
        else
        {
            // move of 1 bloc
            grid[player_pos[0],player_pos[1]] = 0;
            grid[player_pos[0]-2,player_pos[1]] = player_num;
            return 1;
        } 
    }

    public int move_player_right(Vector2Int player_pos, int player_num)
    {
        /*
            Move the player in the grid and ask where to jump if it is in diagonal

            :input: - position of the player (Vector2Int)
                    - number of the player in the grid (int)


            :output: - number of blocs to move in the direction (int) (0, 1 or 2)
        */

        if (player_pos[0] > 14){return 0;} // if player will go out of the grid retur false and exit
        if (grid[player_pos[0]+1,player_pos[1]] == wall_num){return 0;} // if there is a wall, return false and exit

        if (grid[player_pos[0]+2,player_pos[1]] >= 1 && grid[player_pos[0]+2,player_pos[1]] <= 4) // if the player need to jump over an other player to move
        {
            if (player_pos[0] > 12 || grid[player_pos[0]+3,player_pos[1]] == wall_num ||  (grid[player_pos[0]+4,player_pos[1]] >= 1 && grid[player_pos[0]+4,player_pos[1]] <= 4)) // if there is a wall or a player behind the player to jump by
            {
                //check down and up (relative right and left)
                bool can_jump_diaonal = false;
                if (grid[player_pos[0]+2,player_pos[1]-1] == 0 && grid[player_pos[0]+2,player_pos[1]-2] == 0)
                {
                    // can go down / right
                    if (playableMoveIndicatorRight != null)
                    {
                        Destroy(playableMoveIndicatorRight);
                    }
                    else
                    {
                        // create a clickable object who will move the player
                        playableMoveIndicatorRight = Instantiate(playableMoveIndicator);
                        playableMoveIndicatorRight.name = "playableMoveIndicatorRight";
                    }
                    playableMoveIndicatorRight.transform.position = Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+2,player_pos[1]-2));
                    can_jump_diaonal = true;
                }
                if (grid[player_pos[0]+2,player_pos[1]+1] == 0 && grid[player_pos[0]+2,player_pos[1]+2] == 0)
                {
                    // can go up / left
                    if (playableMoveIndicatorLeft != null)
                    {
                        Destroy(playableMoveIndicatorLeft);
                    }
                    else
                    {
                        // create a clickable object who will move the player
                        playableMoveIndicatorLeft = Instantiate(playableMoveIndicator);
                        playableMoveIndicatorLeft.name = "playableMoveIndicatorLeft";
                    }
                    playableMoveIndicatorLeft.transform.position = Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+2,player_pos[1]+2));
                    can_jump_diaonal = true;
                }
                if (can_jump_diaonal) GameObject.Find("/Game").GetComponent<Game_stats>().jump_choosing = true;
                return 0;
            }
            
            else if (grid[player_pos[0]+4,player_pos[1]] == 0) // else if there if there is no wall and nothing behind the player
            {
                //move of 2 blocs
                grid[player_pos[0],player_pos[1]] = 0;
                grid[player_pos[0]+4,player_pos[1]] = player_num;
                return 2;
            }
            return 1; // just so the compilator doesn't show an error but should never go there in theory
        }
        else
        {
            // move of 1 bloc
            grid[player_pos[0],player_pos[1]] = 0;
            grid[player_pos[0]+2,player_pos[1]] = player_num;
            return 1;
        } 
    }

    public int move_player_up(Vector2Int player_pos, int player_num)
    {
        /*
            Move the player in the grid and ask where to jump if it is in diagonal

            :input: - position of the player (Vector2Int)
                    - number of the player in the grid (int)


            :output: - number of blocs to move in the direction (int) (0, 1 or 2)
        */

        if (player_pos[1] < 2){return 0;} // if player will go out of the grid retur false and exit
        if (grid[player_pos[0],player_pos[1]-1] == wall_num){return 0;} // if there is a wall, return false and exit

        if (grid[player_pos[0],player_pos[1]-2] >= 1 && grid[player_pos[0],player_pos[1]-2] <= 4) // if the player need to jump over an other player to move
        {
            if (player_pos[1] < 4 || grid[player_pos[0],player_pos[1]-3] == wall_num ||  (grid[player_pos[0],player_pos[1]-4] >= 1 && grid[player_pos[0],player_pos[1]-4] <= 4)) // if there is a wall or a player behind the player to jump by
            {
                //check up right and left
                bool can_jump_diaonal = false;
                if (grid[player_pos[0]+1,player_pos[1]-2] == 0 && grid[player_pos[0]+2,player_pos[1]-2] == 0)
                {
                    // can go right
                    if (playableMoveIndicatorRight != null)
                    {
                        Destroy(playableMoveIndicatorRight);
                    }
                    else
                    {
                        // create a clickable object who will move the player
                        playableMoveIndicatorRight = Instantiate(playableMoveIndicator);
                        playableMoveIndicatorRight.name = "playableMoveIndicatorRight";
                    }
                    playableMoveIndicatorRight.transform.position = Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+2,player_pos[1]-2));
                    can_jump_diaonal = true;
                }
                if (grid[player_pos[0]-1,player_pos[1]-2] == 0 && grid[player_pos[0]-2,player_pos[1]-2] == 0)
                {
                    // can go down / left
                    if (playableMoveIndicatorLeft != null)
                    {
                        Destroy(playableMoveIndicatorLeft);
                    }
                    else
                    {
                        // create a clickable object who will move the player
                        playableMoveIndicatorLeft = Instantiate(playableMoveIndicator);
                        playableMoveIndicatorLeft.name = "playableMoveIndicatorLeft";
                    }
                    playableMoveIndicatorLeft.transform.position = Gridpos_To_Worldpos(new Vector2Int(player_pos[0]-2,player_pos[1]-2));
                    can_jump_diaonal = true;
                }
                if (can_jump_diaonal) GameObject.Find("/Game").GetComponent<Game_stats>().jump_choosing = true;
                return 0;
            }
            
            else if (grid[player_pos[0],player_pos[1]-4] == 0) // else if there if there is no wall and nothing behind the player
            {
                //move of 2 blocs
                grid[player_pos[0],player_pos[1]] = 0;
                grid[player_pos[0],player_pos[1]-4] = player_num;
                return 2;
            }
            return 1; // just so the compilator doesn't show an error but should never go there in theory
        }
        else
        {
            // move of 1 bloc
            grid[player_pos[0],player_pos[1]] = 0;
            grid[player_pos[0],player_pos[1]-2] = player_num;
            return 1;
        } 
    }

    public int move_player_down(Vector2Int player_pos, int player_num)
    {
        /*
            Move the player in the grid and ask where to jump if it is in diagonal

            :input: - position of the player (Vector2Int)
                    - number of the player in the grid (int)


            :output: - number of blocs to move in the direction (int) (0, 1 or 2)
        */

        if (player_pos[1] > 14){return 0;} // if player will go out of the grid retur false and exit
        if (grid[player_pos[0],player_pos[1]+1] == wall_num){return 0;} // if there is a wall, return false and exit

        if (grid[player_pos[0],player_pos[1]+2] >= 1 && grid[player_pos[0],player_pos[1]+2] <= 4) // if the player need to jump over an other player to move
        {
            if (player_pos[1] > 12 || grid[player_pos[0],player_pos[1]+3] == wall_num ||  (grid[player_pos[0],player_pos[1]+4] >= 1 && grid[player_pos[0],player_pos[1]+4] <= 4)) // if there is a wall or a player behind the player to jump by
            {
                //check up right and left
                bool can_jump_diaonal = false;
                if (grid[player_pos[0]-1,player_pos[1]+2] == 0 && grid[player_pos[0]-2,player_pos[1]+2] == 0)
                {
                    // can go left (relative right)
                    if (playableMoveIndicatorRight != null)
                    {
                        Destroy(playableMoveIndicatorRight);
                    }
                    else
                    {
                        // create a clickable object who will move the player
                        playableMoveIndicatorRight = Instantiate(playableMoveIndicator);
                        playableMoveIndicatorRight.name = "playableMoveIndicatorRight";
                    }
                    playableMoveIndicatorRight.transform.position = Gridpos_To_Worldpos(new Vector2Int(player_pos[0]-2,player_pos[1]+2));
                    can_jump_diaonal = true;
                }
                if (grid[player_pos[0]+1,player_pos[1]+2] == 0 && grid[player_pos[0]+2,player_pos[1]+2] == 0)
                {
                    // can go right(relative left)
                    if (playableMoveIndicatorLeft != null)
                    {
                        Destroy(playableMoveIndicatorLeft);
                    }
                    else
                    {
                        // create a clickable object who will move the player
                        playableMoveIndicatorLeft = Instantiate(playableMoveIndicator);
                        playableMoveIndicatorLeft.name = "playableMoveIndicatorLeft";
                    }
                    playableMoveIndicatorLeft.transform.position = Gridpos_To_Worldpos(new Vector2Int(player_pos[0]+2,player_pos[1]+2));
                    can_jump_diaonal = true;
                }
                if (can_jump_diaonal) GameObject.Find("/Game").GetComponent<Game_stats>().jump_choosing = true;
                return 0;
            }
            
            else if (grid[player_pos[0],player_pos[1]+4] == 0) // else if there if there is no wall and nothing behind the player
            {
                //move of 2 blocs
                grid[player_pos[0],player_pos[1]] = 0;
                grid[player_pos[0],player_pos[1]+4] = player_num;
                return 2;
            }
            return 1; // just so the compilator doesn't show an error but should never go there in theory
        }
        else
        {
            // move of 1 bloc
            grid[player_pos[0],player_pos[1]] = 0;
            grid[player_pos[0],player_pos[1]+2] = player_num;
            return 1;
        } 
    }

    public bool poseable(Vector2Int center_pos, bool rotation)
    {
        //get the player number to get the current player
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
        string player_name = "/Player" + player_num.ToString();
        GameObject player = GameObject.Find(player_name);

        //test if there are wall left to the player
        if (!player.GetComponent<Player>().as_wall()) return false;

        // test if there is a wall bloking the placement of the new wall
        if (rotation == true)
        {
            if(grid[center_pos.x-1,center_pos.y] == wall_num) return false;
            if(grid[center_pos.x,center_pos.y] == wall_num) return false;
            if(grid[center_pos.x+1,center_pos.y] == wall_num) return false;
        }
        else
        {
            if(grid[center_pos.x,center_pos.y-1] == wall_num) return false;
            if(grid[center_pos.x,center_pos.y] == wall_num) return false;
            if(grid[center_pos.x,center_pos.y+1] == wall_num) return false;
        }

        // check that no player are blocked by the new wall
        if(!can_all_win(center_pos, rotation)) return false;

        return true;
    } 

   public void add_wall(Vector2Int wall_pos, bool is_rotated)
    {
        // place the wall in the grid in fonction of his rotation
        if (is_rotated == false)
        {
            grid[wall_pos[0], wall_pos[1]-1] = wall_num;
            grid[wall_pos[0], wall_pos[1]]   = wall_num;
            grid[wall_pos[0], wall_pos[1]+1] = wall_num;
        }
        else
        {
            grid[wall_pos[0]-1, wall_pos[1]] = wall_num;
            grid[wall_pos[0],   wall_pos[1]] = wall_num;
            grid[wall_pos[0]+1, wall_pos[1]] = wall_num;
        }

        //get the player number to get the current player
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
        string player_name = "/Player" + player_num.ToString();
        GameObject player = GameObject.Find(player_name);

        //remove 1 wall to the player
        player.GetComponent<Player>().decrement_wall();
    }

    public Vector2Int Worldpos_To_Gridpos(float worldposx, float worldposz)
    {
        // convert a world position of a player into a position on the grid
        int x = (int)((Mathf.Round(((worldposx * -1)) /2.1f) *2f) +2f);
        int z = (int)((Mathf.Round((worldposz) /2.1f) *2f) +8f);
        return new Vector2Int(x, z);
    }

    public Vector3 Gridpos_To_Worldpos(Vector2Int gridpos)
    {
        // convert a grid position of a player into a position in the world
        float x,z;

        if (gridpos.x == 0) x = 2.1f; //because the formula is not perfect :(
        else x = (float)(((((gridpos.x-1) /2) *2.1f)) *-1);

        if (gridpos.y == 0) z = -8.4f; //because the formula is not perfect :(
        else z = (float)((((gridpos.y-1) /2) *2.1f)-6.3);

        return new Vector3(x, 0.9f, z);
    }

    public void print_grid()
    {
        // Print the grid
        int[,] g=grid;
        string gs="";
        for(int i=0;i<17;i++)
        {
            for(int j=0;j<17;j++)
            {
                if(g[j,i] == 0)
                {
                    gs+=". ";
                }
                else
                {
                    gs+=g[j,i].ToString();
                }
            }
            gs+="\n";
        }
        Debug.Log(gs);
    }

    private bool can_all_win(Vector2Int wall_pos, bool is_rotated)
    {
        //test for each player if he can go to the other side of the map and win. If he cannot, is is blocked and the wall cannot be placed here

        int player_can_go = 0; // number of player who can go to the other side

        for(int i=0; i<MainManager.Instance.number_of_player;i++)
        {
            string player_name = "/Player" + (i+1).ToString();
            GameObject player = GameObject.Find(player_name);
            Vector2Int player_pos = player.GetComponent<Player_mouvement>().player_pos;

            List<Vector2Int> lookingCase = new List<Vector2Int>();
            lookingCase.Add(player_pos);
            int[,] search_grid;
            search_grid = new int[17,17];

            //copy the grid to not midiify it
            for (int x=0;x<17;x++)
            {
                for (int y=0;y<17;y++)
                {
                    search_grid[x, y] = grid[x,y];
                }
            }

            // Set all player posible position to a max of 100
            for(int ii=0;ii<9;ii++)
            {
                for(int j=0;j<9;j++)
                {
                    search_grid[ii*2, j*2] = 100;
                }
            }

            //add the wall to test
            if (is_rotated == false)
            {
                search_grid[wall_pos[0], wall_pos[1]-1] = wall_num;
                search_grid[wall_pos[0], wall_pos[1]]   = wall_num;
                search_grid[wall_pos[0], wall_pos[1]+1] = wall_num;
            }
            else
            {
                search_grid[wall_pos[0]-1, wall_pos[1]] = wall_num;
                search_grid[wall_pos[0],   wall_pos[1]] = wall_num;
                search_grid[wall_pos[0]+1, wall_pos[1]] = wall_num;
            }
            int cost = 0; // number of turn to get to this point

            search_grid[player_pos.x, player_pos.y] = cost;


            while (lookingCase.Count > 0) // whil there are new tiles to search
            {
                cost++;
                List<Vector2Int> new_lookingCase = new List<Vector2Int>();

                for(int ii=0;ii<lookingCase.Count;ii++) // for each new tile
                {
                    Vector2Int case_pos = lookingCase[ii];
                    if(case_pos.x>0) // check if we search to the left
                    {
                        if (search_grid[case_pos.x-1, case_pos.y] == 0) // if there is no wall blocking the left
                        {
                            if (search_grid[case_pos.x-2, case_pos.y] > cost) // and the cost is less than the current cost
                            {
                                new_lookingCase.Add(new Vector2Int(case_pos.x-2, case_pos.y)); // add the left tile to the new list of new tiles
                                search_grid[case_pos.x-2, case_pos.y] = cost; // set the cost of the left tile to the current cost
                            } 
                        }
                    }
                    if(case_pos.x<16) // right
                    {
                        if (search_grid[case_pos.x+1, case_pos.y] == 0)
                        {
                            if (search_grid[case_pos.x+2, case_pos.y] > cost)
                            {
                                new_lookingCase.Add(new Vector2Int(case_pos.x+2, case_pos.y));
                                search_grid[case_pos.x+2, case_pos.y] = cost;
                            }
                        }
                    }
                    if(case_pos.y>0) // up
                    {
                        if (search_grid[case_pos.x, case_pos.y-1] == 0)
                        {
                            if (search_grid[case_pos.x, case_pos.y-2] > cost)
                            {
                                new_lookingCase.Add(new Vector2Int(case_pos.x, case_pos.y-2));
                                search_grid[case_pos.x, case_pos.y-2] = cost;
                            }
                        }
                    }
                    if(case_pos.y<16) // down
                    {
                        if (search_grid[case_pos.x, case_pos.y+1] == 0)
                        {
                            if (search_grid[case_pos.x, case_pos.y+2] > cost)
                            {
                                new_lookingCase.Add(new Vector2Int(case_pos.x, case_pos.y+2));
                                search_grid[case_pos.x, case_pos.y+2] = cost;
                            }
                        }
                    }
                }
                lookingCase = new_lookingCase; // put into the list the new tiles to search
                
            }

            for (int l=0;l<17;l+=2) // count the number of players can go on a case at the other side of the board
            {
                if (i+1 == 1) //player 1
                {
                    if (search_grid[l, 0] < 100)
                    {
                        player_can_go++;
                        break;
                    }
                }
                if (i+1 == 2) //player 2
                {
                    if (search_grid[l, 16] < 100)
                    {
                        player_can_go++;
                        break;
                    }
                }
                if (i+1 == 3) //player 3
                {
                    if (search_grid[16, l] < 100)
                    {
                        player_can_go++;
                        break;
                    }
                }
                if (i+1 == 4) //player 4
                {
                    if (search_grid[0, l] < 100)
                    {
                        player_can_go++;
                        break;
                    }
                }
            }
        }
        // return true if all players can go on a case at the other side of the board
        if (player_can_go == MainManager.Instance.number_of_player) return true;
        else return false;
    }
}
