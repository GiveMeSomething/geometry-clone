using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2.0f; // the speed at which the player moves
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GameTag.Powerup))
        {
            HandlePowerupCollisionAsync(other);
        }
    }

    private async void HandlePowerupCollisionAsync(Collision2D collision)
    {
        //destroy the powerup
        Destroy(collision.gameObject);
        // Speed up for 5 seconds
        speed *= 10;
        await Timer.SetTimeout(() => { speed /= 10; }, 5f);
    }


    // Update is called once per frame
    void Update()
    {
        //move the player in the x direction
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
