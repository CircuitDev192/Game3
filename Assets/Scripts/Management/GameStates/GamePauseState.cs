using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseState : GameBaseState
{
    public override void EnterState(GameManager context)
    {
        Debug.Log("Game entered pause state!");
        EventManager.TriggerGameStateChanged(GameState.Paused);
        Time.timeScale = 0;
    }

    public override void ExitState(GameManager context)
    {
        Debug.Log("Game exited pause state!");
        Time.timeScale = 1;
    }

    public override BaseState<GameManager> UpdateState(GameManager context)
    {
        if (Input.GetKeyDown(KeyCode.Escape)) return context.playState;

        return this;
    }
}
