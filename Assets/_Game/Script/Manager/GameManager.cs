using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { MainMenu, Gameplay, Finish}

public class GameManager : Singleton<GameManager>
{
    private GameState state;
    // Start is called before the first frame update
    void Awake()
    {
        //setup tong quan game
        //setup data
        ChangeState(GameState.MainMenu);
    }

    
    public void ChangeState(GameState gameState)
    {
        state = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return state == gameState;
    }

}
