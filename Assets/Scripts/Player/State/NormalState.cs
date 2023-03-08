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
    }
    public override void Moving()
    {
        //TODO: Make character jump
        Debug.Log("Jump");
        
    }
    public override void DoNotThing()
    {
        //Do not thing
    }
    public override void GoThroughPortal()
    {
        this._playerBehaviour.TransitionTo(new FlyingState());
    }

    
}
