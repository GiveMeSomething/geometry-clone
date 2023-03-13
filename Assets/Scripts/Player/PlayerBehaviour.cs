using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    public float speed;

    [SerializeField]
    public float jumpSpeed;

    public Rigidbody2D rb;
    public Transform Sprite;

    private PlayerState _state;
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
    void Start()
    {
        TransitionTo(new NormalState());
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            HandleUserSingleTouch();
        }
        StateByFrame();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.name.Equals("Portal"))
        {
            GoThroughPortal();
        }
        else
        {
            _state.OnCollisionEnter(collision);
        }
    }
    public void Destroy()
    {
        Debug.Log("End Game");
    }
}
