using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInteraction9 : MonoBehaviour
{
    public GameObject notePanel9; // Panel para la segunda nota
    public GameObject paperObject9; // Segundo objeto 9D
    public Text pickUpText; // Texto que indica la opción de recoger
    public Button closeButton9; // Botón para cerrar la segunda nota
    public Text tipText; // Texto de tip para mostrar después de leer la nota
    public NoteCounter noteCounter; // Referencia al contador de notas

    private bool isNearPaper9 = false; // Para verificar si el jugador está cerca del segundo objeto
    private bool isNoteVisible9 = false; // Para verificar si la segunda nota está visible

    void Start()
    {
        // Asegurarse de que las notas y el texto de recoger estén inicialmente ocultos
        if (notePanel9 != null) notePanel9.SetActive(false);
        if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        if (tipText != null) tipText.gameObject.SetActive(false);

        // Añadir listeners al botón de cerrar
        if (closeButton9 != null) closeButton9.onClick.AddListener(HideNote9);
    }

    void Update()
    {
        // Detectar cuando se presiona la tecla "E" para recoger o esconder la nota
        if (isNearPaper9 && Input.GetKeyDown(KeyCode.E) && !isNoteVisible9)
        {
            ShowNote9();
        }
    }

    void ShowNote9()
    {
        if (notePanel9 != null)
        {
            notePanel9.SetActive(true);
            isNoteVisible9 = true;

            // Ocultar el segundo objeto de papel
            if (paperObject9 != null) paperObject9.SetActive(false);

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);

            // Actualizar el contador de notas
            noteCounter.IncrementNoteCount();

            // Desactivar el movimiento del jugador y la cámara
            SendMessage("DisableMovement", SendMessageOptions.DontRequireReceiver);
        }
    }

    void HideNote9()
    {
        if (notePanel9 != null)
        {
            notePanel9.SetActive(false);
            isNoteVisible9 = false;

            // Mostrar el tip de texto
            if (tipText != null)
            {
                tipText.gameObject.SetActive(true);
                tipText.text = "Esta nota despierta un recuerdo en Elías de sentirse atrapado, sin escapatoria, mientras sus límites físicos y mentales eran llevados al extremo.";
                StartCoroutine(WaitForTip());
            }

            // Eliminar el segundo objeto de papel
            if (paperObject9 != null) Destroy(paperObject9);

            // Reactivar el movimiento del jugador y la cámara
            SendMessage("EnableMovement", SendMessageOptions.DontRequireReceiver);
        }
    }

    IEnumerator WaitForTip()
    {
        yield return new WaitForSeconds(7); // Mostrar el tip por 9 segundos
        if (tipText != null) tipText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            isNearPaper9 = true;

            // Mostrar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            isNearPaper9 = false;

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        }
    }

}
