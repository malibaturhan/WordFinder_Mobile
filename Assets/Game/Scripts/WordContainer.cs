using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class WordContainer : MonoBehaviour
{
    [Header("***Elements***")]
    private LetterContainer[] letterContainers;

    [Header("***Settings***")]
    private int currentLetterIndex;
    void Awake()
    {
        letterContainers = GetComponentsInChildren<LetterContainer>();
        //Initialize();
    }

    public void Initialize()
    {
        currentLetterIndex = 0;
        for (int i = 0; i < letterContainers.Length; i++)
        {
            letterContainers[i].Initialize();
        }
    }

    public void Add(char letter)
    {
        letterContainers[currentLetterIndex].SetLetter(letter);
        currentLetterIndex++;
    }

    public bool IsComplete => currentLetterIndex >= letterContainers.Length;


    public bool RemoveLetter()
    {
        if (currentLetterIndex < 1)
        {
            return false;
        }
        currentLetterIndex--;
        letterContainers[currentLetterIndex].Initialize();
        return true;
    }

    public string GetWord()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < letterContainers.Length; i++)
        {
            sb.Append(letterContainers[i].Letter);
        }
        return sb.ToString();
    }

    public void Colorize(string secretWord)
    {
        List<char> chars = new List<char>(secretWord.ToCharArray());

        for (int i = 0; i < letterContainers.Length; i++)
        {
            Debug.Log($"to colorize iteration {i}");

            char letterToCheck = letterContainers[i].Letter;
            if (letterToCheck == secretWord[i])
            {
                // VALID
                letterContainers[i].SetValid();
                chars.Remove(letterToCheck);
            }
            else if (chars.Contains(letterToCheck))
            {
                // Potential
                letterContainers[i].SetPotential();
                chars.Remove(letterToCheck);
            }
            else 
            {
                // INVALID
                letterContainers[i].SetInvalid();
            }
        }
    }

}
