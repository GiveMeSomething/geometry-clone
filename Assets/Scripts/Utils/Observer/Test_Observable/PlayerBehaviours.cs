using UnityEngine;
using System.Collections;

public class PlayerBehaviours : MonoBehaviour
{
    public float speed = 2.0f; // the speed at which the player moves
    // Detect when the player jumps and when the player dies, and send out events using the Observable<T> class
    public Observable<bool> onJump = new Observable<bool>();
    public Observable<bool> onGameOver = new Observable<bool>();
    //Scrore
    public float totalDistanceTraveled = 0f;

    private Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //move the player in the x direction
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //add force to the player in the y direction to make him jump
            rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            onJump.Notify(true);
        }

        //Calculate the total distance traveled
        float distanceTraveledThisFrame = speed * Time.deltaTime;
        totalDistanceTraveled += distanceTraveledThisFrame;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameTag.Obstacle)
        {
            //stop the player from moving
            rb.velocity = Vector2.zero;
            onGameOver.Notify(true);
        }
    }
}

