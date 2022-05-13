using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int number_of_player;

    public bool autoRotateCam;

    public int volume;

    private void Awake()
    {
        number_of_player = 2;
        autoRotateCam = true;
        volume = 1;

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
