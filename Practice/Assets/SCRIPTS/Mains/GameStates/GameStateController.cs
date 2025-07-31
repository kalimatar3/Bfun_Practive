using System.Collections.Generic;
using DuckGame.Ultilities;
using UnityEngine;
public enum GameStateEnum
{
    Loading,
    Home,
    GamePlay,
    Win,
    Lose
}
public class GameStateController : Singleton<GameStateController>
{
    public Statemachine<GameState> Statemachine;
    private GameLoadingState gameLoadingState;
    private GameHomeState gameHomeState;
    private GamePlayState gamePlayState;
    private GameEndState gameEndState;
    private GameWinState gameWinState;
    private GameLoseState gameLoseState;
    public override void Awake()
    {
        base.Awake();
        gameLoadingState = new GameLoadingState();
        gameHomeState = new GameHomeState();
        gameWinState = new GameWinState();
        gameLoseState = new GameLoseState();
        gamePlayState = new GamePlayState();
        gameEndState = new GameEndState(
            gameWinState,
            gameLoseState
        );
        Statemachine = new Statemachine<GameState>(
            gameLoadingState,
            gameHomeState,
            gamePlayState,
            gameWinState,
            gameLoseState
        );
        Statemachine.Initialize(gameLoadingState);

    }
}