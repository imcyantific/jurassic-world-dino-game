using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text levelText;
    public Text gameOverText;
    public GameObject gameOverPanel;
    public GameObject leaderboardPanel;
    public Text leaderboardText;
    public InputField nameField;

    private int score = 0;
    private int currentLevel = 1;
    private bool isGameActive = true;
    private ObstacleSpawner spawner;

    private int[] levelGoals = { 0, 500, 1000, 2000, 3000 };
    private List<HighScore> highScores = new List<HighScore>();

    void Start()
    {
        spawner = FindFirstObjectByType<ObstacleSpawner>();
        gameOverPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        InvokeRepeating("IncrementScore", 0.1f, 0.1f);
    }

    // Update is called once per frame
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
        CancelInvoke("IncrementScore");
        spawner.StopSpawning();
        gameOverText.text = "Game Over!\n Score: " + score + "\nLevel: " + currentLevel;
        gameOverPanel.SetActive(true);

        if (IsHighScore(score))
        {
            nameField.gameObject.SetActive(true);
        }
    }

    public void SaveHighScore()
    {
        string playerName = nameField.text;
        if (string.IsNullOrEmpty(playerName)) playerName = "Player";
        highScores.Sort((a, b) => b.score.CompareTo(a.score));

        if (highScores.Count > 10)
        {
            highScores.RemoveRange(10, highScores.Count - 10);
        }
        SaveHighScore();
        nameField.gameObject.SetActive(false);
    }
    
    bool IsHighScore(int score)
    {
        if (highScores.Count < 10) return true;
        return score > highScores[highScores.Count - 1].score;
    }

    public void ShowLeaderboard()
    {
        leaderboardPanel.SetActive(!leaderboardPanel.activeSelf);
        UpdateLeaderBoard();
    }

    void UpdateLeaderBoard()
    {
        leaderboardText.text = "TOP 10 HIGH SCORES\n\n";
        for (int i = 0; i< highScores.Count; i++)
        {
            leaderboardText.text += (i + 1) + ". " + highScores[i].name + " - Score: " + highScores[i].score + " - Level: " + highScores[i].level;
        }
    }

    void SaveHighScores()
    {
        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetString("HighScoreName" + i, highScores[i].name);
            PlayerPrefs.SetInt("HighScoreValue" + i, highScores[i].score);
            PlayerPrefs.SetInt("HighScoreLevel" + i, highScores[i].level);
        }
        PlayerPrefs.SetInt("HighScoreCount", highScores.Count);
        PlayerPrefs.Save();
    }

    void LoadHighScores()
    {
        int count = PlayerPrefs.GetInt("HighScoreCount", 0);
        for (int i = 0; i < count; i++)
        {
            string name = PlayerPrefs.GetString("HighScoreName" + i, "Player");
            int scoreValue = PlayerPrefs.GetInt("HighScoreValue" + i, 0);
            int level = PlayerPrefs.GetInt("HighScoreLevel" + i, 1);
            highScores.Add(new HighScore(name, scoreValue, level));
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

[System.Serializable]
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