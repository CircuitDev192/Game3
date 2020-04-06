using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Context<UIManager>
{
    #region States

    public UIMenuState menuState = new UIMenuState();
    public UIGameState gameState = new UIGameState();
    public UIPauseState pauseState = new UIPauseState();
    public UICreditsState creditsState = new UICreditsState();

    #endregion

    private void Awake()
    {
        EventManager.GameStateChanged += GameStateChanged;
        InitializeContext();
    }

    public override void InitializeContext()
    {
        currentState = menuState;
        currentState.EnterState(this);
    }

    private void GameStateChanged(GameState gameState)
    {
        currentState.ExitState(this);

        switch(gameState)
        {
            case GameState.MainMenu:
                currentState = menuState;
                break;

            case GameState.Play:
                currentState = this.gameState;
                break;

            case GameState.Paused:
                currentState = pauseState;
                break;

            case GameState.Credits:
                currentState = creditsState;
                break;
        }

        currentState.EnterState(this);
    }
}
