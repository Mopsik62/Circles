using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public GameObject[] massive;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    [SerializeField] private AudioClip mergeSound;
    [SerializeField] private int highScore = 0;
    [SerializeField] private int scoreInt = 0;
   // public float growDuration = 20.0f; // ¬рем€, за которое объект будет возрастать до нормального размера



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
        //  GameObject newObject = Instantiate(massive[first.GetComponent<Object>().id + 1], spawnpoint, Quaternion.identity);
       // Vector3 neededScale = newObject.transform.localScale;
      //  StartCoroutine(GrowOverTime(newObject));


        //рассчЄт очков
        Score(++first.GetComponent<Object>().id);
        
    }

   /* IEnumerator GrowOverTime(GameObject obj)
    {
        Vector3 originalScale = obj.transform.localScale;

        float timer = 0.0f;
        while (timer < 0.12f)
        {
            timer += Time.fixedDeltaTime;

            // »нтерпол€ци€ между начальным и конечным масштабом объекта
            float t = Mathf.Clamp01(timer / (0.12f));
            Debug.Log(growDuration);

            Debug.Log(t);
            obj.transform.localScale = Vector3.Lerp(new Vector3(0.0001f, 0.0001f, 0.0001f), originalScale, t);

            yield return null;
        }

        // ”станавливаем нормальный масштаб после увеличени€
        obj.transform.localScale = originalScale;
    }*/

    public void Score(int score)
    {
        scoreInt += score;
        //Debug.Log(scoreInt);
        scoreText.text = "Score: " + scoreInt.ToString();
        if (scoreInt > highScore)
        {
            highScore = scoreInt;
            highScoreText.text = "Highscore: " + highScore.ToString();
            SaveHighScore(highScore);
            //Debug.Log(highScoreInt);
        }
        // Debug.Log(substring);
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

}
