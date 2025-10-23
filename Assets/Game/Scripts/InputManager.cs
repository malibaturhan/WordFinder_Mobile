using System;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    [Header("***Elements***")]
    [SerializeField] private WordContainer[] wordContainers;
    [SerializeField] private Button tryButton;

    [Header("***Settings***")]
    [SerializeField] private int currentWordContainerIndex;
    private bool canAddLetter = true;

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

        if(wordToCheck == secretWord)
        {
            Debug.Log("LEVEL COMPLETE");
            DisableTryButton();
        }
        else
        {
            Debug.Log("GOING TO OTHER LINE");
            canAddLetter = true;
            DisableTryButton();
            currentWordContainerIndex++;
        }
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
