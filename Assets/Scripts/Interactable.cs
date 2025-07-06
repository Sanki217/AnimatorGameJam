using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    public string[] dialogueLines;
    private int currentLine = 0;
    private bool playerInRange = false;
    private bool dialogueActive = false;

    public GameObject promptUI;
    public GameObject dialogueUI;
    public TMP_Text dialogueText;

    private PlaneController planeController;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogueActive)
            {
                StartDialogue();
            }
            else
            {
                NextLine();
            }
        }
    }

    void StartDialogue()
    {
        dialogueActive = true;
        promptUI.SetActive(false);
        dialogueUI.SetActive(true);
        currentLine = 0;
        dialogueText.text = dialogueLines[currentLine];

        if (planeController != null)
            planeController.canMove = false;
    }

    void NextLine()
    {
        currentLine++;
        if (currentLine >= dialogueLines.Length)
        {
            dialogueUI.SetActive(false);
            dialogueActive = false;

            if (planeController != null)
                planeController.canMove = true;
        }
        else
        {
            dialogueText.text = dialogueLines[currentLine];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            promptUI.SetActive(true);


            if (planeController == null)
            {
                planeController = other.GetComponent<PlaneController>();
                Debug.Log("Znaleziono PlaneController: " + planeController);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            promptUI.SetActive(false);
        }
    }
}