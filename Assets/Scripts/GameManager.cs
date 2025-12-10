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
    public GameObject[] powerUpBricks;

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

    [Header("Control de posiciones")]
    public float releaseDelay = 2f;
    private bool[] occupied;
    private float[] releaseTimers;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        occupied = new bool[spawnPositions.Length];
        releaseTimers = new float[spawnPositions.Length];
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

        badBrickTimer += Time.deltaTime;
        if (badBrickTimer >= Random.Range(5f, 10f))
        {
            SpawnBadBrick();
            badBrickTimer = 0f;
        }

        powerUpBrickTimer += Time.deltaTime;
        if (powerUpBrickTimer >= Random.Range(8f, 15f))
        {
            SpawnPowerUpBrick();
            powerUpBrickTimer = 0f;
        }

        HandleReleaseTimers();
    }

    private void HandleReleaseTimers()
    {
        for (int i = 0; i < releaseTimers.Length; i++)
        {
            if (releaseTimers[i] > 0f)
            {
                releaseTimers[i] -= Time.deltaTime;
                if (releaseTimers[i] <= 0f)
                {
                    occupied[i] = false;
                }
            }
        }
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
        RefreshUI();

    }


    public void BrickDestroyed(MonoBehaviour brick)
    {
        if (brick == null) return;

        // Solo actuar si el objeto pertenece a un spawn válido
        int posIndex = FindClosestSpawnIndex(brick.transform.position);
        if (posIndex < 0 || posIndex >= occupied.Length) return;

        occupied[posIndex] = true;
        releaseTimers[posIndex] = releaseDelay;
    }


    public void LoseHeart()
    {
        hearts--;
        heartsText.text = hearts.ToString();

        if (hearts <= 0)
        {
            SceneManager.LoadScene(1);
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
        heartsText.text = hearts.ToString();
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

        SceneManager.LoadScene(3); // tu escena de resultados
    }


    private void SpawnBadBrick()
    {
        if (badBricks.Length == 0 || spawnPositions.Length == 0) return;

        int index = Random.Range(0, badBricks.Length);
        int posIndex = GetFreePositionIndex();

        if (posIndex != -1)
        {
            badBricks[index].transform.position = spawnPositions[posIndex].position;
            badBricks[index].SetActive(true);
            occupied[posIndex] = true;
        }
    }

    private void SpawnPowerUpBrick()
    {
        if (powerUpBricks.Length == 0 || spawnPositions.Length == 0) return;

        int index = Random.Range(0, powerUpBricks.Length);
        int posIndex = GetFreePositionIndex();

        if (posIndex != -1)
        {
            powerUpBricks[index].transform.position = spawnPositions[posIndex].position;
            powerUpBricks[index].SetActive(true);
            occupied[posIndex] = true;
        }
    }

    private int GetFreePositionIndex()
    {
        int[] freeIndices = new int[spawnPositions.Length];
        int count = 0;

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            if (!occupied[i])
            {
                freeIndices[count] = i;
                count++;
            }
        }

        if (count == 0) return -1;
        return freeIndices[Random.Range(0, count)];
    }

    private int FindClosestSpawnIndex(Vector3 position)
    {
        int closestIndex = 0;
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
