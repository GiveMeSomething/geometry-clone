using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState 
{
    protected PlayerBehaviour _playerBehaviour;

    public void SetContext(PlayerBehaviour behaviour)
    {
        _playerBehaviour = behaviour;
        SetUpEnviroment();
    }
    public abstract void SetUpEnviroment();
    public abstract void HandleUserSingleTouch();
    public abstract void StateByFrame();
    public abstract void GoThroughPortal();

    public virtual void OnCollisionEnter(Collision2D collision)
    {
        if (collision.transform.CompareTag(GameTag.Obstacle))
        {
            _playerBehaviour.Destroy();
        }
    }
}
