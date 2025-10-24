using System;
using UnityEngine;

public class KeyboardColorizer : MonoBehaviour
{
    [Header("***Elements***")]
    private KeyboardKey[] keys;

    void Awake()
    {
        keys = GetComponentsInChildren<KeyboardKey>();
    }

    private void Start()
    {
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

    private void Initialize()
    {
        for (int i = 0; i < keys.Length; i++) 
        {
            keys[i].Initialize();
        }
    }

    public void Colorize(string secretWord, string wordToCheck)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            char keyLetter = keys[i].Letter;
            for (int j = 0; j < wordToCheck.Length; j++)
            {
                if (keyLetter != wordToCheck[j])
                {
                    continue;
                }
                // the key we pressed is equals to the current wordToCheck letter

                if (keyLetter == secretWord[j])
                {
                    //valid
                    keys[i].SetValid();
                }
                else if (secretWord.Contains(keyLetter))
                {
                    //potential
                    keys[i].SetPotential();
                }
                else
                {
                    // invalid
                    keys[i].SetInvalid();
                }
            }
        }
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallback;
    }
}
