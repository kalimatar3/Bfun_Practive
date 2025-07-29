using UnityEngine;

public enum GameStateEnum {
Loading,
Home,
GmaePlay,
End,
    }
public class GameState : State
{
    public GameState(params State[] childrens) : base(childrens)
    {
    }
}