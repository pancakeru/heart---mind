using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Tilemaps;

public class HeartMechanics : MonoBehaviour
{
    
    //ref to transform and rigidbody components
    private Transform mySize;
    private Rigidbody2D rb;

    private float activeDuration = 3;

    public float angyLevel = 0;
    public float calmLevel = 0;

    private float shrinkRate = 0.1f;
    private bool inZone = false;

    private bool shrunk = false;
    private bool grown = false;
    private bool resetting = false;
    
    private PlayerScript movementScript;

    void Start()
    {
        //get rb and transform components
        mySize = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();

        //get ref to PlayerScript on obj
        movementScript = this.GetComponent<PlayerScript>();
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

       // Debug.Log(angyLevel);


    }

    //trigger stay to check if in calm zone
    void OnTriggerStay2D(Collider2D other) {
        //if in zone and not moving, call shrink
        if (other.gameObject.CompareTag("Calm Zone") && rb.velocity.magnitude == 0 && !resetting) {
            inZone = true;
            Shrink();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        //start growing again after a certain amt of time once out of the zone
        if (other.gameObject.CompareTag("Calm Zone")) {
            inZone = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Wall")) {
            foreach (ContactPoint2D contact in other.contacts)
            {
                Vector2 normal = contact.normal;

                //if the collision happens horizontally
                if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y) && angyLevel < 60f && !resetting)
                {
                    Grow();
                    activeDuration = 3;
                }
            }
        }
    }


    //function for shrinking
    void Shrink() {
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

           if (mySize.localScale.y < 1) {
            shrunk = true;
            }
        } else {
            calmLevel = 10;
        }
    }

    void Grow() {
        angyLevel += 500f * Time.deltaTime;
        //Debug.Log(angyLevel);

        float newScaleMagnitude = Mathf.Abs(mySize.localScale.x) * (1 + angyLevel/100);

        mySize.localScale = new Vector3(
            Mathf.Sign(mySize.localScale.x) * newScaleMagnitude, 
            Mathf.Sign(mySize.localScale.y) * newScaleMagnitude, 
            Mathf.Sign(mySize.localScale.z) * newScaleMagnitude
        );

         movementScript.currentScale = mySize.localScale;

        if (mySize.localScale.y > 1) {
            grown = true;
        }

        if (angyLevel > 60) {
            angyLevel = 60;
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
                activeDuration = 3;
                resetting = false;
            }
        }

    }

}
