using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("***Elements***")]
    [SerializeField] private CanvasGroup gameCG;
    [SerializeField] private CanvasGroup levelCompleteCG;

    [Header("***Level Complete Elements***")]
    [SerializeField] private TextMeshProUGUI levelCompleteCoins;
    [SerializeField] private TextMeshProUGUI levelCompleteSecretWord;
    [SerializeField] private TextMeshProUGUI levelCompleteScore;
    [SerializeField] private TextMeshProUGUI levelCompleteBestScore;

    [Header("*** Game Elements***")]
    [SerializeField] private TextMeshProUGUI gameCoins;
    [SerializeField] private TextMeshProUGUI gameScore;

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
    }
    private void Start()
    {
        GameManager.OnGameStateChanged += GameStateChangedCallback;
        ShowGame();
        HideLevelComplete();
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState) 
        {
            case GameState.LevelComplete:
                ShowLevelComplete();
                HideGame();
                break;


        }
    }

    private void ShowGame()
    {
        gameCoins.text = DataManager.Instance.Coins.ToString();
        gameScore.text = DataManager.Instance.Score.ToString();
        ShowCG(gameCG);
    }
    private void HideGame()
    {
        HideCG(gameCG);
    }
    private void ShowLevelComplete()
    {
        levelCompleteCoins.text = DataManager.Instance.Coins.ToString();
        levelCompleteBestScore.text = DataManager.Instance.BestScore.ToString();
        levelCompleteScore.text = DataManager.Instance.Score.ToString();
        levelCompleteSecretWord.text = WordManager.Instance.SecretWord;
        ShowCG(levelCompleteCG);

    }
    private void HideLevelComplete()
    {
        HideCG(levelCompleteCG);
    }

    private void ShowCG(CanvasGroup cg)
    {
        cg.alpha = 1.0f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    private void HideCG(CanvasGroup cg)
    {
        cg.alpha = 0.0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallback;
    }
}
