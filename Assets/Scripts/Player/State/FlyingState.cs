using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingState : PlayerState
{
    private bool IsTouching = false;

    public override void SetUpEnviroment()
    {
        // Disable UnityEngine gravity in fly mode
        _playerBehaviour.rb.gravityScale = 0.0f;

        // Set Active for rocket in fly mode
        var rocket = _playerBehaviour.transform.GetChild(1);
        rocket.gameObject.SetActive(false);
    }

    public override void HandleUserSingleTouch()
    {
        IsTouching = true;
        _playerBehaviour.transform.Translate(new Vector3(0, _playerBehaviour.speed * Time.deltaTime, 0));
    }

    public override void StateByFrame()
    {
        if (!IsTouching)
        {
            _playerBehaviour.transform.Translate(new Vector3(0, _playerBehaviour.speed * Time.deltaTime * -1, 0));
        }
        IsTouching = false;
    }

    public override void GoThroughPortal()
    {
        _playerBehaviour.TransitionTo(new NormalState());
    }
}
