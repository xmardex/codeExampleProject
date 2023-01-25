
public abstract class PlayerStateBase 
{
    public abstract string ID {get;set;}
    public abstract void EnterState(PlayerStateManager stateManager);
    public abstract void UpdateState(PlayerStateManager stateManager);
}
