using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public GameObject promptImage;           // Obrazek z literk¹ "E"
    public GameObject dialogueUI;            // UI z Image (na którym wyœwietlamy obrazy z tekstem)
    public Image dialogueImageRenderer;      // Komponent Image do wyœwietlania grafiki dialogu
    public Sprite[] dialogueSprites;         // Grafiki z tekstem (jpg/png jako sprite’y)

    private int currentImage = 0;
    private bool playerInRange = false;
    private bool dialogueActive = false;

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
                ShowNextImage();
            }
        }
    }

    void StartDialogue()
    {
        if (dialogueSprites == null || dialogueSprites.Length == 0)
        {
            Debug.LogWarning("Brak przypisanych grafik dialogowych!");
            return;
        }

        dialogueActive = true;
        currentImage = 0;
        promptImage.SetActive(false);
        dialogueUI.SetActive(true);
        dialogueImageRenderer.sprite = dialogueSprites[currentImage];

        if (planeController != null)
            planeController.canMove = false;
    }

    void ShowNextImage()
    {
        currentImage++;
        if (currentImage >= dialogueSprites.Length)
        {
            EndDialogue();
        }
        else
        {
            dialogueImageRenderer.sprite = dialogueSprites[currentImage];
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        dialogueActive = false;

        if (planeController != null)
            planeController.canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            promptImage.SetActive(true);

            if (planeController == null)
            {
                planeController = other.GetComponent<PlaneController>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            promptImage.SetActive(false);
        }
    }
}
