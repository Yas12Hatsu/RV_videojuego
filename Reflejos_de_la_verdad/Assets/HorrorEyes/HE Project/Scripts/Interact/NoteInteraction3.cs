using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NoteInteraction3 : MonoBehaviour
{
    public GameObject notePanel3; // Panel para la segunda nota
    public GameObject paperObject3; // Segundo objeto 3D
    public Text pickUpText; // Texto que indica la opción de recoger
    public Button closeButton3; // Botón para cerrar la segunda nota
    public Text tipText; // Texto de tip para mostrar después de leer la nota
    public NoteCounter noteCounter; // Referencia al contador de notas

    private bool isNearPaper3 = false; // Para verificar si el jugador está cerca del segundo objeto
    private bool isNoteVisible3 = false; // Para verificar si la segunda nota está visible

    void Start()
    {
        // Asegurarse de que las notas y el texto de recoger estén inicialmente ocultos
        if (notePanel3 != null) notePanel3.SetActive(false);
        if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        if (tipText != null) tipText.gameObject.SetActive(false);

        // Añadir listeners al botón de cerrar
        if (closeButton3 != null) closeButton3.onClick.AddListener(HideNote3);
    }

    void Update()
    {
        // Detectar cuando se presiona la tecla "E" para recoger o esconder la nota
        if (isNearPaper3 && Input.GetKeyDown(KeyCode.E) && !isNoteVisible3)
        {
            ShowNote3();
        }
    }

    void ShowNote3()
    {
        if (notePanel3 != null)
        {
            notePanel3.SetActive(true);
            isNoteVisible3 = true;

            // Ocultar el segundo objeto de papel
            if (paperObject3 != null) paperObject3.SetActive(false);

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);

            // Actualizar el contador de notas
            noteCounter.IncrementNoteCount();

            // Desactivar el movimiento del jugador y la cámara
            SendMessage("DisableMovement", SendMessageOptions.DontRequireReceiver);
        }
    }

    void HideNote3()
    {
        if (notePanel3 != null)
        {
            notePanel3.SetActive(false);
            isNoteVisible3 = false;

            // Mostrar el tip de texto
            if (tipText != null)
            {
                tipText.gameObject.SetActive(true);
                tipText.text = "Elías siente un escalofrío. Las palabras despiertan en él una visión fugaz de sí mismo en una camilla, con electrodos pegados a su piel.";
                StartCoroutine(WaitForTip());
            }

            // Eliminar el segundo objeto de papel
            if (paperObject3 != null) Destroy(paperObject3);

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
            isNearPaper3 = true;

            // Mostrar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            isNearPaper3 = false;

            // Ocultar el texto de recoger
            if (pickUpText != null) pickUpText.gameObject.SetActive(false);
        }
    }

}
