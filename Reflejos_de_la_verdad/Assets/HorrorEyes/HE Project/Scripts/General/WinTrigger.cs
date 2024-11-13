using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinTrigger : MonoBehaviour
{
    public NoteCounter noteCounter; // Referencia al script NoteCounter
    public GameObject winPanel; // Panel negro que se mostrará al ganar
    public Text winText; // Texto que se mostrará en el panel de ganar
    public Text timerText; // Texto para el temporizador
    public GameObject enemy; // Enemigo que aparecerá al morir
    public GameObject loserPanel; // Nuevo panel que se mostrará al morir
    public Text loserText; // Texto que se mostrará en el panel de muerte

    private int totalNotes = 9; // Total de notas necesarias para ganar
    public float animationDuration = 1f; // Duración de la animación de aparición
    private float totalTime = 10f; // Tiempo total en segundos
    private Vector3 originalEnemyScale; // Escala original del enemigo

    private Image winPanelImage; // Referencia al componente Image del winPanel (para manejar la transparencia)
    private float remainingTime;
    private bool gameEnded = false;

    void Start()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false); // Asegurarse de que el panel de ganar esté oculto al inicio
            winPanelImage = winPanel.GetComponent<Image>(); // Obtener el componente Image
            if (winPanelImage != null)
            {
                Color color = winPanelImage.color;
                color.a = 0f; // Hacer que el panel sea completamente transparente al principio
                winPanelImage.color = color;
            }
        }

        if (loserPanel != null)
        {
            loserPanel.SetActive(false); // Asegurarse de que el panel de muerte esté oculto al inicio
        }

        if (enemy != null)
        {
            enemy.SetActive(false); // Asegurarse de que el enemigo esté oculto al inicio
            originalEnemyScale = enemy.transform.localScale; // Guardar la escala original del enemigo
        }

        remainingTime = totalTime; // Inicializar el tiempo restante
    }

    void Update()
    {
        if (!gameEnded)
        {
            // Actualizar el temporizador
            remainingTime -= Time.deltaTime;
            UpdateTimerText();

            if (remainingTime <= 0f)
            {
                remainingTime = 0f;
                GameOver();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró en el trigger es el jugador y si ha recolectado todas las notas
        if (other.CompareTag("Player") && noteCounter != null && noteCounter.GetNoteCount() == totalNotes)
        {
            WinGame();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Cambiar el color del texto del temporizador a rojo fuerte si el tiempo restante es menor o igual a 10 segundos
        if (remainingTime <= 10f)
        {
            timerText.color = Color.red;
        }
        else
        {
            // Restaurar el color original (blanco) si el tiempo restante es mayor a 10 segundos
            timerText.color = Color.white;
        }
    }

    private void WinGame()
    {
        gameEnded = true;
        ShowWinPanel("¡Has recolectado todas las notas!");
    }

    private void GameOver()
    {
        gameEnded = true;
        StartCoroutine(ShowEnemyAndLoserPanel());
    }

    private IEnumerator ShowEnemyAndLoserPanel()
    {
        if (enemy != null)
        {
            PositionEnemyInFrontOfPlayer(); // Posicionar al enemigo frente al jugador
            enemy.SetActive(true); // Hacer visible al enemigo
        }

        yield return new WaitForSeconds(3f); // Esperar 3 segundos

        if (loserPanel != null)
        {
            loserPanel.SetActive(true); // Hacer visible el panel de muerte
            if (loserText != null)
            {
                loserText.text = "Has muerto"; // Mostrar texto "Has muerto"
            }
        }
    }

    private void PositionEnemyInFrontOfPlayer()
    {
        // Obtener la cámara principal
        Camera mainCamera = Camera.main;
        if (mainCamera != null && enemy != null)
        {
            // Calcular la posición frente a la cámara, a nivel del suelo
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 forwardDirection = mainCamera.transform.forward;

            // Posicionar al enemigo frente a la cámara y al nivel del suelo
            Vector3 enemyPosition = new Vector3(
                cameraPosition.x + forwardDirection.x * 1f, // 2 unidades frente a la cámara
                cameraPosition.y, // Mantener el mismo nivel que la cámara
                cameraPosition.z + forwardDirection.z * 2f
            );

            enemy.transform.position = enemyPosition;

            // Ajustar la escala del enemigo para hacerlo más pequeño
            enemy.transform.localScale = originalEnemyScale * 0.3f; // Ajustar el factor de escala según sea necesario

            // Asegurarse de que el enemigo mire hacia la cámara
            enemy.transform.LookAt(new Vector3(mainCamera.transform.position.x, enemy.transform.position.y, mainCamera.transform.position.z));
        }
    }

    private void ShowWinPanel(string message)
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            StartCoroutine(FadeInPanel());

            if (winText != null)
            {
                winText.text = message;
            }
        }
    }

    // Coroutine para la animación de desvanecimiento y escala
    private IEnumerator FadeInPanel()
    {
        float elapsedTime = 0f;

        // Animación de desvanecimiento (Alpha)
        while (elapsedTime < animationDuration)
        {
            // Interpolación de la transparencia (alpha) para el panel
            Color panelColor = winPanelImage.color;
            panelColor.a = Mathf.Lerp(0f, 1f, elapsedTime / animationDuration);
            winPanelImage.color = panelColor;

            // Animación de escala
            winPanel.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsedTime / animationDuration); // De tamaño cero a uno

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que al final el alpha sea 1 y la escala sea normal
        Color finalColor = winPanelImage.color;
        finalColor.a = 1f;
        winPanelImage.color = finalColor;

        winPanel.transform.localScale = Vector3.one; // Asegurarse de que la escala sea la original
    }
}
