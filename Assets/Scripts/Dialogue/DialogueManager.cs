using Ludiq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    public Dialogue dialogue;

    public Text nameText;
    public Text dialogueText;

    public bool canShowNext;

    Animator anim;

    private GameManager GM;
    private GameObject panelBox;

    public int currentSentence = 0;
    public bool allowToShowNext = true;

    private void Start()
    {
        sentences = new Queue<string>();
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        canShowNext = false;

        StartDialogue(dialogue);
    }

    public void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && canShowNext && allowToShowNext)
            {
                canShowNext = false;

                currentSentence++;

                DisplayNextSentence();
            }
        }

        if (Input.GetMouseButtonDown(1) && canShowNext && allowToShowNext)
        {
            canShowNext = false;

            currentSentence++;

            DisplayNextSentence();
        }
    }

    public void ShowNext()
    {
        canShowNext = false;

        currentSentence++;

        DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log(dialogue.name);
        dialogue.gameObject.SetActive(true);
        panelBox = dialogue.gameObject;

        anim = dialogue.GetComponent<Animator>();
        anim.SetBool("IsOpen", true);

        GM.PauseGame();
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) { sentences.Enqueue(sentence); }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(0.02f);
        }

        yield return new WaitForSecondsRealtime(1f);

        canShowNext = true;
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation.");

        anim.SetBool("IsOpen", false);

        panelBox.SetActive(false);
        GM.ResumeGame();
    }
}
