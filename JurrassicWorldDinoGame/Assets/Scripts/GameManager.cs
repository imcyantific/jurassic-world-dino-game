using System.Collections.Generic;
using UnityEngine;
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

    private List<HighScore> highScores = new List<HighScore>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {

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
}
