using TMPro;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance;

    [Header("UI")]
    public TextMeshProUGUI messageText; // arrastra aquí tu TextMeshProUGUI desde el Canvas

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Ocultamos el texto al inicio
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Muestra un mensaje en pantalla durante unos segundos
    /// </summary>
    public void ShowMessage(string text, float duration = 2f)
    {
        if (messageText == null) return;

        messageText.text = text;
        messageText.gameObject.SetActive(true);

        // Animación con LeanTween: fade in
        messageText.alpha = 0;
        LeanTween.value(gameObject, 0, 1, 0.3f)
                 .setOnUpdate((float val) => messageText.alpha = val);

        // Ocultar después de "duration"
        LeanTween.delayedCall(gameObject, duration, () =>
        {
            LeanTween.value(gameObject, 1, 0, 0.3f)
                     .setOnUpdate((float val) => messageText.alpha = val)
                     .setOnComplete(() => messageText.gameObject.SetActive(false));
        });
    }
}
//MessageManager.Instance.ShowMessage("¡Bloque destruido!");
//MessageManager.Instance.ShowMessage("Pelota lanzada!", 3f); // con duración personalizada
//MessageManager.Instance.ShowMessage("Has perdido un corazón...");
