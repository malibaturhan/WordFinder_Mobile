using System;
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
    public string GetWord()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < letterContainers.Length; i++)
        {
            sb.Append(letterContainers[i].Letter);
        }
        return sb.ToString();
    }

    public bool IsComplete => currentLetterIndex >= letterContainers.Length;
}
