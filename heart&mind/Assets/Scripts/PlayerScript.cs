using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //component refs
    private Rigidbody2D rb;
    private SpriteRenderer mySprite;
    private Animator myAnim;

    //movement vars
    [SerializeField] float moveSpeed; //how fast player moves
    [SerializeField] float maxSpeed; //max horizontal speed
    
    //jumping vars
    private bool jump = false;
    [SerializeField]  float jumpPower;

    //appearance vars
    public Vector3 currentScale;

    //animation sprite refs
    public AnimationClip walkAnim;
    public AnimationClip idleAnim;
    public Sprite jumpUp;
    public Sprite jumpDown;

    //stuff for character switching
    public bool active;

    void Start()
    {
        //getting this objs components
        rb = this.GetComponent<Rigidbody2D>();
        mySprite = this.GetComponent<SpriteRenderer>();
        myAnim = this.GetComponent<Animator>();

        //lock the z orientation (so bro doesnt go upside down)
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        //initializing movement vars
        moveSpeed *= Time.deltaTime;

        //initializing jump vars
        jumpPower *= Time.deltaTime;

        //store the current scale
        currentScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (active) //checking if active. For the final level
        {
            //move right and left
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Force);
                if (currentScale.x > 0)
                {
                    currentScale.x *= 1;
                }
                else
                {
                    currentScale.x *= -1;
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Force);
                if (currentScale.x < 0)
                {
                    currentScale.x *= 1;
                }
                else
                {
                    currentScale.x *= -1;
                }
            }

            transform.localScale = currentScale;

            //jumping mechanic
            //floor objects in editor need to have lthe layer "floor" assigned to them
            if (Input.GetKey(KeyCode.W) && !jump || Input.GetKey(KeyCode.Space) && !jump)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
                jump = true;
                myAnim.enabled = false;
            }
        }

        //clamping horizontal speed
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        AnimationControl();
        JumpAnimControl();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            jump = false;
            myAnim.SetBool("jumping", false);
            myAnim.enabled = true;
        }
    }

    void AnimationControl() {

        if (!jump) {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
               myAnim.SetBool("walking", true);
            }

            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
                myAnim.SetBool("walking", false);
            }
        } else {
            myAnim.SetBool("walking", false);
            myAnim.SetBool("jumping", true);
        }

    }

    void JumpAnimControl() {
        if (rb.velocity.y >= 0) {
             mySprite.sprite = jumpUp;
         } else {
             mySprite.sprite = jumpDown;
          }

    }

}
