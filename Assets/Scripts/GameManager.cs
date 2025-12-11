using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Juego")]
    public int hearts = 3;
    public int score = 0;

    [Header("Sonidos")]
    public AudioSource audioSource;       // arrastra aquí un AudioSource en el Inspector
    public AudioClip brickDestroyClip;    // sonido al destruir bloque
    public AudioClip ballLaunchClip;      // sonido al lanzar pelota

    [Header("Prefabs de bloques")]
    public GameObject[] rewardBricks;
    public GameObject[] badBricks;
    public GameObject[] powerUpBricks;

    [Header("Posiciones posibles")]
    public Transform[] spawnPositions;

    [Header("UI")]
    public TextMeshProUGUI heartsText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [Header("Timer")]
    public float remainingTime = 60f;

    private bool[] occupied;

    //  Contador global de bloques activos
    public int activeBricks = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;

        }
        Instance = this;

        occupied = new bool[spawnPositions.Length];
    }
    private void Start()
    {
        GenerarBloquesIniciales(); //  aquí se generan los bloques al empezar
        RefreshUI();
    }

    private void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timerText.text = Mathf.CeilToInt(remainingTime).ToString();
        }
        else
        {
            EndGame();
        }
    }
    public void GenerarBloquesIniciales()
    {
        // Barajamos las posiciones para que no siempre sea el mismo orden
        List<Transform> posicionesLibres = new List<Transform>(spawnPositions);

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            if (posicionesLibres.Count == 0) break;

            // Elegir posición aleatoria
            int posIndex = Random.Range(0, posicionesLibres.Count);
            Transform posicionElegida = posicionesLibres[posIndex];
            posicionesLibres.RemoveAt(posIndex);

            // Elegir tipo de bloque aleatorio
            int tipo = Random.Range(0, 3); // 0 = reward, 1 = bad, 2 = power-up
            GameObject[] arrayElegido = rewardBricks;

            if (tipo == 1 && badBricks.Length > 0)
            {
                arrayElegido = badBricks;
            }
            else if (tipo == 2 && powerUpBricks.Length > 0)
            {
                arrayElegido = powerUpBricks;
            }

            // Elegir prefab dentro del array
            int index = Random.Range(0, arrayElegido.Length);
            GameObject prefab = arrayElegido[index];

            // Instanciar bloque en la posición
            GameObject bloque = Instantiate(prefab, posicionElegida.position, Quaternion.identity);

            // Notificar al GameManager que hay un bloque nuevo
            BrickSpawned();

            // Marcar posición como ocupada
            int spawnIndex = FindClosestSpawnIndex(posicionElegida.position);
            if (spawnIndex >= 0 && spawnIndex < occupied.Length)
            {
                occupied[spawnIndex] = true;
            }
        }
    }

    public void PlayBrickDestroySound()
    {
        if (audioSource != null && brickDestroyClip != null)
            audioSource.PlayOneShot(brickDestroyClip);
    }

    public void PlayBallLaunchSound()
    {
        if (audioSource != null && ballLaunchClip != null)
            audioSource.PlayOneShot(ballLaunchClip);
    }

    public void AddScore(int value)
    {
        score += value;
        RefreshUI();
    }

    public void BrickSpawned()
    {
        activeBricks++;
    }

    public void BrickDestroyed(MonoBehaviour brick)
    {
        if (brick == null) return;

        int posIndex = FindClosestSpawnIndex(brick.transform.position);
        if (posIndex >= 0 && posIndex < occupied.Length)
        {
            occupied[posIndex] = false;
        }

        activeBricks--; // restamos al contador
        if (activeBricks <= 0)
        {
            EndGame(); // victoria
        }
    }

    public void LoseHeart()
    {
        hearts--;
        if (hearts <= 0)
        {
            SceneManager.LoadScene(1); // pantalla de game over
            hearts = 3;
        }
        else
        {
            ResetLevel1();
        }
        RefreshUI();
    }

    public void GainHeart()
    {
        hearts++;
        RefreshUI();
    }

    public void ResetLevel1()
    {
        Player.Instance.ResetPlayer();
        Ball.Instance.ResetBall();
    }

    private void EndGame()
    {
        PlayerPrefs.SetInt("FinalScore", score);
        PlayerPrefs.SetInt("FinalHearts", hearts);
        PlayerPrefs.SetInt("FinalTime", Mathf.CeilToInt(remainingTime));
        PlayerPrefs.Save();

        SceneManager.LoadScene(3); // pantalla de win/resultados
    }

    private int FindClosestSpawnIndex(Vector3 position)
    {
        int closestIndex = -1;
        float minDist = float.MaxValue;

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            float dist = Vector3.Distance(position, spawnPositions[i].position);
            if (dist < minDist)
            {
                minDist = dist;
                closestIndex = i;
            }
        }
        return closestIndex;
    }

    private void RefreshUI()
    {
        heartsText.text = hearts.ToString();
        scoreText.text = score.ToString();
        timerText.text = Mathf.CeilToInt(remainingTime).ToString();
    }
}
