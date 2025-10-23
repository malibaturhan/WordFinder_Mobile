using System;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    [Header("***Elements***")]
    [SerializeField] private WordContainer[] wordContainers;
    [SerializeField] private Button tryButton;
    [SerializeField] private KeyboardColorizer keyboardColorizer;

    [Header("***Settings***")]
    [SerializeField] private int currentWordContainerIndex;
    private bool canAddLetter = true;
    [SerializeField] private int scoreToCoinMultiplier = 3;

    void Start()
    {
        Initialize();
        KeyboardKey.onKeyPressed += KeyPressedCallback;
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

        if(wordToCheck == secretWord)
        {
            SetLevelComplete();
        }
        else
        {
            Debug.Log("GOING TO OTHER LINE");
            canAddLetter = true;
            DisableTryButton();
            currentWordContainerIndex++;
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
        for (int i = 0; i < wordContainers.Length; i++) 
        {
            wordContainers[i].Initialize();
        }
    }

    private void OnDisable()
    {
        KeyboardKey.onKeyPressed -= KeyPressedCallback;
    }
}
