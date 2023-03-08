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
    public void Move()
    {
        this._state.Moving();
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
        this.Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.name.Equals("Portal"))
        {
            this.GoThroughPortal();
        }
    }
}
