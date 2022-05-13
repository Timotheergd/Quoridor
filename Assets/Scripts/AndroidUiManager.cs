using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidUiManager : MonoBehaviour
{
    public GameObject game;

    [SerializeField]
    TMPro.TextMeshProUGUI PlaceWallText;

    public GameObject left;
    public GameObject right;
    public GameObject up;
    public GameObject down;
    public GameObject release;
    public GameObject rotate;

    void Start()
    {
        // Display the wall button without the others wall related button
        DisplayWallrelatedButton(false);
    }

    public void LeftButtonPressed()
    {
        game.GetComponent<Game_stats>().is_Left_KeyPressed = true;
    }

    public void RightButtonPressed()
    {
        game.GetComponent<Game_stats>().is_Right_KeyPressed = true;
    }

    public void DownButtonPressed()
    {
        game.GetComponent<Game_stats>().is_Down_KeyPressed = true;
    }

    public void UpButtonPressed()
    {
        game.GetComponent<Game_stats>().is_Up_KeyPressed = true;
    }

    public void HandleWallButtonPressed()
    {
        game.GetComponent<Game_stats>().is_handleWall_KeyPressed = true;
    }

    public void RotateWallButtonPressed()
    {
        game.GetComponent<Game_stats>().is_rotateWall_KeyPressed = true;
    }

    public void ReleaseWallButtonPressed()
    {
        game.GetComponent<Game_stats>().is_releaseWall_KeyPressed = true;
    }

    public void WallLeftButtonPressed()
    {
        game.GetComponent<Game_stats>().is_Wall_Left_KeyPressed = true;
    }

    public void WallRightButtonPressed()
    {
        game.GetComponent<Game_stats>().is_Wall_Right_KeyPressed = true;
    }

    public void WallDownButtonPressed()
    {
        game.GetComponent<Game_stats>().is_Wall_Down_KeyPressed = true;
    }

    public void WallUpButtonPressed()
    {
        game.GetComponent<Game_stats>().is_Wall_Up_KeyPressed = true;
    }

    public void DisplayWallrelatedButton(bool is_placing_wall)
    {
        if (is_placing_wall)
        {
            //Set the text of the button to Cancel
            PlaceWallText.text = "Cancel";
        }
        else
        {
            //Set the text of the button to Wall
            PlaceWallText.text = "Wall";
        }
        // Hide or show the walls related buttons
        left.SetActive(is_placing_wall);
        right.SetActive(is_placing_wall);
        up.SetActive(is_placing_wall);
        down.SetActive(is_placing_wall);
        release.SetActive(is_placing_wall);
        rotate.SetActive(is_placing_wall);
    }
}
