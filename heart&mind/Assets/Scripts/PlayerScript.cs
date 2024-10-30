using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerScript : MonoBehaviour
{
    //component refs
    private Rigidbody2D rb;
    private SpriteRenderer mySprite;
    private Animator myAnim;

    //movement vars
    [SerializeField] float moveSpeed;
    [SerializeField] float maxSpeed;

    //jumping vars
    private bool jump = false;
    [SerializeField] float jumpPower;

    //appearance vars
    public Vector3 currentScale;

    //animation sprite refs
    public AnimationClip walkAnim;
    public AnimationClip idleAnim;
    public Sprite jumpUp;
    public Sprite jumpDown;

    //character switching
    public bool playing;
    private GameObject cam;

    void Start()
    {
        //Component references
        rb = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();

        //lock z orientation
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        //store current scale
        currentScale = transform.localScale;
        cam = GameObject.FindGameObjectWithTag("vCam");
    }

    void FixedUpdate()
    {
        if (playing) 
        {
            HandleMovement();
            HandleJump();
        }

        ClampSpeed();
    }

    void Update()
    {
        if (playing) {
             if (playing && Input.GetKeyDown(KeyCode.Tab))
            {
                 Debug.Log("Trying to switch " + gameObject + " + " + playing);
                cam.GetComponent<CinemachineBehavior>().CameraSet();
             }
        }

        AnimationControl();
        JumpAnimControl();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Force);
            currentScale.x = Mathf.Abs(currentScale.x);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Force);
            currentScale.x = -Mathf.Abs(currentScale.x);
        }

        transform.localScale = currentScale;
    }

    private void HandleJump()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && !jump)
        {
            rb.AddForce(Vector2.up * jumpPower * Time.fixedDeltaTime, ForceMode2D.Impulse);
            jump = true;
            myAnim.enabled = false;
        }
    }

    private void ClampSpeed()
    {
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("pushable"))
        {
            jump = false;
            myAnim.SetBool("jumping", false);
            myAnim.enabled = true;
        }
    }

    void AnimationControl() 
    {
        if (!jump && playing) 
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
                myAnim.SetBool("walking", true);
            }
            else {
                myAnim.SetBool("walking", false);
            }
        } 
        else 
        {
            myAnim.SetBool("walking", false);
            myAnim.SetBool("jumping", true);
        }
    }

    void JumpAnimControl() 
    {
        if (rb.velocity.y >= 0) {
            mySprite.sprite = jumpUp;
        } else {
            mySprite.sprite = jumpDown;
        }
    }
}
