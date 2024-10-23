using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMechanics : MonoBehaviour
{
    
    private Transform mySize;
    private Rigidbody2D rb;

    private float activeDuration = 5;

    public float angyLevel = 0;
    public float calmLevel = 0;

    private float shrinkRate = 0.1f;
    
    private PlayerScript movementScript;

    void Start()
    {
        mySize = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
        movementScript = this.GetComponent<PlayerScript>();
    }


    void Update()
    {



    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Calm Zone") && rb.velocity.magnitude == 0) {
            Shrink();
        }
    }

    void OnTriggerExit2D(Collider2D other) {

    }

    void Shrink() {
        if (calmLevel < 10) {
            float newScaleMagnitude = Mathf.Abs(mySize.localScale.x) - (shrinkRate * Time.deltaTime);

            // Make sure the new scale does not go below a minimum value (to prevent disappearing)
            newScaleMagnitude = Mathf.Max(newScaleMagnitude, 0.1f); // Set a minimum scale to avoid shrinking to zero

        // Apply the new scale while preserving the original sign of x, y, and z
        mySize.localScale = new Vector3(
            Mathf.Sign(mySize.localScale.x) * newScaleMagnitude, 
            Mathf.Sign(mySize.localScale.y) * newScaleMagnitude, 
            Mathf.Sign(mySize.localScale.z) * newScaleMagnitude
        );

            movementScript.currentScale = mySize.localScale;
 
            calmLevel += 2 * Time.deltaTime;
           // Debug.Log(mySize.localScale);
           // Debug.Log(calmLevel);
        } 
    }

    void Grow() {

    }
}
