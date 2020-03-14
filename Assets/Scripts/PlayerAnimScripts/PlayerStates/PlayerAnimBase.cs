using UnityEngine;

public abstract class PlayerAnimBase
{
    public abstract void EnterState(PlayerAnimFSM player);
    public abstract void Update(PlayerAnimFSM player);
}
