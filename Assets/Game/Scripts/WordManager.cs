using System.Globalization;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static WordManager Instance;

    [Header("***Elements***")]
    [SerializeField] private string secretWord;
    [SerializeField] private TextAsset wordsText;
    private string words;

    [Header("***Settings***")]
    private bool shouldReset;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        words = wordsText.text;
    }
    void Start()
    {
        SetNewSecretWord();
        GameManager.OnGameStateChanged += GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                
                break;
            case GameState.LevelComplete:
                shouldReset = true;
                break;
            case GameState.Game:
                if (shouldReset)
                {
                    SetNewSecretWord();
                }
                break;
            case GameState.GameOver:
                shouldReset = true;
                break;


        }
    }


    public string SecretWord => secretWord.ToUpper();

    private void SetNewSecretWord()
    {
        Debug.Log("String total letter lenght: " + words.Length); 
        int wordCount = (words.Length + 2) / 7; // 7 is consideration for new line character

        int wordIndex = Random.Range(0, wordCount);

        int wordStartIndex = wordIndex * 7;

        secretWord = words.Substring(wordStartIndex, 5).ToUpper(CultureInfo.InvariantCulture);

        shouldReset = false;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallback;
    }
}
