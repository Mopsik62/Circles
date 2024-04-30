using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] massive;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public float TimeTillGameOver = 2.0f;


    [SerializeField] private int highScore = 0;
    [SerializeField] private int scoreInt = 0;

    [SerializeField] private Image gameOverPanel;
    [SerializeField] private Image pauseMenu;

    [SerializeField] private float fadeTime = 1.5f;



    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LoadHighScore();
        highScoreText.text = "Highscore: " + highScore.ToString();
        scoreText.text = "Score: " + scoreInt.ToString();

    }

    void Update()
    {

    }
    public void replaceObjects(GameObject first, GameObject second, Vector2 spawnpoint)
    {
        AudioManager.instance.playSound(AudioManager.instance.mergeSound);
        Destroy(first);
        Destroy(second);
        Instantiate(massive[first.GetComponent<Object>().id + 1], spawnpoint, Quaternion.identity);

        //рассчёт очков
        Score(++first.GetComponent<Object>().id);
        
    }

    public void Score(int score)
    {
        scoreInt += score;
        scoreText.text = "Score: " + scoreInt.ToString();
        if (scoreInt > highScore)
        {
            highScore = scoreInt;
            highScoreText.text = "Highscore: " + highScore.ToString();
            SaveHighScore(highScore);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        StartCoroutine(ResetGame());


    }
    //Pause
    public void Pause()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    //Save

    public void SaveHighScore(int highScore)
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }
    
    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    IEnumerator ResetGame()
    {
        Color startColor = gameOverPanel.color;
        startColor.a = 0f;
        float elapsedTime = 0f;
        gameOverPanel.gameObject.SetActive(true);
        
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;

            float newAlpha = Mathf.Lerp(0f, 1f, (elapsedTime / fadeTime));
            startColor.a = newAlpha;
            gameOverPanel.color = startColor;

            yield return null;
        }
        //yield return new WaitForSeconds(2.5f);
        //
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
