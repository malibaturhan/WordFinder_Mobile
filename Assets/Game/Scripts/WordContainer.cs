using System;
using UnityEngine;

public class WordContainer : MonoBehaviour
{
    [Header("***Elements***")]
    private LetterContainer[] letterContainers;
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
        letterContainers[0].SetLetter(letter);
    }
}
