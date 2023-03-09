using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    public float speed;
    private PlayerState _state;
    public void TransitionTo(PlayerState state)
    {
        this._state = state;
        this._state.SetContext(this);
    }
    public void HandleUserSingleTouch()
    {
        this._state.HandleUserSingleTouch();
    }
    public void Move()
    {
        this._state.Move();
    }

    public void GoThroughPortal()
    {
        this._state.GoThroughPortal();
    }
    // Start is called before the first frame update
    void Start()
    {
        this.TransitionTo(new NormalState());

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            this.HandleUserSingleTouch();
        }
        this.Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.name.Equals("Portal"))
        {
            this.GoThroughPortal();
        }
        else
        {
            this._state.OnCollisionEnter(collision);
        }
    }
    public void Destroy()
    {
        Debug.Log("End Game");
    }
}
