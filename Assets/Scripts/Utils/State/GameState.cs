﻿using System;
public abstract class GameState
{
    protected GameManager gameManager;

    public GameState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public abstract void EnterState();

    public abstract void Update();

    public abstract void ExitState();
}

