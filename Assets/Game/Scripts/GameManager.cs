using System;
using UnityEngine;

public enum GameState
{
    Menu,
    Game,
    LevelComplete,
    GameOver,
    Idle  //MAYBE PLAY SOMETHING AND INTERRUPTING PLAYER INPUT UNTIL
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("***Settings***")]
    private GameState gamestate;

    [Header("***Events***")]
    public static Action<GameState> OnGameStateChanged;

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

    public bool IsInGameState => gamestate == GameState.Game;


    public void SetGameState(GameState gameState)
    {
        this.gamestate = gameState;
        OnGameStateChanged?.Invoke(gameState);
    }
    public void NextButtonCallback()
    {
        SetGameState(GameState.Game);
    }
    public void PlayButtonCallback()
    {
        SetGameState(GameState.Game);
    }
    public void BackButtonCallback()
    {
        SetGameState(GameState.Game);
    }

}
