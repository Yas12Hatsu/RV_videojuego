using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInteraction5 : MonoBehaviour
{
    public GameObject notePanel5; // Panel para la segunda nota
    public GameObject paperObject5; // Segundo objeto 5D
    public Text pickUpText; // Texto que indica la opción de recoger
    public Button closeButton5; // Botón para cerrar la segunda nota
    public Text tipText; // Texto de tip para mostrar después de leer la nota
    public NoteCounter noteCounter; // Referencia al contador de notas

    private bool isNearPaper5 = false; // Para verificar si el jugador está cerca del segundo objeto
    private bool isNoteVisible5 = false; // Para verificar si la segunda nota está visible

    void Start()
    {
        // Asegurarse de que las notas y el texto de recoger estén inicialmente ocultos
        if (notePanel5 != null) notePanel5.SetActive(false);
        if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        if (tipText != null) tipText.gameObject.SetActive(false);

        // Añadir listeners al botón de cerrar
        if (closeButton5 != null) closeButton5.onClick.AddListener(HideNote5);
    }

    void Update()
    {
        // Detectar cuando se presiona la tecla "E" para recoger o esconder la nota
        if (isNearPaper5 && Input.GetKeyDown(KeyCode.E) && !isNoteVisible5)
        {
            ShowNote5();
        }
    }

    void ShowNote5()
    {
        if (notePanel5 != null)
        {
            notePanel5.SetActive(true);
            isNoteVisible5 = true;

            // Ocultar el segundo objeto de papel
            if (paperObject5 != null) paperObject5.SetActive(false);

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);

            // Actualizar el contador de notas
            noteCounter.IncrementNoteCount();

            // Desactivar el movimiento del jugador y la cámara
            SendMessage("DisableMovement", SendMessageOptions.DontRequireReceiver);
        }
    }

    void HideNote5()
    {
        if (notePanel5 != null)
        {
            notePanel5.SetActive(false);
            isNoteVisible5 = false;

            // Mostrar el tip de texto
            if (tipText != null)
            {
                tipText.gameObject.SetActive(true);
                tipText.text = "Al leerla, Elías tiene una visión momentánea de sí mismo siendo observado, mientras notas y gráficos en un monitor detallan sus respuestas a diferentes estímulos.";
                StartCoroutine(WaitForTip());
            }

            // Eliminar el segundo objeto de papel
            if (paperObject5 != null) Destroy(paperObject5);

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
            isNearPaper5 = true;

            // Mostrar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            isNearPaper5 = false;

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        }
    }
}
