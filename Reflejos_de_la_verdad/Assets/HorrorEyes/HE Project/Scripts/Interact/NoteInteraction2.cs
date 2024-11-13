using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInteraction2 : MonoBehaviour
{
    public GameObject notePanel2; // Panel para la segunda nota
    public GameObject paperObject2; // Segundo objeto 3D
    public Text pickUpText; // Texto que indica la opción de recoger
    public Button closeButton2; // Botón para cerrar la segunda nota
    public Text tipText; // Texto de tip para mostrar después de leer la nota
    public NoteCounter noteCounter; // Referencia al contador de notas

    private bool isNearPaper2 = false; // Para verificar si el jugador está cerca del segundo objeto
    private bool isNoteVisible2 = false; // Para verificar si la segunda nota está visible

    void Start()
    {
        // Asegurarse de que las notas y el texto de recoger estén inicialmente ocultos
        if (notePanel2 != null) notePanel2.SetActive(false);
        if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        if (tipText != null) tipText.gameObject.SetActive(false);

        // Añadir listeners al botón de cerrar
        if (closeButton2 != null) closeButton2.onClick.AddListener(HideNote2);
    }

    void Update()
    {
        // Detectar cuando se presiona la tecla "E" para recoger o esconder la nota
        if (isNearPaper2 && Input.GetKeyDown(KeyCode.E) && !isNoteVisible2)
        {
            ShowNote2();
        }
    }

    void ShowNote2()
    {
        if (notePanel2 != null)
        {
            notePanel2.SetActive(true);
            isNoteVisible2 = true;

            // Ocultar el segundo objeto de papel
            if (paperObject2 != null) paperObject2.SetActive(false);

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);

            // Actualizar el contador de notas
            noteCounter.IncrementNoteCount();

            // Desactivar el movimiento del jugador y la cámara
            SendMessage("DisableMovement", SendMessageOptions.DontRequireReceiver);
        }
    }

    void HideNote2()
    {
        if (notePanel2 != null)
        {
            notePanel2.SetActive(false);
            isNoteVisible2 = false;

            // Mostrar el tip de texto
            if (tipText != null)
            {
                tipText.gameObject.SetActive(true);
                tipText.text = "La frase despierta un vago recuerdo de una sala llena de luces y máquinas. Aunque no lo comprende, algo en esa nota le resulta inquietantemente familiar.";
                StartCoroutine(WaitForTip());
            }

            // Eliminar el segundo objeto de papel
            if (paperObject2 != null) Destroy(paperObject2);

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
            isNearPaper2 = true;

            // Mostrar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            isNearPaper2 = false;

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        }
    }
}
