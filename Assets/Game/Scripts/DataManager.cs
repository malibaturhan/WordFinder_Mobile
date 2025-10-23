using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static string CoinsPlayerPrefKey = "Coins";
    private static string ScorePlayerPrefKey = "Score";
    private static string BestScorePlayerPrefKey = "BestScore";

    public static DataManager Instance;

    [Header("***Data***")]
    [SerializeField] private int coins;
    [SerializeField] private int score;
    [SerializeField] private int bestScore;

    public int Coins { get { return coins; } }
    public int Score { get { return score; } }
    public int BestScore { get { return bestScore; } }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadData();

    }

    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log($"{amount} COINS ADDED");
        SaveData();
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
        coins = Mathf.Max(coins, 0);
        SaveData();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        if(score > bestScore) bestScore = score;
        SaveData();
    }


    private void LoadData()
    {
        coins = PlayerPrefs.GetInt(CoinsPlayerPrefKey, 150);
        bestScore = PlayerPrefs.GetInt(BestScorePlayerPrefKey, 150);
        score = PlayerPrefs.GetInt(ScorePlayerPrefKey, 150);
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt(CoinsPlayerPrefKey, coins);
        PlayerPrefs.SetInt(BestScorePlayerPrefKey, bestScore);
        PlayerPrefs.SetInt(ScorePlayerPrefKey, score);
    }

    [NaughtyAttributes.Button("Reset Score and Coins")]
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt(CoinsPlayerPrefKey, 0);
        PlayerPrefs.SetInt(BestScorePlayerPrefKey, 0);
        PlayerPrefs.SetInt(ScorePlayerPrefKey, 0);
    }
}
