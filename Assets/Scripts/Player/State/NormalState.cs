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
        _gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        GameObject.FindGameObjectWithTag("PlayerSlab").transform.localScale = new Vector3(0, 0, 0);
    }
    public override void HandleUserSingleTouch()
    {
        //TODO: Make character jump
        if (IsGrounded)
        {
            _playerBehaviour.rb.velocity = Vector3.zero;
            _playerBehaviour.rb.AddForce(Vector2.up * _playerBehaviour.jumpSpeed);
            IsGrounded = false;
        }
    }
    public override void StateByFrame()
    {
        if(IsGrounded)
        {
            Debug.Log("Grounded");

            Vector3 Rotation = _playerBehaviour.Sprite.rotation.eulerAngles;
            Debug.Log(Rotation);
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            Debug.Log(Rotation);

            _playerBehaviour.Sprite.rotation = Quaternion.Euler(Rotation);
        }
        if (!IsGrounded)
        {
            _playerBehaviour.Sprite.Rotate(Vector3.back * 0.8f);
        }
    }
    public override void GoThroughPortal()
    {
        _playerBehaviour.TransitionTo(new FlyingState());
    }

    public override void OnCollisionEnter(Collision2D collision)
    {
        if (collision.transform.name.Equals("Spike"))
        {
            _playerBehaviour.Destroy();
        }
        if (!IsGrounded && (collision.transform.name.Equals("Ground")
            || collision.transform.name.Equals("Block")
            || collision.transform.name.Equals("Slab")))
        {
            IsGrounded = true;
        }
    }
}
