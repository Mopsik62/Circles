using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public GameObject[] massive;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public float TimeTillGameOver = 2.0f;
    [SerializeField] private AudioClip mergeSound;
    [SerializeField] private int highScore = 0;
    [SerializeField] private int scoreInt = 0;
   // public float growDuration = 20.0f; // Время, за которое объект будет возрастать до нормального размера



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

    // Update is called once per frame
    void Update()
    {

    }
    public void replaceObjects(GameObject first, GameObject second, Vector2 spawnpoint)
    {
        AudioManager.instance.playSound(mergeSound);
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
        //какое нибудь gameOverPanel.gameobject.SetActive(True);
        yield return new WaitForSeconds(2.5f);
        //
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }

}
