using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static WordManager Instance;

    [Header("***Elements***")]
    [SerializeField] private string secretWord;

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
    }

    public string SecretWord => secretWord.ToUpper();
}
