using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NoteInteraction7 : MonoBehaviour
{
    public GameObject notePanel7; // Panel para la segunda nota
    public GameObject paperObject7; // Segundo objeto 7D
    public Text pickUpText; // Texto que indica la opci�n de recoger
    public Button closeButton7; // Bot�n para cerrar la segunda nota
    public Text tipText; // Texto de tip para mostrar despu�s de leer la nota
    public NoteCounter noteCounter; // Referencia al contador de notas

    private bool isNearPaper7 = false; // Para verificar si el jugador est� cerca del segundo objeto
    private bool isNoteVisible7 = false; // Para verificar si la segunda nota est� visible

    void Start()
    {
        // Asegurarse de que las notas y el texto de recoger est�n inicialmente ocultos
        if (notePanel7 != null) notePanel7.SetActive(false);
        if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        if (tipText != null) tipText.gameObject.SetActive(false);

        // A�adir listeners al bot�n de cerrar
        if (closeButton7 != null) closeButton7.onClick.AddListener(HideNote7);
    }

    void Update()
    {
        // Detectar cuando se presiona la tecla "E" para recoger o esconder la nota
        if (isNearPaper7 && Input.GetKeyDown(KeyCode.E) && !isNoteVisible7)
        {
            ShowNote7();
        }
    }

    void ShowNote7()
    {
        if (notePanel7 != null)
        {
            notePanel7.SetActive(true);
            isNoteVisible7 = true;

            // Ocultar el segundo objeto de papel
            if (paperObject7 != null) paperObject7.SetActive(false);

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);

            // Actualizar el contador de notas
            noteCounter.IncrementNoteCount();

            // Desactivar el movimiento del jugador y la c�mara
            SendMessage("DisableMovement", SendMessageOptions.DontRequireReceiver);
        }
    }

    void HideNote7()
    {
        if (notePanel7 != null)
        {
            notePanel7.SetActive(false);
            isNoteVisible7 = false;

            // Mostrar el tip de texto
            if (tipText != null)
            {
                tipText.gameObject.SetActive(true);
                tipText.text = "El�as comienza a comprender que era especial para quienes dirig�an esos experimentos, pero no entiende si eso fue una bendici�n o una condena.";
                StartCoroutine(WaitForTip());
            }

            // Eliminar el segundo objeto de papel
            if (paperObject7 != null) Destroy(paperObject7);

            // Reactivar el movimiento del jugador y la c�mara
            SendMessage("EnableMovement", SendMessageOptions.DontRequireReceiver);
        }
    }

    IEnumerator WaitForTip()
    {
        yield return new WaitForSeconds(7); // Mostrar el tip por 7 segundos
        if (tipText != null) tipText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            isNearPaper7 = true;

            // Mostrar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            isNearPaper7 = false;

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        }
    }

}
