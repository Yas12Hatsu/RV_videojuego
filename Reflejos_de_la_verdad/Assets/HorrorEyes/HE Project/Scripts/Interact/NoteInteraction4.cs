using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInteraction4 : MonoBehaviour
{
    public GameObject notePanel4; // Panel para la segunda nota
    public GameObject paperObject4; // Segundo objeto 4D
    public Text pickUpText; // Texto que indica la opci�n de recoger
    public Button closeButton4; // Bot�n para cerrar la segunda nota
    public Text tipText; // Texto de tip para mostrar despu�s de leer la nota
    public NoteCounter noteCounter; // Referencia al contador de notas

    private bool isNearPaper4 = false; // Para verificar si el jugador est� cerca del segundo objeto
    private bool isNoteVisible4 = false; // Para verificar si la segunda nota est� visible

    void Start()
    {
        // Asegurarse de que las notas y el texto de recoger est�n inicialmente ocultos
        if (notePanel4 != null) notePanel4.SetActive(false);
        if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        if (tipText != null) tipText.gameObject.SetActive(false);

        // A�adir listeners al bot�n de cerrar
        if (closeButton4 != null) closeButton4.onClick.AddListener(HideNote4);
    }

    void Update()
    {
        // Detectar cuando se presiona la tecla "E" para recoger o esconder la nota
        if (isNearPaper4 && Input.GetKeyDown(KeyCode.E) && !isNoteVisible4)
        {
            ShowNote4();
        }
    }

    void ShowNote4()
    {
        if (notePanel4 != null)
        {
            notePanel4.SetActive(true);
            isNoteVisible4 = true;

            // Ocultar el segundo objeto de papel
            if (paperObject4 != null) paperObject4.SetActive(false);

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);

            // Actualizar el contador de notas
            noteCounter.IncrementNoteCount();

            // Desactivar el movimiento del jugador y la c�mara
            SendMessage("DisableMovement", SendMessageOptions.DontRequireReceiver);
        }
    }

    void HideNote4()
    {
        if (notePanel4 != null)
        {
            notePanel4.SetActive(false);
            isNoteVisible4 = false;

            // Mostrar el tip de texto
            if (tipText != null)
            {
                tipText.gameObject.SetActive(true);
                tipText.text = "Al leer esto, El�as siente en su mente el eco de un dolor agudo. Recuerda vagamente la presi�n de correas y una voz que repet�a: �Aguanta, es por tu bien.�";
                StartCoroutine(WaitForTip());
            }

            // Eliminar el segundo objeto de papel
            if (paperObject4 != null) Destroy(paperObject4);

            // Reactivar el movimiento del jugador y la c�mara
            SendMessage("EnableMovement", SendMessageOptions.DontRequireReceiver);
        }
    }

    IEnumerator WaitForTip()
    {
        yield return new WaitForSeconds(7); // Mostrar el tip por 5 segundos
        if (tipText != null) tipText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            isNearPaper4 = true;

            // Mostrar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            isNearPaper4 = false;

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        }
    }

}
