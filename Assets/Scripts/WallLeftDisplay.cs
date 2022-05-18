using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLeftDisplay : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshPro wallLeftText;

    [SerializeField]
    public GameObject player;

    
    
    void Start()
    {
        if (MainManager.Instance.number_of_player == 2) wallLeftText.text = 10.ToString();
        else if (MainManager.Instance.number_of_player == 4) wallLeftText.text = 5.ToString();
    }

    public void DisplayWallLeft()
    {
        if (MainManager.Instance.number_of_player == 2 && MainManager.Instance.autoRotateCam == false)
        {
            // get the player num and the current player
            // int player_num = (GameObject.Find("/Game").GetComponent<Game_stats>().turn%2)+1;       
            // string player_name = "/Player" + player_num.ToString();
            // Debug.Log(player_name);
            // player = GameObject.Find(player_name);
            GameObject player = GameObject.Find("/Game").GetComponent<Game_stats>().get_current_player_object();
        }
        wallLeftText.text = player.GetComponent<Player>().wall_left.ToString();
    }
}
