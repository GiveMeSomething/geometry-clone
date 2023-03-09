using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState 
{
    protected PlayerBehaviour _playerBehaviour;
    protected GameObject _gameObject;
    protected GameObject _childGameObject;

    public void SetContext(PlayerBehaviour behaviour)
    {
        this._playerBehaviour = behaviour;
        this._gameObject = behaviour.gameObject;
        this._childGameObject = GameObject.FindGameObjectWithTag("PlayerSlab");
        this.SetUpEnviroment();
    }
    public abstract void SetUpEnviroment();
    public abstract void HandleUserSingleTouch();
    public abstract void Move();
    public abstract void GoThroughPortal();
    public abstract void OnCollisionEnter(Collision2D collision);
}
