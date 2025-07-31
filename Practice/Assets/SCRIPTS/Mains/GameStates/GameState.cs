using UnityEngine;
public abstract class GameState : State
{
    public GameState(params State[] childrens) : base(childrens)
    {
    }
}