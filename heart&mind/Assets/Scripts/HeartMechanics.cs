using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class HeartMechanics : MonoBehaviour
{
    
    //ref to transform and rigidbody components
    private Transform mySize;
    private Rigidbody2D rb;

    private float activeDuration = 5;

    public float angyLevel = 0;
    public float calmLevel = 0;

    private float shrinkRate = 0.1f;
    private float growthRate = 20f;
    private bool inZone = false;

    private bool shrunk = false;
    private bool grown = false;
    private bool resetting = false;
    
    private PlayerScript movementScript;
    private Animator myAnim;

    //ref to UI bar
    public Image angyBar;
    public Image calmBar;

    public Sprite angyUp;
    public Sprite angyDown;
    public Sprite normalUp;
    public Sprite normalDown;


    void Start()
    {
        //get rb and transform components
        mySize = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();

        //get ref to PlayerScript on obj
        movementScript = this.GetComponent<PlayerScript>();
        myAnim = this.GetComponent<Animator>();
        
    }


    void Update()
    {
        //if player not in the zone and they are small size
        if (!inZone && calmLevel > 0 || angyLevel > 0) {
            //grace period of lasting effect
            activeDuration -= 1 * Time.deltaTime;

            //grow when period is over
            if (activeDuration <= 0) {
                ResetSize();
            }
        }

        if (rb.velocity.magnitude != 0) {
            myAnim.SetBool("calm", false);
        }


      //  Debug.Log(rb.velocity.magnitude);

       angyBar.fillAmount = angyLevel / 1;
       calmBar.fillAmount = calmLevel / 10;


    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Calm Zone")) {
            inZone = true;
        }
    }

    //trigger stay to check if in calm zone
    void OnTriggerStay2D(Collider2D other) {
        //if in zone and not moving, call shrink
        if (other.gameObject.CompareTag("Calm Zone") && rb.velocity.magnitude <= 0.5f && !resetting && !grown) {
            inZone = true;
            Shrink();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        //start growing again after a certain amt of time once out of the zone
        if (other.gameObject.CompareTag("Calm Zone")) {
            inZone = false;
         //   Debug.Log("exiting");
        }

        if (angyLevel < 0.5f) {
            myAnim.SetBool("angy", false);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
         foreach (ContactPoint2D contact in other.contacts)
            {
                Vector2 normal = contact.normal;

                //if the collision happens horizontally
                if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y) && angyLevel < 1 && !resetting && !shrunk)
                {
                    Grow();
                    activeDuration = 3;
                    myAnim.SetBool("angy", true);
                }
            }
    }


    //function for shrinking
    void Shrink() {

        myAnim.SetBool("calm", true);
        //set limit to shrink time
        if (calmLevel < 10) {
            //Decrement x scale
            float newScaleMagnitude = Mathf.Abs(mySize.localScale.x) - (shrinkRate * Time.deltaTime);

        //apply new scale while preserving the original sign of x, y, and z
        mySize.localScale = new Vector3(
            Mathf.Sign(mySize.localScale.x) * newScaleMagnitude, 
            Mathf.Sign(mySize.localScale.y) * newScaleMagnitude, 
            Mathf.Sign(mySize.localScale.z) * newScaleMagnitude
        );

            movementScript.currentScale = mySize.localScale;
 
            calmLevel += 2 * Time.deltaTime;

           shrunk = true;
        } else {
            calmLevel = 10;
        }
    }

    void Grow() {
        angyLevel += 2.5f * Time.deltaTime;
        

        float newScaleMagnitude = Mathf.Abs(mySize.localScale.x) + (growthRate * angyLevel * Time.deltaTime);

        mySize.localScale = new Vector3(
            Mathf.Sign(mySize.localScale.x) * newScaleMagnitude, 
            Mathf.Sign(mySize.localScale.y) * newScaleMagnitude, 
            Mathf.Sign(mySize.localScale.z) * newScaleMagnitude
        );

         movementScript.currentScale = mySize.localScale;

        grown = true;

        if (angyLevel > 1) {
            angyLevel = 1;
            myAnim.SetBool("angy", true);
            movementScript.jumpUp = angyUp;
            movementScript.jumpDown = angyDown;
        }

    }

     void ResetSize() {

        resetting = true;

        if (shrunk) {
            float newScaleMagnitude = Mathf.Abs(mySize.localScale.x) + (shrinkRate * Time.deltaTime);

            mySize.localScale = new Vector3(
            Mathf.Sign(mySize.localScale.x) * newScaleMagnitude, 
            Mathf.Sign(mySize.localScale.y) * newScaleMagnitude, 
            Mathf.Sign(mySize.localScale.z) * newScaleMagnitude
        );

            movementScript.currentScale = mySize.localScale;
 
            calmLevel -= 2 * Time.deltaTime;

            if (calmLevel < 0) {
                calmLevel = 0;
                shrunk = false;
                activeDuration = 5;
                resetting = false;
            }
        }

        if (grown) {
            float newScaleMagnitude = Mathf.Abs(mySize.localScale.x) - (growthRate/9.6f * angyLevel * Time.deltaTime);

        mySize.localScale = new Vector3(
            Mathf.Sign(mySize.localScale.x) * newScaleMagnitude, 
            Mathf.Sign(mySize.localScale.y) * newScaleMagnitude, 
            Mathf.Sign(mySize.localScale.z) * newScaleMagnitude
        );

         movementScript.currentScale = mySize.localScale;

         angyLevel -= 0.25f * Time.deltaTime;

         if (angyLevel < 0.2f) {
             myAnim.SetBool("angy", false);
             movementScript.jumpUp = normalUp;
             movementScript.jumpDown = normalDown;
         }

             if (angyLevel < 0) {
                angyLevel = 0;
                grown = false;
                activeDuration = 5;
                resetting = false;
                myAnim.SetBool("angy", false);
            }
        }

    }

}
