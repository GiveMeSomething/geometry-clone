using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingState : PlayerState
{
    private bool IsTouching = false;
    public override void SetUpEnviroment()
    {
        //TODO: Set up the enviroment
        Debug.Log("Flying Enviroment");
        this._gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    public override void HandleUserSingleTouch()
    {
        IsTouching = true;
        this._gameObject.transform.Translate(new Vector3(0, this._playerBehaviour.speed * Time.deltaTime, 0));
    }
    public override void Move()
    {
        if (!IsTouching)
        {
            this._gameObject.transform.Translate(new Vector3(0, this._playerBehaviour.speed * Time.deltaTime * -1, 0));
        }
        IsTouching = false;
    }
    public override void GoThroughPortal()
    {
        this._playerBehaviour.TransitionTo(new NormalState());
    }


}
