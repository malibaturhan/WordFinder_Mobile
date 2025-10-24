using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("***Elements***")]
    [SerializeField] private CanvasGroup MenuCG;
    [SerializeField] private CanvasGroup gameCG;
    [SerializeField] private CanvasGroup levelCompleteCG;
    [SerializeField] private CanvasGroup gameOverCG;

    [Header("***Menu Elements***")]
    [SerializeField] private TextMeshProUGUI menuCoins;
    [SerializeField] private TextMeshProUGUI menuBestScore;

    [Header("***Level Complete Elements***")]
    [SerializeField] private TextMeshProUGUI levelCompleteCoins;
    [SerializeField] private TextMeshProUGUI levelCompleteSecretWord;
    [SerializeField] private TextMeshProUGUI levelCompleteScore;
    [SerializeField] private TextMeshProUGUI levelCompleteBestScore;

    [Header("*** Game Elements***")]
    [SerializeField] private TextMeshProUGUI gameCoins;
    [SerializeField] private TextMeshProUGUI gameScore;

    [Header("***Game Over Elements***")]
    [SerializeField] private TextMeshProUGUI gameOverCoins;
    [SerializeField] private TextMeshProUGUI gameOverSecretWord;
    [SerializeField] private TextMeshProUGUI gameOverBestScore;


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
        HideGame();
        HideLevelComplete();
        HideGameOver();
        ShowMenu();
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState) 
        {
            case GameState.Menu:
                HideGame();
                HideGameOver();
                HideLevelComplete();
                ShowMenu();
                break;
            case GameState.LevelComplete:
                HideMenu();
                HideGame();
                HideGameOver();
                ShowLevelComplete();
                break;
            case GameState.Game:
                HideMenu();
                HideLevelComplete();
                HideGameOver();
                ShowGame();
                break;
            case GameState.GameOver:
                HideMenu();
                HideLevelComplete();
                HideGame();
                ShowGameOver();
                break;


        }
    }

    private void ShowMenu()
    {
        menuCoins.text = DataManager.Instance.Coins.ToString();
        menuBestScore.text = DataManager.Instance.BestScore.ToString();
        ShowCG(MenuCG);
    }
    private void HideMenu()
    {
        HideCG(MenuCG);
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
    private void ShowGameOver()
    {
        gameOverCoins.text = DataManager.Instance.Coins.ToString();
        gameOverBestScore.text = DataManager.Instance.BestScore.ToString();
        gameOverSecretWord.text = WordManager.Instance.SecretWord;
        ShowCG(gameOverCG);

    }
    private void HideGameOver()
    {
        HideCG(gameOverCG);
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
