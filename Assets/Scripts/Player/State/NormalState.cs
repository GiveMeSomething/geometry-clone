using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NormalState : PlayerState
{
    private bool IsGrounded = false;

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
        //Debug.Log("Jump");
        if (IsGrounded)
        {
            this._playerBehaviour.rb.velocity = Vector3.zero;
            this._playerBehaviour.rb.AddForce(Vector2.up * this._playerBehaviour.jumpSpeed);
            IsGrounded = false;
        }
    }
    public override void Move()
    {
        //Do not thing
        if(IsGrounded)
        {
            Debug.Log("Grounded");

            Vector3 Rotation = this._playerBehaviour.Sprite.rotation.eulerAngles;
            Debug.Log(Rotation);
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            Debug.Log(Rotation);

            this._playerBehaviour.Sprite.rotation = Quaternion.Euler(Rotation);
        }
        if (!IsGrounded)
        {
            this._playerBehaviour.Sprite.Rotate(Vector3.back * 0.8f);
        }
    }
    public override void GoThroughPortal()
    {
        this._playerBehaviour.TransitionTo(new FlyingState());
    }

    public override void OnCollisionEnter(Collision2D collision)
    {
        if (collision.transform.name.Equals("Spike"))
        {
            _playerBehaviour.Destroy();
        }
        if (collision.transform.name.Equals("Ground"))
        {
            IsGrounded = true;
        }
    }
}
