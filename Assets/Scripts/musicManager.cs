using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio")]
    public AudioSource audioSource;   // arrastra aquí tu AudioSource en el Inspector
    public AudioClip musicClip;       // arrastra aquí tu clip de música

    private void Awake()
    {
        // Singleton para que no se destruya entre escenas
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Configuración del AudioSource
        if (audioSource != null && musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.loop = true; //  bucle infinito
            audioSource.Play();
        }
    }
}
