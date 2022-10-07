using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelCompleted;

    public GameObject GameOverPanel;
    public GameObject LevelCompletedPanel;

    public static int currentLevelIndex;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public Slider gameProgressSlider;

    public static int numberOfRingsPassed;

    private void Awake()
    {
        //PlayerPrefs.DeleteKey ("CurrentLevelIndex"); 
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }

    void Start()
    {
        gameOver = levelCompleted = false;
        numberOfRingsPassed = 0;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();

        int progress = numberOfRingsPassed * 100/FindObjectOfType<HelixManager>().numberOfRings;
        gameProgressSlider.value = progress;

        if (gameOver)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
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
