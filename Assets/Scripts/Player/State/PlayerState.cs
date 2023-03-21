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
    }
}
