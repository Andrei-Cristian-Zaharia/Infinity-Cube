using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStatus : MonoBehaviour
{
    public float seconds;

    public bool unlocked = false;
    public bool done = false;
    public int stars = 0;

    public int[] timesForStars = new int[3];

    [SerializeField] private Button button;

    public void InitLevelButton()
    {
        button.GetComponent<Button>().interactable = unlocked;

        // set the image with the number of stars
    }
}
