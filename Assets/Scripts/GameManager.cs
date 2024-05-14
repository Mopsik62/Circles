using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using TMPro;

public class PlayerInfo
{
    public int HighScore;
    public int Progress;
}
public class GameManager : MonoBehaviour
{   
    public PlayerInfo playerInfo;

    public static GameManager instance;
    public GameObject[] massive;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public float TimeTillGameOver = 2.0f;
    public GameObject[] gameProgressBar;
    public TextMeshProUGUI debug;
    [SerializeField] private bool isPaused = false;

   // [SerializeField] private int highScore = 0;
    [SerializeField] private int scoreInt = 0;

    [SerializeField] private Image gameOverPanel;
    [SerializeField] private Image pauseMenu;

    [SerializeField] private float fadeTime = 1.5f;

    private Color[] initialProgressColors;
   // [SerializeField] private int progress = 1;

    [SerializeField] private ParticleSystem particles;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);

    [DllImport("__Internal")]
    private static extern void Hello(string value);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern void SetToLeadeboard(int value);
    private void Awake()
    {
        instance = this;
        playerInfo = new PlayerInfo();
        initialProgressColors = new Color[gameProgressBar.Length];
    }
    void Start()
    {
        pauseMenu.gameObject.SetActive(false);

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Debug.Log("Unity WEBGL");
            debug.text = debug.text + "/Loading from cloud /";
            Hello("Loading from cloud");
            LoadExtern();

        }
        else
        {
            debug.text = debug.text + "/Loading from PC/";
            //Hello("Loading from PC");
            Load();
        }


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            { Resume(); }
            else { Pause(); } 
        }

    }

    public void replaceObjects(GameObject first, GameObject second, Vector2 spawnpoint)
    {
        AudioManager.instance.playSound(AudioManager.instance.mergeSound);
        Destroy(first);
        Destroy(second);
        if (first.GetComponent<Object>().id != 10)
        {
            GameObject createdObject = massive[first.GetComponent<Object>().id + 1];
            // localeScale.x т.к нужен float, а x=y;
            PlayParticles(spawnpoint, initialProgressColors[first.GetComponent<Object>().id + 1], 20, createdObject.transform.localScale.x);

            Instantiate(createdObject, spawnpoint, Quaternion.identity);

            //прогресс
            if (createdObject.GetComponent<Object>().id > playerInfo.Progress)
            {
                playerInfo.Progress = createdObject.GetComponent<Object>().id;
                SaveSomething("Progress", playerInfo.Progress);
                gameProgressBar[playerInfo.Progress].GetComponent<SpriteRenderer>().color = initialProgressColors[playerInfo.Progress];
                PlayParticles(gameProgressBar[playerInfo.Progress].transform.position, initialProgressColors[playerInfo.Progress], 100, createdObject.transform.localScale.x);
            }
        }
        else
        {
            Debug.Log("YOU WIN");
        }
        //рассчёт очков
        Score(++first.GetComponent<Object>().id);


    }

    public void Score(int score)
    {
        scoreInt += score;
        scoreText.text = "Score: " + scoreInt.ToString();
        if (scoreInt > playerInfo.HighScore)
        {
            playerInfo.HighScore = scoreInt;
            highScoreText.text = "Highscore: " + playerInfo.HighScore.ToString();
            SaveSomething("HighScore", playerInfo.HighScore);
            //SetToLeadeboard(highScore);
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
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.gameObject.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
    //Save
    public void SaveSomething (string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        playerInfo.HighScore = PlayerPrefs.GetInt("HighScore");
        playerInfo.Progress = PlayerPrefs.GetInt("Progress");
        SaveForYandex();

    }
    public void SaveSomething(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    public void SaveSomething(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
    public void SaveForYandex()
    {
        string jsonString = JsonUtility.ToJson(playerInfo);
        SaveExtern(jsonString);
    }

    public void Load()
    {
        string s = "/Load() highScore = " + PlayerPrefs.GetInt("HighScore") + ", progress = " + PlayerPrefs.GetInt("Progress") + "/";
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            Hello(s);
        debug.text = debug.text + s;
        playerInfo.HighScore = PlayerPrefs.GetInt("HighScore");
        playerInfo.Progress = PlayerPrefs.GetInt("Progress");
        SetData();
       

    }
    public void LoadFromYandex(string value)
    {
        playerInfo = JsonUtility.FromJson<PlayerInfo>(value);
        SaveSomething("HighScore", playerInfo.HighScore);
        SaveSomething("Progress", playerInfo.Progress);
        debug.text = debug.text + "/LoadFromYandex after saving HighScore = " + playerInfo.HighScore + " and Progress = " + playerInfo.Progress + "/";
        Load();
    }
    public void SetData()
    {
        highScoreText.text = "Highscore: " + playerInfo.HighScore.ToString();
        scoreText.text = "Score: " + scoreInt.ToString();
        debug.text = debug.text + "/DATA WAS SET/";


        for (int i = 0; i < gameProgressBar.Length; i++)
        {
            Color currentColor = gameProgressBar[i].GetComponent<SpriteRenderer>().color;

            initialProgressColors[i] = gameProgressBar[i].GetComponent<SpriteRenderer>().color;
            if (i > playerInfo.Progress)
            {
                currentColor.r = 0;
                currentColor.g = 0;
                currentColor.b = 0;
                gameProgressBar[i].GetComponent<SpriteRenderer>().color = currentColor;
            }
            //Debug.Log(gameProgressBar[i].GetComponent<SpriteRenderer>().color);

        }
    }

    public void PlayParticles( Vector2 spawnpoint, Color? color = null, short? count = null, float? size = null)
    {

        ParticleSystem newParticles = Instantiate(particles, spawnpoint, Quaternion.identity);
            if (size != null)
        {
            newParticles.startSize = (float)size * 0.5f;
        }
        if (color != null)
        {
            newParticles.startColor = (Color)color;
        }
        
        if (count != null)
        {
            newParticles.emission.SetBurst(0, new ParticleSystem.Burst(0.0f, (short)count));
        }

 

        newParticles.Play();

        Destroy(newParticles.gameObject, 2.5f);

    }

    public void ResetProgress()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.DeleteKey("Progress");
        Resume();
        StartCoroutine(ResetGame());
    }

    public void QuitGame()
    {
       // Debug.Log("Quiting");
        Application.Quit();
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
