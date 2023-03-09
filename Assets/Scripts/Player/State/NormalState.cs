using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : PlayerState
{
    public override void SetUpEnviroment()
    {
        //TODO: Set up the enviroment
        Debug.Log("Normal Enviroment");
        this._gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        _childGameObject.transform.localScale = new Vector3(0, 0, 0);
    }
    public override void HandleUserSingleTouch()
    {
        //TODO: Make character jump
        Debug.Log("Jump");
        
    }
    public override void Move()
    {
        //Do not thing
    }
    public override void GoThroughPortal()
    {
        this._playerBehaviour.TransitionTo(new FlyingState());
    }

    public override void OnCollisionEnter(Collision2D collision)
    {
        
    }
}