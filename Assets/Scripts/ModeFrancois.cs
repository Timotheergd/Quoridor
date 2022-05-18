using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeFrancois : MonoBehaviour
{
    public int clic_number;
    [SerializeField]
    public Image image;

    void Start()
    {
        clic_number = 0;
    }

    void Awake()
    {
        if (MainManager.Instance.is_modeFrancois)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        }
        else
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
            
    }

    public void modefrancois()
    {
        clic_number++;

        if (clic_number%10 == 0)
        {
            MainManager.Instance.is_modeFrancois = true;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        }
        else
        {
            MainManager.Instance.is_modeFrancois = false;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
    }
}
