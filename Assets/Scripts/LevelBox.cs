using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBox : MonoBehaviour
{
    public GameObject[] boxes;

    private Animator anim;
    public int currentIndex;

    private void Start()
    {
        currentIndex = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentIndex + 1 == boxes.Length) return;

            CloseCurrentBox();
            currentIndex++;
            OpenCurrentBox();
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentIndex == 0) return;

            CloseCurrentBox();
            currentIndex--;
            OpenCurrentBox();
        }
    }

    public void CloseCurrentBox()
    {
        anim = boxes[currentIndex].GetComponent<Animator>();
        anim.SetBool("IsOpen", false);
    }

    public void OpenCurrentBox()
    {
        anim = boxes[currentIndex].GetComponent<Animator>();
        anim.SetBool("IsOpen", true);
    }

    public void NextLevelBox()
    {
        if (currentIndex + 1 == boxes.Length) return;

        CloseCurrentBox();
        currentIndex++;
        OpenCurrentBox();
    }

    public void PreviousLevelBox()
    {
        if (currentIndex == 0) return;

        CloseCurrentBox();
        currentIndex--;
        OpenCurrentBox();
    }
}
