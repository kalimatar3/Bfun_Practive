using System.Collections.Generic;
using DuckGame.Ultilities;
using UnityEngine;

public class GameStateController : Singleton<GameStateController>
{
    public Statemachine<GameState> GameStatemachine;
    private GameLoadingState gameLoadingState;
    private GameHomeState gameHomeState;
    private GamePlayState gamePlayState;
    private GameEndState gameEndState;
    private GameWinState gameWinState;
    private GameLoseState gameLoseState;
    private void Start()
    {
        gameLoadingState = (GameLoadingState)new State();
        gameHomeState = new GameHomeState();
        gameWinState = new GameWinState();
        gameLoseState = new GameLoseState();
        gameEndState = (GameEndState)new State(gameWinState, gameLoseState); 
        GameStatemachine = new Statemachine<GameState>(
            gameLoadingState,
            gameHomeState
        );
        GameStatemachine.Initialize(gameLoadingState);

    }
}