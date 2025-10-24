using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [Header("***Elements***")]
    [SerializeField] private WordContainer[] wordContainers;
    [SerializeField] private Button tryButton;
    [SerializeField] private KeyboardColorizer keyboardColorizer;

    [Header("***Settings***")]
    [SerializeField] private int currentWordContainerIndex;
    private bool canAddLetter = true;
    [SerializeField] private int scoreToCoinMultiplier = 3;

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

    void Start()
    {
        Initialize();
        KeyboardKey.onKeyPressed += KeyPressedCallback;
        GameManager.OnGameStateChanged += GameStateChangedCallback;

    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.LevelComplete:

                break;
            case GameState.Game:
                Initialize();
                break;


        }
    }


    private void KeyPressedCallback(char letter)
    {
        if (!canAddLetter) return;

        wordContainers[currentWordContainerIndex].Add(letter);

        if (wordContainers[currentWordContainerIndex].IsComplete)
        {
            canAddLetter = false;
            EnableTryButton();
        }
    }

    public void CheckWord()
    {
        string wordToCheck = wordContainers[currentWordContainerIndex].GetWord();
        string secretWord = WordManager.Instance.SecretWord;

        wordContainers[currentWordContainerIndex].Colorize(secretWord);
        keyboardColorizer.Colorize(secretWord, wordToCheck);

        if (wordToCheck == secretWord)
        {
            SetLevelComplete();
        }
        else // WRONG WORD
        {
            currentWordContainerIndex++;
            DisableTryButton();
            Debug.LogWarning("current word container index" + currentWordContainerIndex);
            Debug.LogWarning("current word container length" + wordContainers.Length);
            if (currentWordContainerIndex >= wordContainers.Length)
            {
                Debug.Log("GAME OVER");
                DataManager.Instance.ResetScore();
                GameManager.Instance.SetGameState(GameState.GameOver);
            }
            else
            {
                canAddLetter = true;


            }
        }
    }

    private void SetLevelComplete()
    {
        UpdateData();
        GameManager.Instance.SetGameState(GameState.LevelComplete);
    }

    private void UpdateData()
    {
        int scoreToAdd = wordContainers.Length - currentWordContainerIndex;
        DataManager.Instance.IncreaseScore(scoreToAdd);
        DataManager.Instance.AddCoins(scoreToAdd * scoreToCoinMultiplier);
    }

    public void BackspacePressedCallback()
    {
        if (!GameManager.Instance.IsInGameState) return;

        bool removedLetter = wordContainers[currentWordContainerIndex].RemoveLetter();
        if (removedLetter)
        {
            DisableTryButton();
        }
        canAddLetter = true;
    }

    private void EnableTryButton()
    {
        tryButton.interactable = true;
    }

    private void DisableTryButton()
    {
        tryButton.interactable = false;
    }

    private void Initialize()
    {
        currentWordContainerIndex = 0;
        canAddLetter = true;

        DisableTryButton();

        for (int i = 0; i < wordContainers.Length; i++)
        {
            wordContainers[i].Initialize();
        }
    }

    private void OnDisable()
    {
        KeyboardKey.onKeyPressed -= KeyPressedCallback;
        GameManager.OnGameStateChanged -= GameStateChangedCallback;
    }

    public WordContainer CurrentWordContainer => wordContainers[currentWordContainerIndex];
    
}
