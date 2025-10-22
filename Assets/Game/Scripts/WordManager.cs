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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string SecretWord => secretWord.ToUpper();
}
