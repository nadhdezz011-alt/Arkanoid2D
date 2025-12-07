using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Instancia única accesible desde cualquier script
    public static GameManager Instance;

    public int hearts = 3;

    public GameObject[] bricks;

    private void Awake()
    {
        // Configuración del Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // opcional si quieres que persista entre escenas
    }

    public void LoseHeart()
    {
        hearts--;
        if (hearts <= 0)
        {
            SceneManager.LoadScene(1);
            Debug.Log("GameOver");
            hearts = 3; // Reiniciar corazones para la próxima partida
        }
        else
        {
            ResetLevel1();
            Debug.Log("Hearts left: " + hearts);
        }
    }

    public void ResetLevel1()
    {
        // Ahora llamamos directamente a los Singletons de Player y Ball
        Player.Instance.ResetPlayer();
        Ball.Instance.ResetBall();
    }
    public void winscreen() 
    {
        if (bricks.Length == (0))
        {
            SceneManager.LoadScene(2);
            Debug.Log("You Win!");
        }
       
    }
}
