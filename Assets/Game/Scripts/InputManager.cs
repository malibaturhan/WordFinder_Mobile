using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [Header("***Elements***")]
    [SerializeField] private WordContainer[] wordContainers;

    [Header("***Settings***")]
    [SerializeField] private int currentWordContainerIndex;

    void Start()
    {
        Initialize();
        KeyboardKey.onKeyPressed += KeyPressedCallback;
    }

    private void KeyPressedCallback(char letter)
    {
        wordContainers[currentWordContainerIndex].Add(letter);
        if (wordContainers[currentWordContainerIndex].IsComplete)
        {
            CheckWord();
        }
    }

    private void CheckWord()
    {
        string wordToCheck = wordContainers[currentWordContainerIndex].GetWord();
        string secretWord = WordManager.Instance.SecretWord;

        if(wordToCheck == secretWord)
        {
            Debug.Log("LEVEL COMPLETE");
        }
        else
        {
            Debug.Log("GOING TO OTHER LINE");
            currentWordContainerIndex++;
        }
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
