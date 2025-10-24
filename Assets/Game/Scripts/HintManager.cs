using UnityEngine;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance;
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

    }
    
    public void KeyboardHint()
    {
        
    }

    public void LetterHint()
    {
        Debug.Log("letter hint activated");
    }
}
