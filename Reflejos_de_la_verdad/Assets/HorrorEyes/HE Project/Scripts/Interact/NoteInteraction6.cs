using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInteraction6 : MonoBehaviour
{
    public GameObject notePanel6; // Panel para la segunda nota
    public GameObject paperObject6; // Segundo objeto 6D
    public Text pickUpText; // Texto que indica la opci�n de recoger
    public Button closeButton6; // Bot�n para cerrar la segunda nota
    public Text tipText; // Texto de tip para mostrar despu�s de leer la nota
    public NoteCounter noteCounter; // Referencia al contador de notas

    private bool isNearPaper6 = false; // Para verificar si el jugador est� cerca del segundo objeto
    private bool isNoteVisible6 = false; // Para verificar si la segunda nota est� visible

    void Start()
    {
        // Asegurarse de que las notas y el texto de recoger est�n inicialmente ocultos
        if (notePanel6 != null) notePanel6.SetActive(false);
        if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        if (tipText != null) tipText.gameObject.SetActive(false);

        // A�adir listeners al bot�n de cerrar
        if (closeButton6 != null) closeButton6.onClick.AddListener(HideNote6);
    }

    void Update()
    {
        // Detectar cuando se presiona la tecla "E" para recoger o esconder la nota
        if (isNearPaper6 && Input.GetKeyDown(KeyCode.E) && !isNoteVisible6)
        {
            ShowNote6();
        }
    }

    void ShowNote6()
    {
        if (notePanel6 != null)
        {
            notePanel6.SetActive(true);
            isNoteVisible6 = true;

            // Ocultar el segundo objeto de papel
            if (paperObject6 != null) paperObject6.SetActive(false);

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);

            // Actualizar el contador de notas
            noteCounter.IncrementNoteCount();

            // Desactivar el movimiento del jugador y la c�mara
            SendMessage("DisableMovement", SendMessageOptions.DontRequireReceiver);
        }
    }

    void HideNote6()
    {
        if (notePanel6 != null)
        {
            notePanel6.SetActive(false);
            isNoteVisible6 = false;

            // Mostrar el tip de texto
            if (tipText != null)
            {
                tipText.gameObject.SetActive(true);
                tipText.text = "El�as siente n�useas. Recuerda vagamente la figura de un hombre con bata blanca, aunque su rostro sigue siendo una sombra en sus recuerdos.";
                StartCoroutine(WaitForTip());
            }

            // Eliminar el segundo objeto de papel
            if (paperObject6 != null) Destroy(paperObject6);

            // Reactivar el movimiento del jugador y la c�mara
            SendMessage("EnableMovement", SendMessageOptions.DontRequireReceiver);
        }
    }

    IEnumerator WaitForTip()
    {
        yield return new WaitForSeconds(7); // Mostrar el tip por 6 segundos
        if (tipText != null) tipText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            isNearPaper6 = true;

            // Mostrar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            isNearPaper6 = false;

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        }
    }
}
