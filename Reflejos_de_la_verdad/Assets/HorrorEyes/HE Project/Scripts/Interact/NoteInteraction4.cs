using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInteraction4 : MonoBehaviour
{
    public GameObject notePanel4; // Panel para la segunda nota
    public GameObject paperObject4; // Segundo objeto 4D
    public Text pickUpText; // Texto que indica la opción de recoger
    public Button closeButton4; // Botón para cerrar la segunda nota
    public Text tipText; // Texto de tip para mostrar después de leer la nota
    public NoteCounter noteCounter; // Referencia al contador de notas

    private bool isNearPaper4 = false; // Para verificar si el jugador está cerca del segundo objeto
    private bool isNoteVisible4 = false; // Para verificar si la segunda nota está visible

    void Start()
    {
        // Asegurarse de que las notas y el texto de recoger estén inicialmente ocultos
        if (notePanel4 != null) notePanel4.SetActive(false);
        if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        if (tipText != null) tipText.gameObject.SetActive(false);

        // Añadir listeners al botón de cerrar
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

            // Desactivar el movimiento del jugador y la cámara
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
                tipText.text = "Al leer esto, Elías siente en su mente el eco de un dolor agudo. Recuerda vagamente la presión de correas y una voz que repetía: “Aguanta, es por tu bien.”";
                StartCoroutine(WaitForTip());
            }

            // Eliminar el segundo objeto de papel
            if (paperObject4 != null) Destroy(paperObject4);

            // Reactivar el movimiento del jugador y la cámara
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
