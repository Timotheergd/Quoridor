using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject game;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void turnCam()
    {
        /*
            Turn the camera for the current player
        */
        if (GameObject.Find("/Game").GetComponent<Game_stats>().jump_choosing == false)
        {
            int player_turn = (game.GetComponent<Game_stats>().turn%MainManager.Instance.number_of_player);
            int angle = player_turn * (360/MainManager.Instance.number_of_player); 
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}
