using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI gameOverText;
    public GameObject gameOverPanel;
    public GameObject leaderboardPanel;
    public TextMeshProUGUI leaderboardText;
    public TMP_InputField nameField;
    public UnityEngine.UI.Button saveScoreButton;

    private int score = 0;
    private int currentLevel = 1;
    private bool isGameActive = true;
    private ObstacleSpawner spawner;
    private DinoController dinoSAUR;

    private int[] levelGoals = { 0, 500, 1000, 2000, 3000 };
    private static List<HighScore> highScores = new List<HighScore>();

    void Start()
    {
        spawner = FindFirstObjectByType<ObstacleSpawner>();
        gameOverPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        dinoSAUR = GameObject.Find("Dino").GetComponent<DinoController>();
        InvokeRepeating("IncrementScore", 0.1f, 0.1f);
    }

    void Update()
    {
        if (isGameActive)
        {
            CheckLevelProgression();
        }
    }

    void IncrementScore()
    {
        if (isGameActive)
        {
            score += 1;
            scoreText.text = "Score: " + score;
            levelText.text = "Level: " + currentLevel;
        }
    }

    void CheckLevelProgression()
    {
        if (currentLevel < levelGoals.Length)
        {
            if (score >= levelGoals[currentLevel])
            {
                LevelUp();
            }
        }
    }

    void LevelUp()
    {
        currentLevel++;
        spawner.IncreaseSpeed();
    }

    public void GameOver()
    {
        isGameActive = false;

        dinoSAUR.Die();

        CancelInvoke("IncrementScore");
        spawner.StopSpawning();
        gameOverText.text = "Score: " + score + "\nLevel: " + currentLevel;
        gameOverPanel.SetActive(true);
        nameField.gameObject.SetActive(true);
    }

    public void SaveHighScore()
    {
        saveScoreButton.enabled = false;
        string playerName = nameField.text;
        if (string.IsNullOrEmpty(playerName)) playerName = "Player";
        highScores.Add(new HighScore (playerName, currentLevel, score));
        highScores.Sort((a, b) => b.score.CompareTo(a.score));

        if (highScores.Count > 10)
        {
            highScores.RemoveRange(10, highScores.Count - 10);
        }

        UpdateLeaderBoard();
        nameField.text = "";
        nameField.gameObject.SetActive(false);
    }

    public void ShowLeaderboard()
    {
        gameOverPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
        UpdateLeaderBoard();
    }

    void UpdateLeaderBoard()
    {
        string leaderboardDisplay = "";
        for (int i = 0; i < highScores.Count; i++)
        {
            leaderboardDisplay += (i + 1) + ". " + highScores[i].name +
                                  "                   Score: " + highScores[i].score +
                                  "  Level: " + highScores[i].level + "\n";
        }
        leaderboardText.text = leaderboardDisplay;
    }

    public void CloseGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
public class HighScore
{
    public string name;
    public int level;
    public int score;

    public HighScore(string name, int level, int score)
    {
        this.name = name;
        this.level = level;
        this.score = score;
    }
}