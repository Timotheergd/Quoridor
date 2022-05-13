using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableMove : MonoBehaviour
{
    /* 
        Script for the indicator to where the player can jump
        These indicator are clickable
    */

    RaycastHit hitInfo;
    Ray ray;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject objectHit = hitInfo.transform.gameObject;

            if (objectHit.name == gameObject.name) // if the indicator is clicked
            {
                // get the current player num to get the current player
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

                // get the player position in the grid
                Vector2Int player_grid_pos = GameObject.Find("/Board").GetComponent<Game_grid>().Worldpos_To_Gridpos(player.transform.position.x, player.transform.position.z);

                //move player to the selected indicator
                GameObject.Find("/Board").GetComponent<Game_grid>().grid[player_grid_pos.x, player_grid_pos.y] = 0; // reset the old position
                player.transform.position = new Vector3(transform.position.x, 2f,transform.position.z); // move the player
                player_grid_pos = GameObject.Find("/Board").GetComponent<Game_grid>().Worldpos_To_Gridpos(transform.position.x, transform.position.z); // get the newposition of the player in the grid
                
                GameObject.Find("/Board").GetComponent<Game_grid>().grid[player_grid_pos.x, player_grid_pos.y] = player_num; // set the new position of the player in the grid
                player.GetComponent<Player_mouvement>().player_pos = new Vector2Int(player_grid_pos.x, player_grid_pos.y); // // set the new position of the player in the grid in the stats of the player

                GameObject.Find("/Game").GetComponent<Game_stats>().jump_choosing = false;
                GameObject.Find("/Game").GetComponent<Game_stats>().end_turn();

                // remove the inicators
                GameObject[] playableMoves;
                playableMoves = GameObject.FindGameObjectsWithTag("playableMove");
                foreach (GameObject playableMove in playableMoves)
                {
                    Destroy(playableMove);
                }
                
            }
        }
    }
}
