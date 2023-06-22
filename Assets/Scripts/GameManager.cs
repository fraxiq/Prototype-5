using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public AudioSource music;
    public Slider sliderMusic;
    private int score;
    private float spawnRate = 1f;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pausedScreen;
    public int lives = 3;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        sliderMusic.value = 0.4f;
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGameActive)
        {
            livesText.text = "Lives: " + lives;
            if (lives <= 0)
            {
                lives = 0;
                GameOver();
            }
        }
        Pause();
        music.volume = sliderMusic.value;
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }

    }
    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& isGameActive)
        {
            pausedScreen.SetActive(true);
            isGameActive = false;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGameActive)
        {
            Time.timeScale = 1;
            pausedScreen.SetActive(false);
            isGameActive = true;
        }

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate = spawnRate / difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.SetActive(false);

    }
}
