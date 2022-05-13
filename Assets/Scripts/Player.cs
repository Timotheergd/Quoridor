using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int wall_left;

    [SerializeField]
    public int player_num;
    
    void Start()
    {
        //change the number of wall if number of player is 2 or 4
        if (MainManager.Instance.number_of_player == 2) wall_left = 10;
        else if (MainManager.Instance.number_of_player == 4) wall_left = 5;
    }

    public void decrement_wall()
    {
        if (as_wall())
        {
            wall_left--; // decrement the number of wall

            // Display the number of wall left
            if (MainManager.Instance.number_of_player == 2 && MainManager.Instance.autoRotateCam == false)
            {
                GameObject.Find("/Player1Cube").GetComponent<WallLeftDisplay>().DisplayWallLeft();    
            }
            else
            {
                GameObject.Find("/Player" + player_num.ToString() + "Cube").GetComponent<WallLeftDisplay>().DisplayWallLeft();
            }
        }
    }

    public bool as_wall() // return if there are wall left
    {
        return wall_left>0;
    }
}
