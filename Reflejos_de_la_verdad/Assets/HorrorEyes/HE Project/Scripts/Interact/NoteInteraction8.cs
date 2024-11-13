using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInteraction8 : MonoBehaviour
{
    public GameObject notePanel8; // Panel para la segunda nota
    public GameObject paperObject8; // Segundo objeto 8D
    public Text pickUpText; // Texto que indica la opci�n de recoger
    public Button closeButton8; // Bot�n para cerrar la segunda nota
    public Text tipText; // Texto de tip para mostrar despu�s de leer la nota
    public NoteCounter noteCounter; // Referencia al contador de notas

    private bool isNearPaper8 = false; // Para verificar si el jugador est� cerca del segundo objeto
    private bool isNoteVisible8 = false; // Para verificar si la segunda nota est� visible

    void Start()
    {
        // Asegurarse de que las notas y el texto de recoger est�n inicialmente ocultos
        if (notePanel8 != null) notePanel8.SetActive(false);
        if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        if (tipText != null) tipText.gameObject.SetActive(false);

        // A�adir listeners al bot�n de cerrar
        if (closeButton8 != null) closeButton8.onClick.AddListener(HideNote8);
    }

    void Update()
    {
        // Detectar cuando se presiona la tecla "E" para recoger o esconder la nota
        if (isNearPaper8 && Input.GetKeyDown(KeyCode.E) && !isNoteVisible8)
        {
            ShowNote8();
        }
    }

    void ShowNote8()
    {
        if (notePanel8 != null)
        {
            notePanel8.SetActive(true);
            isNoteVisible8 = true;

            // Ocultar el segundo objeto de papel
            if (paperObject8 != null) paperObject8.SetActive(false);

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);

            // Actualizar el contador de notas
            noteCounter.IncrementNoteCount();

            // Desactivar el movimiento del jugador y la c�mara
            SendMessage("DisableMovement", SendMessageOptions.DontRequireReceiver);
        }
    }

    void HideNote8()
    {
        if (notePanel8 != null)
        {
            notePanel8.SetActive(false);
            isNoteVisible8 = false;

            // Mostrar el tip de texto
            if (tipText != null)
            {
                tipText.gameObject.SetActive(true);
                tipText.text = "Doctor, doctor, doctor Ramos empieza a escuchar el nombre, todos los recuerdos caen sobre �l como un torrente. Dr. Ramos, el hombre que estuvo detr�s de cada experimento, fue quien lo llev� al l�mite hasta que su cuerpo no pudo m�s.";
                StartCoroutine(WaitForTip());
            }

            // Eliminar el segundo objeto de papel
            if (paperObject8 != null) Destroy(paperObject8);

            // Reactivar el movimiento del jugador y la c�mara
            SendMessage("EnableMovement", SendMessageOptions.DontRequireReceiver);
        }
    }

    IEnumerator WaitForTip()
    {
        yield return new WaitForSeconds(8); // Mostrar el tip por 8 segundos
        if (tipText != null) tipText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            isNearPaper8 = true;

            // Mostrar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            isNearPaper8 = false;

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        }
    }
}
