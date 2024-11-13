using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NoteInteraction1 : MonoBehaviour
{
    public GameObject notePanel1; // Panel para la primera nota
    public GameObject paperObject1; // Primer objeto 3D
    public Text pickUpText; // Texto que indica la opci�n de recoger
    public Button closeButton1; // Bot�n para cerrar la primera nota
    public Text tipText; // Texto de tip para mostrar despu�s de leer la nota
    public NoteCounter noteCounter; // Referencia al contador de notas

    private bool isNearPaper1 = false; // Para verificar si el jugador est� cerca del primer objeto
    private bool isNoteVisible1 = false; // Para verificar si la primera nota est� visible

    void Start()
    {
        // Asegurarse de que las notas y el texto de recoger est�n inicialmente ocultos
        if (notePanel1 != null) notePanel1.SetActive(false);
        if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        if (tipText != null) tipText.gameObject.SetActive(false);

        // A�adir listeners al bot�n de cerrar
        if (closeButton1 != null) closeButton1.onClick.AddListener(HideNote1);
    }

    void Update()
    {
        // Detectar cuando se presiona la tecla "E" para recoger o esconder la nota
        if (isNearPaper1 && Input.GetKeyDown(KeyCode.E) && !isNoteVisible1)
        {
            ShowNote1();
        }
    }

    void ShowNote1()
    {
        if (notePanel1 != null)
        {
            notePanel1.SetActive(true);
            isNoteVisible1 = true;

            // Ocultar el primer objeto de papel
            if (paperObject1 != null) paperObject1.SetActive(false);

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);

            // Actualizar el contador de notas
            if (noteCounter != null)
            {
                noteCounter.IncrementNoteCount();
            }
            // Desactivar el movimiento del jugador y la c�mara
            SendMessage("DisableMovement", SendMessageOptions.DontRequireReceiver);
        }
    }

    void HideNote1()
    {
        if (notePanel1 != null)
        {
            notePanel1.SetActive(false);
            isNoteVisible1 = false;

            // Mostrar el tip de texto
            if (tipText != null)
            {
                tipText.gameObject.SetActive(true);
                tipText.text = "El�as siente un leve mareo al leer la nota y, sin saber por qu�, experimenta un dolor en sus mu�ecas.";
                StartCoroutine(WaitForTip());
            }

            // Eliminar el primer objeto de papel
            if (paperObject1 != null) Destroy(paperObject1);

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
            isNearPaper1 = true;

            // Mostrar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            isNearPaper1 = false;

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        }
    }
}
