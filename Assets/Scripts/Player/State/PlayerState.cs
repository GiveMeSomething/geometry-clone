using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState 
{
    protected PlayerBehaviour _playerBehaviour;
    protected GameObject _gameObject;

    public void SetContext(PlayerBehaviour behaviour)
    {
        _playerBehaviour = behaviour;
        _gameObject = behaviour.gameObject;
        SetUpEnviroment();
    }
    public abstract void SetUpEnviroment();
    public abstract void HandleUserSingleTouch();
    public abstract void StateByFrame();
    public abstract void GoThroughPortal();
    public abstract void OnCollisionEnter(Collision2D collision);
}
