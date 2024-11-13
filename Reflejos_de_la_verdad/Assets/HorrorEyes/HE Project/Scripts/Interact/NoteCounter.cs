using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteCounter : MonoBehaviour
{
    public Text countText; // Referencia al texto del contador de notas
    public Text tipText; // El componente Text para mostrar el tip final
    private int noteCount = 0; // Contador de notas leídas
    private int totalNotes = 9; // Total de notas disponibles

    void Start()
    {
        // Inicializar el contador
        if (countText != null)
        {
            countText.text = $"{noteCount}/{totalNotes}";
        }
    }

    public void IncrementNoteCount()
    {
        noteCount++;
        UpdateNoteCount();

        if (noteCount == totalNotes)
        {
            StartCoroutine(ShowFinalTip());
        }
    }

    void UpdateNoteCount()
    {
        if (countText != null)
        {
            countText.text = $"{noteCount}/{totalNotes}";
        }
    }
    public int GetNoteCount()
    {
        return noteCount;
    }

    private IEnumerator ShowFinalTip()
    {
        yield return new WaitForSeconds(11); // Esperar 8 segundos antes de mostrar el tip

        if (tipText != null)
        {
            tipText.gameObject.SetActive(true);
            tipText.text = "Eh encontrado todos los fragmentos!... Aunque ya no puedo vengarme del Dr. Ramos, puedo superar ese trauma.";
            StartCoroutine(HideTipAfterDelay());
        }
    }

    private IEnumerator HideTipAfterDelay()
    {
        yield return new WaitForSeconds(15); // Mostrar el tip por 7 segundos
        if (tipText != null)
        {
            tipText.gameObject.SetActive(false);
        }
    }
}
