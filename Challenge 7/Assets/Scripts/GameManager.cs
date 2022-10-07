using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelCompleted;
    public static bool isGameStarted;
    public static bool mute = false;

    public GameObject GameOverPanel;
    public GameObject LevelCompletedPanel;
    public GameObject startMenuPanel;
    public GameObject gamePlayPanel;

    public static int currentLevelIndex;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public Slider gameProgressSlider;

    public static int numberOfRingsPassed;
    public static int score = 0;
    public static int highScore;

    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "Best Score\n" + highScore;
    }

    void Start()
    {
        isGameStarted = gameOver = levelCompleted = false;
        numberOfRingsPassed = 0;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();

        int progress = numberOfRingsPassed * 100 / FindObjectOfType<HelixManager>().numberOfRings;
        gameProgressSlider.value = progress;

        scoreText.text = score.ToString();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isGameStarted)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            isGameStarted = true;
            gamePlayPanel.SetActive(true);
            startMenuPanel.SetActive(false);
        }

        if (gameOver)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                if(score > highScore)
                    PlayerPrefs.SetInt("HighScore", score);
                score = 0;
                PlayerPrefs.DeleteKey ("CurrentLevelIndex"); 

                SceneManager.LoadScene("Level");
            }
        }

        if (levelCompleted)
        {
            LevelCompletedPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex + 1);
                SceneManager.LoadScene("Level");
            }
        }
    }
}
