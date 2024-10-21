using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //component refs
    private Rigidbody2D rb;
    private SpriteRenderer mySprite;
    private Animator myAnim;

    //movement vars
    private float moveSpeed; //how fast player moves
    private float maxSpeed = 5.5f; //max horizontal speed

    //jumping vars
    private bool jump = false;
    private float jumpPower;


    void Start()
    {
        //getting this objs components
        rb = this.GetComponent<Rigidbody2D>();
        mySprite = this.GetComponent<SpriteRenderer>();
        myAnim = this.GetComponent<Animator>();

        //lock the z orientation (so bro doesnt go upside down)
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        //initializing movement vars
        moveSpeed = 300 * Time.deltaTime;

        //initializing jump vars
        jumpPower = 25000 * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        //move right and left
        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.A)) {
            rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Force);
        }

        //clamping horizontal speed
        if (Mathf.Abs(rb.velocity.x) > maxSpeed) {
             rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        //jumping mechanic
        if(Input.GetKey(KeyCode.W) && !jump) {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
            jump = true;
        }

    }

    void OnCollisionEnter2D(Collision2D other) {
        //reset jump upon collision with wall obj
        if (other.gameObject.CompareTag("Wall")) {
            jump = false;
        }
    }
}
