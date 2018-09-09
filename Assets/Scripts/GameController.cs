using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards; //вражеский объект
    public int hazardCount; //количество созданных врагов
    public float spawnWait; //временная задержка между появлением врагов
    public float startWait; //временная задержка перед началом игры
    public float waveWait; //временная задержка между волнами

    private float randDistance; //дистанция появления врагов
    private float randDirection; //радиус появления врагов

    public GUIText scoreText; //переменная для ссылания GUI-текста игрового счета
    public GUIText restartText; //переменная для ссылания GUI-текста рестарта игры
    public GUIText gameOverText; //переменная для ссылания GUI-текста об окончании игры

    //отслеживают когда игра закончилась, а когда продолжается
    private bool gameOver;
    private bool restart;

    private int score;

    GameObject hazard;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();

        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    //создание волн
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            //создание врагов вокруг вокруг игрока на определенном расстоянии
            for (int i = 0; i < hazardCount; i++)
            {
                //задаем количество "умных" врагов взависимости от количества набранных очков
                if(score < 200)
                    hazard = hazards[Random.Range(0, hazards.Length - 4)];
                else if (score < 500)
                    hazard = hazards[Random.Range(0, hazards.Length - 3)];
                else if (score < 1000)
                    hazard = hazards[Random.Range(0, hazards.Length - 2)];
                else if (score < 1500)
                    hazard = hazards[Random.Range(0, hazards.Length - 1)];
                else
                    hazard = hazards[Random.Range(0, hazards.Length)];

                // Задаём случайные переменные для расстояния и направления
                randDistance = Random.Range(35, 35); //дистанция появления врагов
                randDirection = Random.Range(0, 360); //радиус появления врагов
                // Используем переменные для задания координат появления врага
                float posX = this.transform.position.x + (Mathf.Cos((randDirection) * Mathf.Deg2Rad) * randDistance);
                float posZ = this.transform.position.z + (Mathf.Sin((randDirection) * Mathf.Deg2Rad) * randDistance);
                // Создаём врага на заданных координатах
                Instantiate(hazard, new Vector3(posX, 0, posZ), this.transform.rotation);
                yield return new WaitForSeconds(spawnWait);

                if (gameOver)
                {
                    yield return new WaitForSeconds(1);
                    restartText.text = "Press 'R' for restart";
                    restart = true;
                }
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                break;
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOverText.text = "Game over!";
        gameOver = true;
    }
}