using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [Header("***Elements***")]
    [SerializeField] private WordContainer[] wordContainers;

    void Start()
    {
        Initialize();
        KeyboardKey.onKeyPressed += KeyPressedCallback;
    }

    private void KeyPressedCallback(char letter)
    {
        wordContainers[0].Add(letter);
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
