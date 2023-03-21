using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NormalState : PlayerState
{
    private bool IsGrounded = false;

    public override void SetUpEnviroment()
    {
        // Enable UnityEngine gravity in normal mode
        _playerBehaviour.rb.gravityScale = _playerBehaviour.gravity * GameConst.SPEED_SCALE;

        // Hide rocket when in normal mode
        var rocket = _playerBehaviour.transform.GetChild(1);
        rocket.gameObject.SetActive(false);
    }

    public override void HandleUserSingleTouch()
    {
        //TODO: Make character jump
        if (IsGrounded)
        {
            _playerBehaviour.rb.velocity = Vector3.zero;
            //_playerBehaviour.rb.AddForce(new Vector2(0, _playerBehaviour.jumpSpeed * Mathf.Sqrt(GameConst.SPEED_SCALE)), ForceMode2D.Impulse);
            _playerBehaviour.rb.AddForce(new Vector2(0, _playerBehaviour.jumpSpeed * Mathf.Sqrt(_playerBehaviour.rb.gravityScale)), ForceMode2D.Impulse);
            
            IsGrounded = false;
        }
    }
    public override void StateByFrame()
    {
        if (IsGrounded)
        {
            Vector3 Rotation = _playerBehaviour.Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;

            _playerBehaviour.Sprite.rotation = Quaternion.Euler(Rotation);
        }

        if (!IsGrounded)
        {
            _playerBehaviour.Sprite.Rotate(Vector3.back * _playerBehaviour.rotateSpeed);
        }
    }
    public override void GoThroughPortal()
    {
        _playerBehaviour.TransitionTo(new FlyingState());
        Vector3 Rotation = _playerBehaviour.Sprite.rotation.eulerAngles;
        Rotation.z = Mathf.Round(Rotation.z / 90) * 90;

        _playerBehaviour.Sprite.rotation = Quaternion.Euler(Rotation);
    }

    public override void OnCollisionEnter(Collision2D collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.transform.CompareTag(GameTag.BuildingBlock))
        {
            var shouldDestroy = false;
            foreach (var contactPoint in collision.contacts)
            {
                var normalized = contactPoint.point.normalized;

                // Skip when the contact is above the cube
                if (Vector3.Dot(normalized, Vector3.up) < 0.5f)
                {
                    continue;
                }

                if (normalized.x < 0f || normalized.y < 0f)
                {
                    shouldDestroy = true;
                    break;
                }
            }

            if (shouldDestroy)
            {
                // TODO: Remove log later
                Debug.Log("Game OVer");
                _playerBehaviour.Destroy();
                return;
            }
        }

        if (collision.transform.CompareTag(GameTag.BuildingBlock))
        {
            IsGrounded = true;
        }

        if (!IsGrounded)
        {
            if (collision.transform.CompareTag(GameTag.Platform))
            {
                IsGrounded = true;
            }
        }
    }
}
