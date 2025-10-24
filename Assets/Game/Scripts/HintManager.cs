using UnityEngine;
using System.Collections.Generic;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance;

    [Header("***Elements***")]
    private KeyboardKey[] keys;
    [SerializeField] private GameObject keyboard;

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
        keys = keyboard.GetComponentsInChildren<KeyboardKey>();
        Debug.Log($"HINT MANAGER GOT {keys.Length} keyboard keys");
    }

    // Grays out the letters from keyboard
    // if they're not included in secret word
    public void KeyboardHint()
    {
        string secretWord = WordManager.Instance.SecretWord;

        List<KeyboardKey> untouchedKeys = new List<KeyboardKey>();
        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i].IsUntouched)
            {
                untouchedKeys.Add(keys[i]);
            }
        }

        // removing the keys inside secret word
        List<KeyboardKey> t_untouchedKeys = new List<KeyboardKey>(untouchedKeys);
        for (int i = 0; i < untouchedKeys.Count; i++)
        {
            if (secretWord.Contains(untouchedKeys[i].Letter))
            {
                t_untouchedKeys.Remove(untouchedKeys[i]);
            }
        }
        if (t_untouchedKeys.Count < 1)
        {
            return; // we cannot apply
        }

        int randomKeyIndex = Random.Range(0, t_untouchedKeys.Count);
        t_untouchedKeys[randomKeyIndex].SetInvalid();
    }

    List<int> letterHintGivenIndices = new List<int>();
    public void LetterHint()
    {
        Debug.LogWarning("GIVEN LETTER HINTS COUNT : " +letterHintGivenIndices.Count);
        if (letterHintGivenIndices.Count >= 5)
        {
            Debug.LogWarning("Cannot give any hints - ALL GIVEN");
            return; // given all hints possible
        }
        List<int> letterHintNotGivenIndices = new List<int>();
        for(int i = 0; i < 5; i++)
        {
            if (!letterHintGivenIndices.Contains(i))
            {
                letterHintNotGivenIndices.Add(i);
            }
        }
        WordContainer currentWordContainer = InputManager.Instance.CurrentWordContainer;
        string secretWord = WordManager.Instance.SecretWord;

        int randomIndex = 
            letterHintNotGivenIndices[Random.Range(0, letterHintNotGivenIndices.Count)];
        letterHintGivenIndices.Add(randomIndex);

        currentWordContainer.AddAsHint(randomIndex, secretWord[randomIndex]);

    }
}
