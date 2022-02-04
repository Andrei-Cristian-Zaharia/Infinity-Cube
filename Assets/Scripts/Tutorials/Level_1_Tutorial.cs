using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1_Tutorial : MonoBehaviour
{
    public DialogueManager dialogueManager;
    private GameManager gameManager;

    private GameObject player;
    public GameObject joyStick;
    public GameObject[] ilustrations;
    public int currentTask = 0;

    private bool end = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        joyStick = GameObject.FindGameObjectWithTag("Joystick");
        player.GetComponent<PlayerController>().joystick = joyStick.GetComponent<Joystick>();
        joyStick.SetActive(false);
        player.GetComponent<PlayerController>().canJump = false;
        dialogueManager.allowToShowNext = true;
        gameManager.PauseGame();
    }

    private void Update()
    {
        if (dialogueManager.currentSentence == 2 || dialogueManager.currentSentence == 3)
        {
            dialogueManager.allowToShowNext = false;
            joyStick.SetActive(true);
            gameManager.ResumeGame();
        }

        if (currentTask == 0 && dialogueManager.currentSentence == 2)
        {
            player.GetComponent<PlayerController>().jump = false;

          if (player.GetComponent<PlayerController>().moveInput != 0)
            {
                currentTask++;
                dialogueManager.canShowNext = true;
                dialogueManager.allowToShowNext = true;
                gameManager.PauseGame();
                dialogueManager.ShowNext();
            }
        }

        if (currentTask == 1)
        {
            if (!player.GetComponent<PlayerController>().canJump)
                player.GetComponent<PlayerController>().canJump = true;

            if (player.GetComponent<PlayerController>().jump == true)
            {
                currentTask++;
                dialogueManager.canShowNext = true;
                dialogueManager.allowToShowNext = true;
                dialogueManager.ShowNext();
            }
        }

        if (currentTask == 2 && !end)
        {
            StartCoroutine(EndDialogue());
        }
    }

    IEnumerator EndDialogue()
    {
        yield return new WaitForSeconds(.2f);

        end = true;
        joyStick.SetActive(true);
        player.GetComponent<PlayerController>().canJump = true;
        gameManager.ResumeGame();
        dialogueManager.EndDialogue();
    }
}
