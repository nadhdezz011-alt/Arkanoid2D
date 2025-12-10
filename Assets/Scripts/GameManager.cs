using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Juego")]
    public int hearts = 3;
    public int score = 0;

    [Header("Arrays de bloques")]
    public GameObject[] rewardBricks;
    public GameObject[] badBricks;
    public GameObject[] powerUpBricks; // volvemos a usarlo

    [Header("Posiciones posibles")]
    public Transform[] spawnPositions;

    [Header("UI")]
    public TextMeshProUGUI heartsText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [Header("Timer")]
    public float remainingTime = 60f;

    private float badBrickTimer;
    private float powerUpBrickTimer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        // Cuenta atrás
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timerText.text = Mathf.CeilToInt(remainingTime).ToString();
        }
        else
        {
            EndGame();
        }

        // Aparición aleatoria de bricks malos
        badBrickTimer += Time.deltaTime;
        if (badBrickTimer >= Random.Range(5f, 10f))
        {
            SpawnBadBrick();
            badBrickTimer = 0f;
        }

        // Aparición aleatoria de bricks de power-up (solo bloques, no corazones)
        powerUpBrickTimer += Time.deltaTime;
        if (powerUpBrickTimer >= Random.Range(8f, 15f)) // un poco más espaciado
        {
            SpawnPowerUpBrick();
            powerUpBrickTimer = 0f;
        }
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public void BrickDestroyed(MonoBehaviour brick)
    {
        // Aquí podrías comprobar si quedan bricks activos
    }

    public void LoseHeart()
    {
        hearts--;
        heartsText.text = hearts.ToString();

        if (hearts <= 0)
        {
            SceneManager.LoadScene(1); // GameOver
            hearts = 3;
        }
        else
        {
            ResetLevel1();
        }
    }

    public void GainHeart()
    {
        hearts++;
        heartsText.text = hearts.ToString();
    }

    public void ResetLevel1()
    {
        Player.Instance.ResetPlayer();
        Ball.Instance.ResetBall();
    }

    private void EndGame()
    {
        SceneManager.LoadScene(3); // Escena de resultados
    }

    private void SpawnBadBrick()
    {
        if (badBricks.Length == 0 || spawnPositions.Length == 0) return;

        int index = Random.Range(0, badBricks.Length);
        int posIndex = Random.Range(0, spawnPositions.Length);

        badBricks[index].transform.position = spawnPositions[posIndex].position;
        badBricks[index].SetActive(true);
    }

    private void SpawnPowerUpBrick()
    {
        if (powerUpBricks.Length == 0 || spawnPositions.Length == 0) return;

        int index = Random.Range(0, powerUpBricks.Length);
        int posIndex = Random.Range(0, spawnPositions.Length);

        powerUpBricks[index].transform.position = spawnPositions[posIndex].position;
        powerUpBricks[index].SetActive(true);
    }
}
