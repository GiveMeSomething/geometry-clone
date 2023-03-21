using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float rotateSpeed;
    public float gravity;

    public Rigidbody2D rb;
    public Transform Sprite;

    private PlayerState _state;

    public Observable<bool> GameOverEvent = new Observable<bool>();
    public float totalDistanceTraveled = 0f;

    public void TransitionTo(PlayerState state)
    {
        _state = state;
        _state.SetContext(this);
    }

    public void HandleUserSingleTouch()
    {
        _state.HandleUserSingleTouch();
    }

    public void StateByFrame()
    {
        _state.StateByFrame();
    }

    public void GoThroughPortal()
    {
        _state.GoThroughPortal();
    }

    // Start is called before the first frame update
    private void Start()
    {
        TransitionTo(new NormalState());
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float distanceTraveledThisFrame = speed * Time.deltaTime;
        totalDistanceTraveled += distanceTraveledThisFrame;
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    HandleUserSingleTouch();
                }
            }
        }
        StateByFrame();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.name.Equals(GameTag.Portal))
        {
            GoThroughPortal();
        }
        else
        {
            // _state.OnCollisionEnter(collision);
            GameOverEvent.Notify(true);
        }
    }

    public void Destroy()
    {
        GameOverEvent.Notify(true);
        Debug.Log("Die");
    }
}
