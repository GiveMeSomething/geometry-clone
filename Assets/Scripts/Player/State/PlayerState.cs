using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState 
{
    protected PlayerBehaviour _playerBehaviour;
    protected GameObject _gameObject;

    public void SetContext(PlayerBehaviour behaviour)
    {
        this._playerBehaviour = behaviour;
        this._gameObject = behaviour.gameObject;
        this.SetUpEnviroment();
    }
    public abstract void SetUpEnviroment();
    public abstract void Moving();
    public abstract void DoNotThing();
    public abstract void GoThroughPortal();
}
