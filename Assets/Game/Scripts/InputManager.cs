using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [Header("***Elements***")]
    [SerializeField] private WordContainer[] wordContainers;

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
        }
    }

    public void CheckWord()
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
            canAddLetter = true;
            currentWordContainerIndex++;
        }
    }

    public void BackspacePressedCallback()
    {
        wordContainers[currentWordContainerIndex].RemoveLetter();
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
