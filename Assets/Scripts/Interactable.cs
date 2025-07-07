using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public GameObject promptImage;           // Obrazek z literk? "E"
    public GameObject dialogueUI;            // UI, który zawiera obiekty dialogowe jako dzieci
    public GameObject[] dialogueObjects;     // GameObjecty z grafik? dialogow? (np. prefabki z obrazem i tekstem)

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
        if (dialogueObjects == null || dialogueObjects.Length == 0)
        {
            Debug.LogWarning("Brak przypisanych obiektów dialogowych!");
            return;
        }

        dialogueActive = true;
        currentImage = 0;
        promptImage.SetActive(false);
        dialogueUI.SetActive(true);

        for (int i = 0; i < dialogueObjects.Length; i++)
            dialogueObjects[i].SetActive(false); // ukryj wszystkie

        dialogueObjects[currentImage].SetActive(true); // poka? pierwszy

        if (planeController != null)
            planeController.canMove = false;
    }

    void ShowNextImage()
    {
        dialogueObjects[currentImage].SetActive(false); // ukryj obecny

        currentImage++;
        if (currentImage >= dialogueObjects.Length)
        {
            EndDialogue();
        }
        else
        {
            dialogueObjects[currentImage].SetActive(true); // poka? nast?pny
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        dialogueActive = false;

        // Ukryj ostatni element, na wypadek, gdyby by? widoczny
        if (currentImage < dialogueObjects.Length)
            dialogueObjects[currentImage].SetActive(false);

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
                planeController = other.GetComponent<PlaneController>();
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
