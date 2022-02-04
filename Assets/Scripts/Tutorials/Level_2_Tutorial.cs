using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2_Tutorial : MonoBehaviour
{
    public DialogueManager dialogueManager;
    private GameManager gameManager;

    private GameObject player;
    public GameObject joyStick;
    public GameObject[] ilustrations;
    public int currentTask = 0;

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
        if (dialogueManager.currentSentence == 2)
        {
            StartCoroutine(EndDialogue());
        }
    }

    IEnumerator EndDialogue()
    {
        yield return new WaitForSeconds(0.1f);

        joyStick.SetActive(true);
        player.GetComponent<PlayerController>().canJump = true;
        gameManager.ResumeGame();
        dialogueManager.EndDialogue();
    }
}
