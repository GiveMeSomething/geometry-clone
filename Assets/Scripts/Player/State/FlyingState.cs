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
        _gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        GameObject.FindGameObjectWithTag("PlayerSlab").transform.localScale = new Vector3(1.5f, 0.2f, 0);
    }
    public override void HandleUserSingleTouch()
    {
        IsTouching = true;
        _gameObject.transform.Translate(new Vector3(0, _playerBehaviour.speed * Time.deltaTime, 0));
    }
    public override void StateByFrame()
    {
        if (!IsTouching)
        {
            _gameObject.transform.Translate(new Vector3(0, _playerBehaviour.speed * Time.deltaTime * -1, 0));
        }
        IsTouching = false;
    }
    public override void GoThroughPortal()
    {
        _playerBehaviour.TransitionTo(new NormalState());
    }

    public override void OnCollisionEnter(Collision2D collision)
    {
        if (collision.transform.name.Equals("Spike"))
        {
            _playerBehaviour.Destroy();
        }
    }
}
